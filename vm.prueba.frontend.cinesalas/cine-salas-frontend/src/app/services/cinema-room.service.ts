import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CinemaRoomDTO { id?: number; name: string; capacity: number; isVIP?: boolean; status?: string; movieCount?: number; }

@Injectable({ providedIn: 'root' })
export class CinemaRoomService {
  private base = '/api/CinemaRoom';
  constructor(private http: HttpClient) {}
  getAll(): Observable<CinemaRoomDTO[]> { return this.http.get<CinemaRoomDTO[]>(`${this.base}/getAll`); }
  get(id: number) { return this.http.get<CinemaRoomDTO>(`${this.base}/${id}`); }
}