import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enToken } from '../core/base.service';
import { Service } from '../core/service';
import { Credencial } from '../models/credencial.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class CredencialService extends Service<Credencial> {
  constructor(
    localStorageServico: LocalStorageService, 
    httpClient: HttpClient
  ) {
    super(localStorageServico, httpClient);
    this.endpoint = 'credencial';
  }

  listarPorUsuario(idusuario: number): Observable<any> {
    return this.get(`${this.endpoint}/listar/${idusuario}`, enToken.Send);
  } 
}
