import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageInfo, PaginationResult } from './model/pagination-result';

const url = "api/Galleria";

@Injectable({
  providedIn: 'root'
})
export class GalleriaService {

  constructor(private http: HttpClient) { }

  async getGalleria(paginazione: PageInfo) {
    let response = await this.http.get<PaginationResult<PaginationResult<String[]>>>(
        url + `?page=${paginazione.currentPage}&pageCount=${paginazione.pageSize}`)
      .toPromise();
    response?.data?.map(m => `${location.origin}/${String(m)}`);
    return response;
  }
}
