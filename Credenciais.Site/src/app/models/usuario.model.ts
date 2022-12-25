import { Credencial } from "./credencial.model";
import { Model } from "../core/model";

export class Usuario extends Model {
    Login?: string;
    Senha?: string;
    Credenciais?: Credencial[]
}
