export class Feedback {
    guid: string;
    nome: string;
    messaggio: string;
    rating: number;
    
    constructor(data?: Partial<Feedback>) {
        Object.assign(this, data);
    }
}