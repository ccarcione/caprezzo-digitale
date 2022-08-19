import { Allegato } from "./allegato";
import { TipoMessaggio } from "./tipo-messaggio";

export class Messaggio {
    id!: number;
    titolo!: string;
    sottotitolo!: string;
    dataPubblicazione!: Date;
    urlImmagineCopertina!: string;
    urlPdfImmagineCopertina!: string;
    testo!: string;
    tipologiaPostId!: number;
    tipoMessaggio!: TipoMessaggio;
    allegati!: Allegato[];

    constructor(data?: Partial<Messaggio>) {
        Object.assign(this, data);
    }
}
