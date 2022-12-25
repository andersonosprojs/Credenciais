import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enToken } from '../core/base.service';
import { Service } from '../core/service';
import { Usuario } from '../models/usuario.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService extends	Service<Usuario> {
	
  constructor(
    localStorageServico: LocalStorageService,    
    httpClient: HttpClient
  )	{
    super(localStorageServico, httpClient);
    this.endpoint = 'usuario';
  }

  selecionarPorLogin(login: string): Observable<any> {
    return this.get(`${this.endpoint}/selecionarporlogin/${login}`, enToken.Send);
  }  

  alterarSenha(data: Usuario): Observable<any> {
    return this.post(`${this.endpoint}/alterarsenha`, data, enToken.Send);
  }
}
