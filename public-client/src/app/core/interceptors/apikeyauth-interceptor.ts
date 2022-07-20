import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http'
import { Observable } from 'rxjs';
import * as CryptoJS from 'crypto-js';
import { environment } from '@env/environment';

@Injectable()
export class ApiKeyAuthInterceptor implements HttpInterceptor {
  // the number of .net ticks at the unix epoch
  epochTicks: number = 621355968000000000;
  // there are 10000 .net ticks per millisecond
  ticksPerMillisecond: number = 10000;
  clientName: string = environment.ApiKeyClientName;

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>> {
    const s = req.url.toLocaleLowerCase();
    if (s.indexOf(environment.ApiKeyPath) === -1) {
        return next.handle(req);
    }

    // calculate the total number of .net ticks for your date
    let ticks: string = (this.epochTicks + ((new Date().getTime()) * this.ticksPerMillisecond)).toString();
    // calculate hash secret
    let secret: string = CryptoJS.HmacSHA512(ticks.concat(this.clientName), environment.ApiKey).toString();
    // add custom header
    const authReq = req.clone({
        headers: req.headers
            .set('Client-Secret', secret)
            .set('Client-Date', ticks)
            .set('Client-Name', this.clientName)
        });

    return next.handle(authReq);
  }
}
