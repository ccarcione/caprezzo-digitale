import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Messaggio } from './model/messaggio';
import { switchMap } from 'rxjs/operators';

const url = "api/Messaggio";

@Injectable({
  providedIn: 'root'
})

export class MessageService {
  private messageSub = new BehaviorSubject<Messaggio[]>([]);

  messages$ = this.initMessageHub();

  initMessageHub() {
    return this.http.get<Messaggio[]>(url).pipe(
      switchMap(messages => {
        messages = messages.map(message => {
          let m = new Messaggio(message);
          m.allegati.forEach(a => a.filePath = a.filePath ? `${location.origin}/${a.filePath}` : '')
          return m;
        });
        this.messageSub.next(messages);
        return this.messageSub.asObservable();
      })
    );
  }


  constructor(private http: HttpClient) { }

  async getById(id: number): Promise<Messaggio> {
    let message =await this.http.get<Messaggio>(url + '/'+ id).toPromise();
    return new Messaggio(message);

  }
}
