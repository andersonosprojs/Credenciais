import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UtilsService } from 'src/app/core/utils.service';
import { Credencial } from 'src/app/models/credencial.model';
import { Token } from 'src/app/models/token.model';
import { CredencialService } from 'src/app/services/credencial.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-credencial',
  templateUrl: './credencial.component.html',
  styleUrls: ['./credencial.component.scss']
})
export class CredencialComponent implements OnInit {
  // @Input() cadastroAtivo?: boolean;
  @Input() credencial?: Credencial;
  @Output() evFecharCadastro: EventEmitter<boolean> = new EventEmitter();

  // router?: Router;
  token?: Token;

  form!: FormGroup; 

  constructor(
    // router: Router,
    private localStorageServico: LocalStorageService,
    private utilsServico: UtilsService,
    private credencialServico: CredencialService,
    private fb: FormBuilder
  ) { 
    // this.router = router;
  }

  ngOnInit(): void {
    const jsonToken = this.localStorageServico.getData('token');
    if (jsonToken != '')
      this.token = JSON.parse(jsonToken) as Token;
      
    // if (this.token == undefined || this.token == null) {
    //   // this.router?.navigate(['/login']);
    //   return
    // }

    if (this.credencial != undefined) {
      this.preencherForm()
      return
    }

    this.form = this.fb.group({ 
      id: new FormControl(''),
      titulo: new FormControl('', [Validators.required]),
      login: new FormControl('', [Validators.required]),
      senha: new FormControl('', [Validators.required]),
      assinatura: new FormControl(''),
      url: new FormControl(''),
      agencia: new FormControl(''),
      conta: new FormControl(''),
      pix: new FormControl(''),
      usuarioAplicativo: new FormControl(''),
      senhaAplicativo: new FormControl(''),
      senhaCartao: new FormControl(''),
      observacao: new FormControl(''),
      idUsuario: new FormControl(''),
    });
  }

  private preencherForm() {
    this.form = this.fb.group({ 
      id: new FormControl(this.credencial?.id),
      titulo: new FormControl(this.credencial?.titulo, [Validators.required]),
      login: new FormControl(this.credencial?.login, [Validators.required]),
      senha: new FormControl(this.credencial?.senha, [Validators.required]),
      assinatura: new FormControl(this.credencial?.assinatura),
      url: new FormControl(this.credencial?.url),
      agencia: new FormControl(this.credencial?.agencia),
      conta: new FormControl(this.credencial?.conta),
      pix: new FormControl(this.credencial?.pix),
      usuarioAplicativo: new FormControl(this.credencial?.usuarioAplicativo),
      senhaAplicativo: new FormControl(this.credencial?.senhaAplicativo),
      senhaCartao: new FormControl(this.credencial?.senhaCartao),
      observacao: new FormControl(this.credencial?.observacao),
      idUsuario: new FormControl(this.credencial?.idUsuario),
    });
  }

  salvar() {
    if (!this.form.valid) {
      this.utilsServico.showNotificacao('Não foi possível salvar dos dados');
      return
    }

    const id = this.form.get('id')?.value == ''? 0: this.form.get('id')?.value;
    const idUsuario = this.form.get('idUsuario')?.value == ''? this.token?.idUsuario: this.form.get('idUsuario')?.value;
    const credencial: Credencial = {
      id: id,
      titulo: this.form.get('titulo')?.value,            
      login: this.form.get('login')?.value,            
      senha: this.form.get('senha')?.value,            
      assinatura: this.form.get('assinatura')?.value,            
      url: this.form.get('url')?.value,            
      agencia: this.form.get('agencia')?.value,            
      conta: this.form.get('conta')?.value,            
      pix: this.form.get('pix')?.value,            
      usuarioAplicativo: this.form.get('usuarioAplicativo')?.value,            
      senhaAplicativo: this.form.get('senhaAplicativo')?.value,            
      senhaCartao: this.form.get('senhaCartao')?.value,            
      observacao: this.form.get('observacao')?.value,            
      idUsuario: idUsuario,            
    }

    this.credencialServico.salvar(credencial).subscribe({
      next: () => {
        this.utilsServico.showNotificacao('Credencial salva com sucesso!');
        this.voltar();
        // this.router?.navigate(['']);
      },
      error: (value: any) => this.utilsServico.showNotificacao(value.error),
    });            
  }

  voltar() {
    // this.cadastroAtivo = false;
    this.evFecharCadastro.emit(false);
    // this.router?.navigate(['/menu']);
  }


}
