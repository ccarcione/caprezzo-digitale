import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { BehaviorSubject, switchMap } from 'rxjs';
import { Messaggio } from './models/messaggio';

const url = environment.apiUrl + "/api/bacheca";

@Injectable({
  providedIn: 'root'
})
export class BachecaService {
  private messageSub = new BehaviorSubject<Messaggio[]>([]);

  messages$ = this.initMessageHub();

  initMessageHub() {
    return this.http.get<Messaggio[]>(url).pipe(
      switchMap(messages => {
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
