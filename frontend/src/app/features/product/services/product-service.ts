import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product, CreateProductRequest, UpdateProductRequest } from '../models/product.model';
import { ApiPaginatedResponse } from '../../../core/models/ApiPaginatedResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly _apiUrl = `${environment.apiUrl}/products`;
  private readonly _httpClient = inject(HttpClient);

  getAll(page: number = 1, pageSize: number = 10): Observable<ApiPaginatedResponse<Product>> {
    let params = new HttpParams().set('page', page.toString()).set('pageSize', pageSize.toString());

    return this._httpClient.get<ApiPaginatedResponse<Product>>(this._apiUrl, { params });
  }

  getById(id: number): Observable<Product> {
    return this._httpClient.get<Product>(`${this._apiUrl}/${id}`);
  }

  create(product: CreateProductRequest): Observable<Product> {
    return this._httpClient.post<Product>(this._apiUrl, product);
  }

  update(id: number, product: UpdateProductRequest): Observable<Product> {
    return this._httpClient.put<Product>(`${this._apiUrl}/${id}`, product);
  }

  delete(id: number): Observable<void> {
    return this._httpClient.delete<void>(`${this._apiUrl}/${id}`);
  }

  activate(id: number): Observable<void> {
    return this._httpClient.patch<void>(`${this._apiUrl}/activate/${id}`, {});
  }
}
