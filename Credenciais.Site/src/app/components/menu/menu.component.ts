import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UtilsService } from 'src/app/core/utils.service';
import { Token } from 'src/app/models/token.model';
import { Usuario } from 'src/app/models/usuario.model';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { UsuarioService } from 'src/app/services/usuario.service';
import { ConfirmaComponent } from '../confirma/confirma.component';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  
  titulo: string = 'Credenciais';
  router?: Router;
  rootUrl?: string;
  token?: Token;

  constructor(
    router: Router,
    public dialog: MatDialog,
    private utilsServico: UtilsService,
    private usuarioServico: UsuarioService,
    private localStorageServico: LocalStorageService,
    public http: HttpClient
  ) { 
    this.router = router;
  }

  ngOnInit(): void {
    this.rootUrl = window.location.href.replace('menu', '');
    const jsonToken = this.localStorageServico.getData('token');
    if (jsonToken != '')
      this.token = JSON.parse(jsonToken) as Token;
      
    if (this.token == undefined || this.token == null) {
      this.router?.navigate(['/login']);
      return
    }
  }

  public logout(): void {
    this.localStorageServico.removeData('token');
    this.token = undefined;
    this.router?.navigate(['/login']);
  }

  alterarSenha() {
    try {
      const token = this.token?.login || '';
      this.router?.navigate(
        ['/redefinirsenha'], { 
          queryParams: { 
            value: this.utilsServico.encrypt(token), 
            key: this.utilsServico.encrypt(this.getKey(token.substring(0,5))),
            key2: this.utilsServico.encrypt('alter')
          }
        }
      );      
    } catch (erro) {
      this.utilsServico.showNotificacao(`Erro ao alterar senha. Detalhes: ${erro}`);
    }
  }

  private getKey(value: string) {
    let key = '';
    for (let index = value.length; index >= 0; index--)
      key += value.substring(index,index - 1);      
    return key;
  }


  excluirUsuario() {
    let mensagem: string[] = [];
    mensagem.push('A exclusão do seu usuário será definitiva. Não podendo de');
    mensagem.push('forma alguma ser realiza a recuperação. Serão');
    mensagem.push('excluídas também todas as credenciais relacionadas.');
    mensagem.push('');
    mensagem.push('Desseja realmente prosseguir com a exclusão?');
    
    const dialogRef = this.dialog.open(ConfirmaComponent);
    dialogRef.componentInstance.title = `Usuário: ${this.token?.login}`;
    dialogRef.componentInstance.messages = mensagem;
    dialogRef.componentInstance.btnPrimary = 'Não';
    dialogRef.componentInstance.btnSecundary = 'Sim';

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        this.usuarioServico.selecionarPorLogin(this.token?.login || '').subscribe({
          next: (value: Usuario) => {
            this.usuarioServico.excluir(value.id || 0).subscribe({
              next: () => {
                this.utilsServico.showNotificacao('Usuário excluído com sucesso');
                this.logout();
              }
            })
          },
          error: value => console.log(value)
        });
      }
    });
  }  

}
