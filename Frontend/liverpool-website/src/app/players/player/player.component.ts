import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { playerInterface } from '../playerInterface';
import { PlayerService } from '../player.service';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  players: playerInterface[] = [];
  formCreate = new FormGroup({
    Name: new FormControl(null, Validators.required),
    BirthDate: new FormControl(null, Validators.required),
    Nationality: new FormControl(null, Validators.required),
    Position: new FormControl(null, Validators.required),
    ShirtNumber: new FormControl(null, Validators.required),
    PhotoUrl: new FormControl(null)
  });

  formUpdate = new FormGroup({
    PlayerId: new FormControl(null, Validators.required),
    Name: new FormControl(null),
    BirthDate: new FormControl(null),
    Nationality: new FormControl(null),
    Position: new FormControl(null),
    ShirtNumber: new FormControl(null),
    PhotoUrl: new FormControl(null)
  });

  formDelete = new FormGroup({
    PlayerId: new FormControl(null, Validators.required)
  });

  posAttr: string = "";
  adminOptParam: string = "";
  adminOpt: string[] = ["Create", "Update", "Delete"];

  constructor(
    private playerService: PlayerService, 
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.router.events.subscribe((ev) => {
      if (ev instanceof NavigationEnd) {
        this.posAttr = String(this.route.snapshot.paramMap.get('pos'));
        if (this.posAttr != "null") {
          this.getPlayersByPos(this.posAttr);
        } else {
          this.adminOptParam = String(this.route.snapshot.paramMap.get('opt'));
        }
      }
    });
  }

  ngOnInit(): void {
    this.posAttr = String(this.route.snapshot.paramMap.get('pos'));
    if (this.posAttr != "null") {
      this.getPlayersByPos(this.posAttr);
    } else {
      this.adminOptParam = String(this.route.snapshot.paramMap.get('opt'));
    }
  }

  onSubmit() {
    const opt = String(this.route.snapshot.paramMap.get('opt'));
    const token = localStorage.getItem('tok'); // Retrieve the token from local storage

    if (!token) {
      window.alert("You are not logged in.");
      return;
    }

    if (opt === "Create") {
      this.playerService.createPlayer(this.formCreate.value, token).subscribe(() => {
        window.alert("Player was inserted successfully");
      });
    } else if (opt === 'Update') {
      this.playerService.updatePlayer(this.formUpdate.value, token).subscribe(() => {
        window.alert("Player was updated successfully");
      });
    } else if (opt === 'Delete') {
      const playerId = this.formDelete.get('PlayerId')?.value;
      this.playerService.deletePlayer(playerId, token).subscribe(() => {
        window.alert("Player was deleted successfully");
      });
    }
  }

  getPlayersByPos(pos: string) {
    const token = localStorage.getItem('tok'); // Ensure the token is passed
    if (token) {
      this.playerService.getPlayersByPos(pos, token).subscribe(players => this.players = players);
    } else {
      window.alert("You are not logged in.");
    }
  }

  showDetails(player: playerInterface): void {
    player.selected = !player.selected;
  }
}
