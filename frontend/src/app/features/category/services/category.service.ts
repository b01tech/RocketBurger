import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Category, CreateCategoryRequest, UpdateCategoryRequest } from '../models/category.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private readonly _apiUrl = `${environment.apiUrl}/categories`;
  private readonly _httpClient = inject(HttpClient);

  getAll(): Observable<Category[]> {
    return this._httpClient.get<Category[]>(this._apiUrl);
  }

  getById(id: number): Observable<Category> {
    return this._httpClient.get<Category>(`${this._apiUrl}/${id}`);
  }

  create(category: CreateCategoryRequest): Observable<Category> {
    return this._httpClient.post<Category>(this._apiUrl, category);
  }

  update(id: number, category: UpdateCategoryRequest): Observable<Category> {
    return this._httpClient.patch<Category>(`${this._apiUrl}/${id}`, category);
  }
}
