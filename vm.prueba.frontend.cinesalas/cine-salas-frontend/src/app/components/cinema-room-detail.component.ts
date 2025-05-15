import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CinemaRoomService, CinemaRoomDTO } from '../services/cinema-room.service';
import { NgIf } from '@angular/common';        // ← importa NgIf
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [NgIf, RouterModule],             // ← añade NgIf aquí
  selector: 'app-cinema-room-detail',
  templateUrl: './cinema-room-detail.component.html',
})
export class CinemaRoomDetailComponent implements OnInit {
  room?: CinemaRoomDTO;

  constructor(
    private svc: CinemaRoomService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.svc.get(id).subscribe(r => this.room = r);
  }
}
