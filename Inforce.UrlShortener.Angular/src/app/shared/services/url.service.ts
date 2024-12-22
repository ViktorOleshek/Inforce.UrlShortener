import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UrlModel } from '../models/url-model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  private readonly apiUrl = `${environment.apiUrl}/Url`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<UrlModel[]> {
    return this.http.get<UrlModel[]>(`${this.apiUrl}`);
  }

  getById(id: number): Observable<UrlModel> {
    return this.http.get<UrlModel>(`${this.apiUrl}/${id}`);
  }

  add(url: UrlModel): Observable<UrlModel> {
    return this.http.post<UrlModel>(`${this.apiUrl}`, url);
  }

  update(id: number, url: UrlModel): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, url);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
