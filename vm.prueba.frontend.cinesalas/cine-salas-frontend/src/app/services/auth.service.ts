import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

interface LoginDTO { username: string; password: string; }

@Injectable({ 
  providedIn: 'root' 
})
/*export class AuthService {
  private tokenKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjY5NTBiMmY1LWEyMTEtNDU0MC1iNTk5LTU1ZDMwNTQ3NzVkNSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ3MzM4MzkyLCJpc3MiOiJ2bS5wcnVlYmEuY2luZXNhbGFzLmFwaSIsImF1ZCI6InZtLnBydWViYS5jaW5lc2FsYXMuY2xpZW50In0.flMGGFtlDrcwR9ccfidMsSu4qCmfI1x7OBcFncmDQXs';
  constructor(private http: HttpClient) {}
  login(data: LoginDTO) {
    return this.http.post('/api/Auth/login', data).pipe(
      tap((res: any) => localStorage.setItem(this.tokenKey, res.token))
    );
  }
  get token() { return localStorage.getItem(this.tokenKey) || ''; }
}*/
export class AuthService {
  constructor(private http: HttpClient) {}

  login(dto: { username: string; password: string }) {
    return this.http.post('/api/auth/login', dto);
  }
}