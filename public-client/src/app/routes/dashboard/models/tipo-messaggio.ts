export class TipoMessaggio {
  id!: number;
  displayName!: string;
  descrizione!: string;
  icona!: string;
  colore!: string;

  constructor(data?: Partial<TipoMessaggio>) {
      Object.assign(this, data);
  }
}
