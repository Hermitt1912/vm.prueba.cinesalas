import { Component, OnInit } from '@angular/core';
import { MovieService, MovieDTO } from '../services/movie.service';

@Component({
  standalone: true,
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
})
export class MovieListComponent implements OnInit {
  movies: MovieDTO[] = [];
  constructor(private svc: MovieService) {}
  ngOnInit() {
    this.svc.getAll().subscribe(data => this.movies = data);
  }
}