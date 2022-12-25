import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as CryptoJS from 'crypto-js';

const keyCrypt = "credenciais@2022#12";

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor(
    private snackBar: MatSnackBar,
    public dialog: MatDialog,
  ) { }

  public encrypt(txt: string): string {
    return CryptoJS.AES.encrypt(txt, keyCrypt).toString();
  }

  public decrypt(txtToDecrypt: string) {
    return CryptoJS.AES.decrypt(txtToDecrypt, keyCrypt).toString(CryptoJS.enc.Utf8);
  }
  
  
  public rolarAte(
    documento: Document,
    chave: string, // Chave: Id, Classe, Elemento, etc.
    comportamento: ScrollBehavior = 'smooth' // Comportamento: Suave é o padrão.
  ) {
    documento.querySelector(chave)?.scrollIntoView({
      behavior: comportamento
    });
  }

  public tratarFormularioInvalido(
    fomulario: FormGroup,
    documento: Document,
    mensagem: string,
    chave: string,  // Chave: Id, Classe, Elemento, etc.
    comportamento: ScrollBehavior = 'smooth' // Comportamento: Suave é o padrão.
  ) {
    this.marcarComoTocado(fomulario);
    this.showNotificacaoFail(mensagem);
    this.rolarAte(document, chave, comportamento);
  }

  public marcarComoTocado(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      control!.markAsTouched({ onlySelf: true });
    });
  }

  public showNotificacao(mensagem: string, acao: string = 'OK', duracao: number = 3000) {
    this.snackBar.open(mensagem, acao, {
      duration: duracao,
      horizontalPosition: 'end',
      panelClass: ['css-snackbar'],
      verticalPosition: 'bottom',
    })
  }

  public showNotificacaoFail(mensagem: string, acao: string = 'OK', duracao: number = 3000) {
    this.snackBar.open(mensagem, acao, {
      duration: duracao,
      horizontalPosition: 'end',
      panelClass: ['css-snackbar-fail'],
      verticalPosition: 'bottom',
    })
  }

  public showNotificacaoOption(options: any) {

    this.snackBar.open(options.mensagem, options.acao, {
      duration: options.duracao,
      horizontalPosition: 'end',
      verticalPosition: 'bottom',
      ...options.snackoptions,
      panelClass: ['css-snackbar'],
    }).onAction().subscribe(() => options.metodoacao && options.metodoacao.call());

    options.callback && options.callback();
  }

  showNotification(message: string, callback?: Function, metodoAcao?: Function) {
    this.showNotificacaoOption({
      mensagem: message,
      acao: 'OK',
      duracao: 5000,
      metodoacao: metodoAcao,
      panelClass: ['css-snackbar'],
    });

    callback && callback();
  }

  public validaCpfCnpj(documento: string): boolean {
    documento = documento.replace(/\D/g, '');

    if (documento.length === 11) {
        return this.validaCpf(documento);
    } else if (documento.length === 14) {
        return this.validaCnpj(documento);
    } else {
        return false;
    }
  }

  private validaCpf(cpf: string) {
    cpf = cpf.replace(/\D+/g, '');
    if (
        !cpf ||
        cpf.length !== 11 ||
        cpf === '0'.repeat(11) ||
        cpf === '1'.repeat(11) ||
        cpf === '2'.repeat(11) ||
        cpf === '3'.repeat(11) ||
        cpf === '4'.repeat(11) ||
        cpf === '5'.repeat(11) ||
        cpf === '6'.repeat(11) ||
        cpf === '7'.repeat(11) ||
        cpf === '8'.repeat(11) ||
        cpf === '9'.repeat(11)
    ) {
        return false;
    }
    let soma = 0;
    let resto;
    for (let i = 1; i <= 9; i++) {
        soma = soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
    }
    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) {
        resto = 0;
    }
    if (resto !== parseInt(cpf.substring(9, 10))) {
        return false;
    }
    soma = 0;
    for (let i = 1; i <= 10; i++) {
        soma = soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
    }
    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) {
        resto = 0;
    }
    if (resto !== parseInt(cpf.substring(10, 11))) {
        return false;
    }
    return true;
  }

  private validaCnpj(cnpj: string) {
    cnpj = cnpj.replace(/\D+/g, '');
    if (
        !cnpj ||
        cnpj.length !== 14 ||
        cnpj === '0'.repeat(14) ||
        cnpj === '1'.repeat(14) ||
        cnpj === '2'.repeat(14) ||
        cnpj === '3'.repeat(14) ||
        cnpj === '4'.repeat(14) ||
        cnpj === '5'.repeat(14) ||
        cnpj === '6'.repeat(14) ||
        cnpj === '7'.repeat(14) ||
        cnpj === '8'.repeat(14) ||
        cnpj === '9'.repeat(14)
    ) {
        return false;
    }
    let tamanho = cnpj.length - 2;
    let numeros: any = cnpj.substring(0, tamanho);
    const digitos = cnpj.substring(tamanho);
    let soma = 0;
    let pos: number = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) {
            pos = 9;
        }
    }
    let resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado !== (+digitos.charAt(0))) {
        return false;
    }
    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) {
            pos = 9;
        }
    }
    resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado !== (+digitos.charAt(1))) {
        return false;
    }
    return true;
  }

  public validatorCpfCnpj(fc:  FormControl)  {

    if  (!fc.value)  return  false;

    if  (fc.value.replace(/\D+/g,  '')  ===  '00000000000'  ||  fc.value.replace(/\D+/g,  '')  ===  '000000000000000')  {
          return  false ;
    }  else  if  (fc.value.replace(/\D+/g,  '').length  ===  11)  {
          return this.validaCpf(fc.value)  ?  false  :  {  cpfCnpj:  true  };
    }  else if (fc.value.replace(/\D+/g,  '').length  ===  14)  {
          return this.validaCnpj(fc.value)  ?  false  :  {  cpfCnpj:  true  };
    }  else  {
          return  {  cpfCnpj:  true  };
    }
  }

  public ObjCompare(obj1: any, obj2: any){
    const Obj1_keys = Object.keys(obj1);
    const Obj2_keys = Object.keys(obj2);
    if (Obj1_keys.length !== Obj2_keys.length){
        return false;
    }
    for (let k of Obj1_keys){
        if(obj1[k] !== obj2[k]){
           return false;
        }
    }
    return true;
  }

}

