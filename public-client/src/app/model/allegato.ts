export class Allegato {
    id: number;
    messaggioId: string;
    descrizione: string;
    fileName: string;
    filePath: string;
    
    constructor(data?: Partial<Allegato>) {
        Object.assign(this, data);
    }
}