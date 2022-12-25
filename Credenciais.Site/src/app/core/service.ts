import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { LocalStorageService } from "../services/local-storage.service";
import { BaseService, enToken } from "./base.service";
import { ICrud } from "./icrud";
import { Model } from "./model";

export abstract class Service<T extends Model> extends BaseService implements ICrud<T> {

    endpoint?: string;

    constructor(
        localStorageServico: LocalStorageService,
        http: HttpClient
    ) {
        super(localStorageServico, http);
    }
        
    listar(): Observable<T[]> {
        return this.list(`${this.endpoint}/listar`, enToken.Send);
    }
    selecionar(id: number): Observable<T> {
        return this.get(`${this.endpoint}/selecionar/${id}`, enToken.Send);        
    }
    salvar(data: T): Observable<T> {
        return this.post(`${this.endpoint}/salvar`, data, enToken.Send);
    }
    excluir(id: number): Observable<T> {
        return this.delete(`${this.endpoint}/excluir/${id}`, enToken.Send);
    }
}
