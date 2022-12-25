import { Injectable } from '@angular/core';
import { UtilsService } from '../core/utils.service';
import { Token } from '../models/token.model';

// const keyCrypt = "credenciais@2022#12";

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {  

  constructor(
    private utilsServico: UtilsService,
  ) { }

  public saveData(key: string, value: string) {
    localStorage.setItem(key, this.utilsServico.encrypt(value));
  }

  public getData(key: string) {
    return this.utilsServico.decrypt(localStorage.getItem(key) || '');
  }

  public getToken(key: string): Token {
    let token!: Token;
    let result = this.utilsServico.decrypt(localStorage.getItem(key) || '');
    if (result != '')
      token = JSON.parse(result) as Token;
    return token;
  }
  
  public removeData(key: string) {
    localStorage.removeItem(key);
  }

  public clearData() {
    localStorage.clear();
  }

  // private encrypt(txt: string): string {
  //   return CryptoJS.AES.encrypt(txt, keyCrypt).toString();
  // }

  // private decrypt(txtToDecrypt: string) {
  //   return CryptoJS.AES.decrypt(txtToDecrypt, keyCrypt).toString(CryptoJS.enc.Utf8);
  // }
}