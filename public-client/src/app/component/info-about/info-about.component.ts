import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/layout/loading/loading.service';
import { VersionJson, AssetsService } from 'src/app/assets.service';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

@Component({
  selector: 'app-info-about',
  templateUrl: './info-about.component.html',
  styleUrls: ['./info-about.component.css']
})
export class InfoAboutComponent implements OnInit, OnDestroy, ShellData {

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;
  versionJson: VersionJson = new VersionJson();
  creditsList: Array<string[]> = [
    ['flaticon', 'https://media.flaticon.com/license/license.pdf?_gl=1%5C*768uvi%5C*_ga%5C*ODc0NzQxNzMyLjE2MjQ4NzE5ODM.%5C*_ga_3Q8LH3P0VP%5C*MTYyNDg3MTk4Mi4xLjEuMTYyNDg3NjM5MS4w&_ga=2.162352316.1002056635.1624871983-874741732.1624871983', 'https://www.flaticon.com/'],
    ['Angular CLI', 'https://raw.githubusercontent.com/angular/angular-cli/master/LICENSE', 'https://github.com/angular/angular-cli#readme'],
    ['Material Angular', 'https://raw.githubusercontent.com/angular/components/master/LICENSE', 'https://github.com/angular/components#readme'],
    ['Material Design Icons', 'https://raw.githubusercontent.com/Templarian/MaterialDesign/master/LICENSE', 'https://github.com/Templarian/MaterialDesign#readme'],
    ['icon-icons.com', 'https://opensource.org/licenses/mit-license.php', 'https://icon-icons.com/'],
    ['icon-icons.com', 'https://icon-icons.com/search/icons/license', 'https://icon-icons.com/'],
    ['crypto-js', 'https://raw.githubusercontent.com/brix/crypto-js/develop/LICENSE', 'https://github.com/brix/crypto-js#readme'],
    ['Css Star Rating', 'https://raw.githubusercontent.com/BioPhoton/css-star-rating/master/LICENSE', 'https://github.com/BioPhoton/css-star-rating#readme'],
    ['ngx-masonry', 'https://raw.githubusercontent.com/wynfred/ngx-masonry/master/LICENSE', 'https://github.com/wynfred/ngx-masonry#readme'],
    ['Opengallery', 'https://raw.githubusercontent.com/RLoris/lib-ng-opengallery/master/LICENSE', 'https://github.com/RLoris/lib-ng-opengallery#readme'],
    ['Internet Connection Monitoring Service', 'https://raw.githubusercontent.com/yildiraymeric/ngx-connection-service/develop/LICENSE', 'https://github.com/yildiraymeric/ngx-connection-service#readme'],
    ['ngx-extended-pdf-viewer', 'https://raw.githubusercontent.com/stephanrauh/ngx-extended-pdf-viewer/main/LICENSE', 'https://github.com/stephanrauh/ngx-extended-pdf-viewer#readme'],
    ['masonry-layout', 'https://desandro.mit-license.org/', 'https://github.com/desandro/masonry#readme'],
    ['ngx-toastr', 'https://raw.githubusercontent.com/scttcper/ngx-toastr/master/LICENSE', 'https://github.com/scttcper/ngx-toastr#readme'],
    ['rxjs', 'https://raw.githubusercontent.com/ReactiveX/rxjs/master/LICENSE.txt', 'https://github.com/reactivex/rxjs#readme'],
  ].sort((a, b) => (a > b ? 1 : -1));

  constructor(private loadingService: LoadingService,
    private assetsService: AssetsService,
    private ss: ShellService) { }

  ngOnDestroy(): void {
    this.ss.unregister();
  }

  async ngOnInit(): Promise<void> {
    this.ss.register(this);
    this.versionJson = await this.assetsService.getVersion();
    
    this.loadingService.setStatusLoadingApp(false);
  }
}
