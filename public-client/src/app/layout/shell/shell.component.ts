import { Component, OnInit, ContentChild, ElementRef } from '@angular/core';
import { combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {ShellService} from './shell.service'
import { MatDrawer } from '@angular/material/sidenav';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { ShellMenuDirective } from './shell-menu.directive';
import { VersionJson, AssetsService } from '../../assets.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit {
  versionJson: VersionJson = new VersionJson();
  openPropertyDrawer: boolean;
  isForceOpenDrawer: boolean;
  
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

    
  @ContentChild(ShellMenuDirective, {static: true}) fallbackMenu;
  get hasMenu(): boolean { return this.menu || this.fallbackMenu; }
  get menu(): ElementRef { return this.shellService.data.menu; }
  get title(): ElementRef { return this.shellService.data.title; }
  get titleContainer(): ElementRef { return this.shellService.data.titleContainer; }
  get actions(): ElementRef { return this.shellService.data.actions; }
  get search(): ElementRef { return this.shellService.data.search; }
  get nav(): ElementRef { return this.shellService.data.nav; }
  constructor(private breakpointObserver: BreakpointObserver, private shellService: ShellService,
    private assetsService: AssetsService, private router: Router) {
      // questo lo faccio perchè dio bono le pwa su apple device fanno cagare!!!
      // se l'app viene installata da una pagina diversa da quella di atterraggio
      // questa non riesce a caricarsi alla homepage (perchè l'app viene installata
      // con il segmento della pagina).
      this.router.navigateByUrl('/');
    }
    
    async ngOnInit() {
      this.shellService.isForceOpen$.subscribe(x => this.isForceOpenDrawer = x);
      this.versionJson = await this.assetsService.getVersion();
  }

  onNavClick(drawer: MatDrawer) {
    if (drawer.mode == 'over') {
      drawer.close();
    }
  }

  marginTopAppleDevice() {
    if (this.isIos() && this.isInStandaloneMode()) {
      return 'margin-top-apple-device';
    }
    return '';
  }

  // Detects if device is on iOS 
  isIos() {
    const userAgent = window.navigator.userAgent.toLowerCase();
    return /iphone|ipad|ipod/.test( userAgent );
  }
  // Detects if device is in standalone mode
  isInStandaloneMode() {
    return ('standalone' in window.navigator) && (window.navigator['standalone']);
  }
}
