import { Routes } from '@angular/router';
import { LoginComponent } from './components/login.component';
import { CinemaRoomListComponent } from './components/cinema-room-list.component';
import { CinemaRoomDetailComponent } from './components/cinema-room-detail.component';
import { MovieListComponent } from './components/movie-list.component';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'rooms', component: CinemaRoomListComponent },
  { path: 'rooms/:id', component: CinemaRoomDetailComponent },
  { path: 'movies', component: MovieListComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: '' }
];


export const routes = appRoutes;