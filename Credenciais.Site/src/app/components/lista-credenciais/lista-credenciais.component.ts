import { Component, DoCheck } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { UtilsService } from 'src/app/core/utils.service';
import { Credencial } from 'src/app/models/credencial.model';
import { Token } from 'src/app/models/token.model';
import { CredencialService } from 'src/app/services/credencial.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { ConfirmaComponent } from '../confirma/confirma.component';

const LIST_COLUMNS: string[] = [
  'titulo',
  'login',
  'senha',
  'assinatura',
  'agencia',
  'conta',
  'pix',
  'acoes',
];

@Component({
  selector: 'app-lista-credenciais',
  templateUrl: './lista-credenciais.component.html',
  styleUrls: ['./lista-credenciais.component.scss']
})
export class ListaCredenciaisComponent implements DoCheck {
  token!: Token;
  listarCredenciais: boolean = true;
  cadastroAtivo: boolean = false;
  credencial?: Credencial;

  dataSource: MatTableDataSource<Credencial> = new MatTableDataSource<Credencial>([]);
  displayedColumns = LIST_COLUMNS;

  constructor(
    public dialog: MatDialog,
    private localStorageServico: LocalStorageService,
    private credencialServico: CredencialService,
    private utilsServico: UtilsService,
  ) { }

  ngOnInit(): void { }

  ngDoCheck() {
    if (this.listarCredenciais) {
      const token = this.localStorageServico.getToken('token');
      if (token && token.idUsuario && token.idUsuario > 0) {        
        this.credencialServico.listarPorUsuario(token.idUsuario || 0).subscribe({
          next: value => {
            this.listarCredenciais = false;
            this.dataSource.data = value
          },
          error: value => console.log(value)
        });  
      }
    }
  }

  alterar(value: Credencial) {
    this.credencial = value;
    this.cadastroAtivo = true;
  }
  
  excluir(value: Credencial) {
    if (value) {
      let mensagem: string[] = [];
      mensagem.push('Deseja excluir essa credencial?');
      
      const dialogRef = this.dialog.open(ConfirmaComponent);
      dialogRef.componentInstance.title = `Credencial: ${value.titulo}`;
      dialogRef.componentInstance.messages = mensagem;
      dialogRef.componentInstance.btnPrimary = 'Sim';
      dialogRef.componentInstance.btnSecundary = 'Não';
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.credencialServico.excluir(value.id || 0).subscribe({
            next: value => {
              this.utilsServico.showNotificacao('Credencial excluída');
              this.listarCredenciais = true;
            },
            error: value => console.log(value)
          })    
        }
      });
    }
  }

  filtrar(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ativarCadastro() {
    this.credencial = undefined;
    this.cadastroAtivo = true;
  }

  onFecharCadastro(event: any) {
    this.cadastroAtivo = event;
    this.listarCredenciais = true;
  }
}
