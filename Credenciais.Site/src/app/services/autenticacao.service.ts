import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enToken } from '../core/base.service';
import { Service } from '../core/service';
import { Login } from '../models/login.model';
import { Usuario } from '../models/usuario.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoService extends	Service<Usuario> {
	
  constructor(
    localStorageServico: LocalStorageService,    
    httpClient: HttpClient
  )	{
    super(localStorageServico, httpClient);
    this.endpoint = 'autenticacao';
  }

  cadastrarUsuaro(data: Usuario): Observable<Usuario> {
    return this.post(`${this.endpoint}/cadastrarusuario`, data);
  }

  login(data: Login): Observable<Login> {
    return this.post(`${this.endpoint}/login`, data, enToken.NoSend);
  }
  
  redefinirSenha(data: Usuario): Observable<Usuario> {
    return this.post(`${this.endpoint}/redefinirsenha`, data);
  }  
}
