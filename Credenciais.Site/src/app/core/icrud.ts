import { Observable } from "rxjs";

export interface ICrud<T> {
    listar(): Observable<T[]>;
    selecionar(id: number): Observable<T>;
    salvar(data: T): Observable<T>;
    excluir(id: number): Observable<T>;
}
