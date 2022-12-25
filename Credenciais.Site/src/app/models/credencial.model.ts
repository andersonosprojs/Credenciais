import { Model } from "../core/model";
import { Usuario } from "./usuario.model";

export class Credencial extends Model {
    titulo?: string;
    login?: string;
    senha?: string;
    assinatura?: string;
    url?: string;
    agencia?: string;
    conta?: string;
    pix?: string;
    usuarioAplicativo?: string;
    senhaAplicativo?: string;
    senhaCartao?: string;
    observacao?: string;
    idUsuario?: number;
    usuario?: Usuario;
}
