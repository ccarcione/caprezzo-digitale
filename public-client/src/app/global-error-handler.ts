import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { LogService } from './log.service'

import { HttpErrorResponse } from "@angular/common/http";
import { ToastrService } from "ngx-toastr";
import { take } from "rxjs/operators";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  private toastrService:ToastrService;
  constructor(
    private logService: LogService,
    private injector: Injector
  ) { }

  handleError(error: any) {
    this.toastrService = this.injector.get(ToastrService);
    if (error.rejection instanceof HttpErrorResponse) {
      if (error.status === 401 || error.status === 403) {
        this.toastrService.error('', 'Operazione non autorizzata');
        //.open('Operazione non autorizzata', 'Ok', { duration: 1000 });
      } else {

        this.toastrService
        .error('Qualcosa non ha funzionato, ma non preoccuparti, ricarica la pagina :)', 'OPS')
        .onTap
        .pipe(take(1))
        .subscribe(() =>location.reload());
      }
    } else {
      const err = {
        message: error.message ? error.message : error.toString(),
        stack: error.stack ? error.stack : '',
        name: error.name
      };

      // Optionally send it to your back-end API
      this.logService.logError(err);

      // Notify the user
      this.toastrService.error('Qualcosa non ha funzionato, ma non preoccuparti, ricarica la pagina :)', 'OPS');
    }


  }
}


