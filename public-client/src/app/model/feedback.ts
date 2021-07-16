export class Feedback {
    nome: string;
    messaggio: string;
    rating: number;
    
    constructor(data?: Partial<Feedback>) {
        Object.assign(this, data);
    }
}