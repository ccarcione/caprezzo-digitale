import { Component } from '@angular/core';

@Component({
  selector: 'app-branding',
  template: `
    <a class="matero-branding" href="/">
      <img src="./assets/images/icon-original.png" class="matero-branding-logo-expanded" alt="logo" />
      <span class="matero-branding-name" style="font-weight: bold;">Caprezzo Digitale</span>
    </a>
  `,
})
export class BrandingComponent {}
