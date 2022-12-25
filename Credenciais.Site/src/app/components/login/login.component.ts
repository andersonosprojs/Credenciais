import { Token } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UtilsService } from 'src/app/core/utils.service';
import { Email } from 'src/app/models/email.model';
import { Login } from 'src/app/models/login.model';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { EmailService } from 'src/app/services/email.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  hide: boolean = true;
  token?: Token;
  router?: Router;
  rootUrl?: string;

  form!: FormGroup; 

  constructor(
    router: Router,
    private autenticacaoServico: AutenticacaoService,
    private emailServico: EmailService,
    private utilsServico: UtilsService,
    private localStorageServico: LocalStorageService,
    private fb: FormBuilder
  ) { 
    this.router = router;
  }

  ngOnInit(): void {
    this.rootUrl = window.location.href.replace('login', '');
    
    const jsonToken = this.localStorageServico.getData('token');
    if (jsonToken != '')
      this.token = JSON.parse(jsonToken);

    if (this.token != undefined && this.token != null) {
      this.router?.navigate(['/menu']);
      return
    }
    
    this.form = this.fb.group({ 
      login: new FormControl('', [Validators.required, Validators.email]),
      senha: new FormControl('')
    });
  }

  logar() {
    try {

      if (!this.form.valid) {
        this.utilsServico.showNotificacao('E-mail vazio ou inválido');
        return
      }

      if (this.form.get('senha')?.value == undefined || this.form.get('senha')?.value == '') {
        this.utilsServico.showNotificacao('Senha é obrigatória');
        return
      }

      const login: Login = {
        Login: this.form.get('login')?.value,
        Senha: this.form.get('senha')?.value
      }

      this.autenticacaoServico.login(login).subscribe({
        next: (value: any) => {
          this.localStorageServico.saveData('token', JSON.stringify(value));
          this.router?.navigate(['/menu']);
        },
        error: (value: any) => {
          const erros = value.error.errors;
          erros.forEach((erro: string) => {
            this.utilsServico.showNotificacao(erro);
          });
        }
      });
    } catch (erro) {
      this.utilsServico.showNotificacao(`Erro ao logar. Detalhes: ${erro}`);
    }
  }

  enviarEmail() {
    try {

      if (!this.form.valid) {
        this.utilsServico.showNotificacao('E-mail vazio ou inválido');
        return
      }

      const encryptLogin = this.utilsServico.encrypt(this.form.get('login')?.value);
      const encryptKey = this.utilsServico.encrypt(this.getKey());

      const email: Email = {
        EmailDestino: this.form.get('login')?.value,
        Assunto: 'Credenciais.Site (redefinição de senha)',
        Mensagem: `Clique no link para redefinir senha: ${this.rootUrl}redefinirsenha?value=${encryptLogin}&key=${encryptKey}`
      }

      this.emailServico.enviarEmail(email).subscribe({
        next: (value: any) => {
          this.utilsServico.showNotificacao(value.mensagem);
        },
        error: (value: any) => {
          const erros = value.error.errors;
          erros.forEach((erro: string) => {
            this.utilsServico.showNotificacao(erro);
          });
        }
      });
    } catch (erro) {
      this.utilsServico.showNotificacao(`Erro ao logar. Detalhes: ${erro}`);
    }
  }

  private getKey() {
    let value = this.form.get('login')?.value.substring(0,5);
    let key = '';
    for (let index = value.length; index >= 0; index--)
      key += value.substring(index,index - 1);      
    return key;
  }
}