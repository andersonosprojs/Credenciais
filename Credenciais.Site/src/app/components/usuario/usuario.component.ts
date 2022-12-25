import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UtilsService } from 'src/app/core/utils.service';
import { Usuario } from 'src/app/models/usuario.model';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent implements OnInit {
  hideSenha: boolean = true;
  hideConfirmarSenha: boolean = true;
  router?: Router;

  form!: FormGroup; 

  constructor(
    router: Router,
    private atenticacaoServico: AutenticacaoService,
    private utilsServico: UtilsService,
    private fb: FormBuilder
  ) { 
    this.router = router;
  }

  ngOnInit(): void {
    this.form = this.fb.group({ 
      login: new FormControl('', [Validators.required, Validators.email]),
      senha: new FormControl('', [Validators.required]),
      confirmarsenha: new FormControl('', [Validators.required])
    });
  }

  cadastrar() {
    if (!this.form.valid) {
      this.utilsServico.showNotificacao('Não foi possível cadastrar o usuário');
      return
    }

    if (this.form.get('senha')?.value != this.form.get('confirmarsenha')?.value) {
      this.utilsServico.showNotificacao('As senhas estão diferentes');
      return      
    }

    const usuario: Usuario = {
      Login: this.form.get('login')?.value,
      Senha: this.form.get('senha')?.value,            
    }

    this.atenticacaoServico.cadastrarUsuaro(usuario).subscribe({
      next: () => {
        this.utilsServico.showNotificacao('Usuário cadastrado com sucesso!');
        this.router?.navigate(['']);
      },
      error: (value: any) => this.utilsServico.showNotificacao(value.error),
    });            
  }

}
