import { Component, OnInit } from '@angular/core';
import { CinemaRoomService, CinemaRoomDTO } from '../services/cinema-room.service';
import { RouterModule } from '@angular/router';
import { NgForOf, NgIf } from '@angular/common'; 

@Component({
  standalone: true,
  imports: [
    NgForOf,
    //NgIf,
    RouterModule
  ],
  selector: 'app-cinema-room-list',
  templateUrl: './cinema-room-list.component.html',
})
export class CinemaRoomListComponent implements OnInit {
  rooms: CinemaRoomDTO[] = [];
  constructor(private svc: CinemaRoomService) {}
  ngOnInit() { 
    this.svc.getAll().subscribe(data => this.rooms = data);
  }
}