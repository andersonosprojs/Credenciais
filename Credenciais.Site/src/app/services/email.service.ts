import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enToken } from '../core/base.service';
import { Service } from '../core/service';
import { Email } from '../models/email.model';
import { Usuario } from '../models/usuario.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class EmailService extends	Service<Usuario> {
	
  constructor(
    localStorageServico: LocalStorageService,    
    httpClient: HttpClient
  )	{
    super(localStorageServico, httpClient);
    this.endpoint = 'email';
  }

  enviarEmail(data: Email): Observable<any> {
    return this.post(`email/enviaremail`, data, enToken.NoSend);
  }  
}
