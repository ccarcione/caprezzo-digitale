import { Injectable, OnDestroy, ElementRef } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShellService {
  data: ShellData = {};
  private isForceOpen = new BehaviorSubject<boolean>(null);
  isForceOpen$ = this.isForceOpen.asObservable();
  register(component: OnDestroy & ShellData) {
    for (const p in component) {
      if (component.hasOwnProperty(p)) {
        this.data[p] = component[p];
      }
    }

    const od = component.ngOnDestroy.bind(component);
    component.ngOnDestroy = () => { od(); this.unregister(); };
  }

  unregister() { this.data = {}; }

  forceCloseDrawer() {
    this.isForceOpen.next(false);
  }

  resetBehaviorOpenProperyDrawer() {
    this.isForceOpen.next(null);
  }
}

export interface ShellData {
  title?: ElementRef;
  titleContainer?: ElementRef;
  search?: ElementRef;
  actions?: ElementRef;
  menu?: ElementRef;
  nav?: ElementRef;
  drawer?: ElementRef;
}