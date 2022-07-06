export interface AppSettings {
  navPos: 'side' | 'top';
  theme: 'light' | 'dark';  // always in automatic mode based on the time
  dir: 'ltr' | 'rtl';
  showHeader: boolean;
  headerPos: 'fixed' | 'static' | 'above';
  showUserPanel: boolean;
  sidenavOpened: boolean;
  sidenavCollapsed: boolean;
  language: string;
}

export const defaults: AppSettings = {
  navPos: 'side',
  theme: 'light',
  dir: 'ltr',
  showHeader: true,
  headerPos: 'fixed',
  showUserPanel: false,
  sidenavOpened: true,
  sidenavCollapsed: false,
  language: 'it-IT',
};
