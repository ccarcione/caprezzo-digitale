import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { PageInfo, PaginationResult } from '@shared/models/pagination-result';

const url = environment.apiUrl + "/api/Galleria";

@Injectable({
  providedIn: 'root'
})
export class GalleriaService {

  constructor(private http: HttpClient) { }

  async getGalleria(paginazione: PageInfo) {
    let response = await this.http.get<PaginationResult<String[]>>(
        url + `?page=${paginazione.currentPage}&pageCount=${paginazione.pageSize}`)
      .toPromise();
    response?.data?.map(m => `${location.origin}/${String(m)}`);
    return response;
  }
}
