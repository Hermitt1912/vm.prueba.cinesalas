import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface MovieDTO { id?: number; name: string; description?: string; releaseDate: string; director?: string; duration?: number; genre?: string; }

@Injectable({ providedIn: 'root' })
export class MovieService {
  private base = '/api/Movie';
  constructor(private http: HttpClient) {}
  getAll(): Observable<MovieDTO[]> { return this.http.get<MovieDTO[]>(this.base); }
}