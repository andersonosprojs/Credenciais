import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UtilsService } from 'src/app/core/utils.service';
import { Usuario } from 'src/app/models/usuario.model';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-redefinirsenha',
  templateUrl: './redefinirsenha.component.html',
  styleUrls: ['./redefinirsenha.component.scss']
})
export class RedefinirsenhaComponent implements OnInit {
  
  hideSenha: boolean = true;
  hideConfirmarSenha: boolean = true;
  login?: string;
  router?: Router;
  key2?: string; 
  title: string = 'Redefinição de Senha';
  labelBtn: string = 'Redefinir Senha';

  form!: FormGroup; 

  constructor(
    private fb: FormBuilder,
    private actRoute: ActivatedRoute,
    router: Router,
    private utilsServico: UtilsService,
    private localStorageServico: LocalStorageService,
    private autenticacaoServico: AutenticacaoService,
    private usuarioServico: UsuarioService
  ) { 
    this.router = router;
  }

  ngOnInit(): void {
    this.actRoute.queryParams.subscribe(params => {
      this.login = this.utilsServico.decrypt(params['value'].replaceAll(' ', '+'));
      const key = this.getKey(this.utilsServico.decrypt(params['key'].replaceAll(' ', '+')));
      try {
        this.key2 = this.utilsServico.decrypt(params['key2'].replaceAll(' ', '+'));  
      } catch (error) {
        this.key2 = '';
      }
      
      
      if (this.key2 == 'alter')
      {
        this.title = 'Alteração de Senha';
        this.labelBtn = 'Alterar Senha'
      }

      if ((this.login == undefined || this.login == '') || 
          (key == undefined || key == '') || 
          (this.login.substring(0,5) != key)) {
        this.utilsServico.showNotificacao('Não foi possível reconhecer o link informado');
        this.router?.navigate(['']);
        return      
      }
    });    

    this.form = this.fb.group({ 
      senha: new FormControl('', [Validators.required]),
      confirmarsenha: new FormControl('', [Validators.required])
    });
  }

  private getKey(value: string) {
    let key = '';
    for (let index = value.length; index >= 0; index--)
      key += value.substring(index,index - 1);      
    return key;
  }

  redefinirSenha() {

    if (!this.form.valid) {
      this.utilsServico.showNotificacao('Senha e Confirmação de senha são obrigatórios');
      return
    }

    if (this.form.get('senha')?.value != this.form.get('confirmarsenha')?.value) {
      this.utilsServico.showNotificacao('As senhas estão diferentes');
      return      
    }

    const usuario: Usuario = {
      Login: this.login,
      Senha: this.form.get('senha')?.value,      
    }

    if (this.key2 == 'alter') {
      this.usuarioServico.alterarSenha(usuario).subscribe({
        next: (value: any) => {
          this.utilsServico.showNotificacao('Senha alterado com sucesso!');
          this.localStorageServico.removeData('token');            
          this.router?.navigate(['']);
        },
        error: (value: any) => console.log(value),
      });  
    }
    else {
      this.autenticacaoServico.redefinirSenha(usuario).subscribe({
        next: (value: any) => {
          this.utilsServico.showNotificacao('Senha redefinida com sucesso!');            
          this.router?.navigate(['']);
        },
        error: (value: any) => console.log(value),
      });              
    }
  }

}
