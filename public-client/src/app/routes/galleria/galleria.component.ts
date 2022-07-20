import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { PageInfo } from '@shared/models/pagination-result';
import { GalleriaService } from './galleria.service';

@Component({
  selector: 'app-galleria',
  templateUrl: './galleria.component.html',
  styleUrls: ['./galleria.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GalleriaComponent implements OnInit {

  pagination: PageInfo = new PageInfo();
  imagesData = [];
  pageSize = 15;

  constructor(private galleriaService: GalleriaService) { }

  async ngOnInit() {
    this.pagination.currentPage = 1;
    this.pagination.pageSize = this.pageSize;
    const paginationResult = await this.galleriaService.getGalleria(this.pagination);
    // this.imagesData = paginationResult.data.map(x => new Media(x, ''));
    this.pagination = paginationResult?.pagination as PageInfo;

    console.info(paginationResult?.data);
  }

}
