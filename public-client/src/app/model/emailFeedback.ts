export class EmailFeedback {
    nome: string;
    messaggio: string;
    rating: number;
    
    constructor(data?: Partial<EmailFeedback>) {
        Object.assign(this, data);
    }
}