import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private isLoadingSub = new BehaviorSubject<boolean>(false);
  isLoading$ = this.isLoadingSub.asObservable();

  constructor() { }

  setStatusLoadingApp(status: boolean) {
    // setTimeout per evitare problemi di -->
    // ExpressionChangedAfterItHasBeenCheckedError: Expression has changed after it was checked.
    setTimeout(() => {
      this.isLoadingSub.next(status);
    });
  }

}
