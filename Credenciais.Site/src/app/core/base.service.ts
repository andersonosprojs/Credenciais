import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Token } from '../models/token.model';
import { LocalStorageService } from '../services/local-storage.service';

const _httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE',
    'Access-Control-Allow-Headers':
      'Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers'
  })
};

export enum enToken {
  Send,
  NoSend,
}

@Injectable({
  providedIn: 'root',
})
export class BaseService {
  private url: string = environment.apiUrl;
  private token!: Token;

  constructor(
    private localStorageServico: LocalStorageService,
    private http: HttpClient
  ) {
    const jsonToken = this.localStorageServico.getData('token');
    if (jsonToken != '')
      this.token = JSON.parse(jsonToken) as Token;
  }

  SetUrl(url: string) {
    this.url = url;
  }

  getToken() {
    return this.token.token || '';
  }

  getLogin() {
    return this.token.login || '';
  }

  getHeaders() {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.getToken()}`
    });
  }

  list<T>(rota: string, token: enToken): Observable<T[]> {
    if (token == enToken.Send)
      return this.http.get(`${this.url}${rota}`, { headers: this.getHeaders() }) as Observable<T[]>;
    else 
      return this.http.get(`${this.url}${rota}`) as Observable<T[]>;
  }

  get<T>(rota: string, token: enToken): Observable<T> {
    if (token == enToken.Send)
      return this.http.get(`${this.url}${rota}`,{ headers: this.getHeaders() }) as Observable<T>;
    else 
      return this.http.get(`${this.url}${rota}`) as Observable<T>;
  }

  post<T>(rota: string, data?: T, token?: enToken): Observable<T> {
    if (token == enToken.Send)
      return this.http.post(`${this.url}${rota}`,data,{ headers: this.getHeaders() }) as Observable<T>;
    else
      return this.http.post(`${this.url}${rota}`,data) as Observable<T>;
  }

  put<T>(rota: string, data: T, token: enToken): Observable<T> {
    if (token == enToken.Send)
      return this.http.put(`${this.url}${rota}`, data,{ headers: this.getHeaders() }) as Observable<T>;
    else 
      return this.http.put(`${this.url}${rota}`, data) as Observable<T>;
  }

  delete<T>(rota: string, token: enToken): Observable<T> {
    if (token == enToken.Send)
      return this.http.delete(`${this.url}${rota}`, { headers: this.getHeaders() }) as Observable<T>;
    else
      return this.http.delete(`${this.url}${rota}`) as Observable<T>;
  }

  // deleteAll<T>(rota: string, headers?: HttpHeaders, body?: T ): Observable<T[]> {
  //   if (body != undefined) {
  //     if (headers != undefined)
  //       headers.append('Content-Type', 'application/json');
  //     else if (headers == undefined) {
  //       headers = new HttpHeaders({ 
  //         'Content-Type': 'application/json', 
  //       });
  //     }      
  //   }

  //   return this.http.delete(`${this.url}${rota}`, {
  //     headers: headers,
  //     body: body
  //   }) as Observable<T[]>;
  // }
}
