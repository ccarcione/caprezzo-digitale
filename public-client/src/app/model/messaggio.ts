import { TipoMessaggio } from 'src/app/model/tipo-messaggio';
import { Allegato } from 'src/app/model/allegato';

export class Messaggio {
    id: number;
    titolo:string;
    sottotitolo:string;
    dataPubblicazione: Date;
    urlImmagine: string;
    testo:string;
    tipologiaPostId: number;
    tipoMessaggio: TipoMessaggio;
    allegati:Allegato[];
    
    showMore:boolean=false;

    constructor(data?: Partial<Messaggio>) {
        Object.assign(this, data);
    }
}