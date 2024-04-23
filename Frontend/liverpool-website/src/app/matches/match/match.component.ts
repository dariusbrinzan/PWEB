import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { matchInterface } from '../matchInterface';
import { MatchService } from '../match.service';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent implements OnInit {

  matches: matchInterface[] = [];
  liverpoolCrest: String = "/assets/images/Teams/Liverpool.png";

  formCreate = new FormGroup({
    TeamId: new FormControl(null, Validators.required),
    HomeOrAway: new FormControl(null, Validators.required),
    GoalsFor: new FormControl(null, Validators.required),
    GoalsAgainst: new FormControl(null, Validators.required),
    CompetitionId: new FormControl(null, Validators.required),
    MatchDate: new FormControl(null, Validators.required)
  });

  formUpdate = new FormGroup({
    MatchId: new FormControl(null, Validators.required),
    TeamId: new FormControl(null),
    HomeOrAway: new FormControl(null),
    GoalsFor: new FormControl(null),
    GoalsAgainst: new FormControl(null),
    CompetitionId: new FormControl(null),
    MatchDate: new FormControl(null)
  });

  formDelete = new FormGroup({
    MatchId: new FormControl(null, Validators.required)
  });

  posAttr: string = "";
  adminOptParam: string = "";
  adminOpt: string[] = ["Create", "Update", "Delete"];

  constructor(
    private matchService: MatchService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.router.events.subscribe((ev) => {
      if (ev instanceof NavigationEnd) {
        this.posAttr = String(this.route.snapshot.paramMap.get('comp'));
        if (this.posAttr != "null") {
          this.getMatchesByComp(this.posAttr);
        } else {
          this.adminOptParam = String(this.route.snapshot.paramMap.get('opt'));
        }
      }
    });
  }

  ngOnInit(): void {
    this.posAttr = String(this.route.snapshot.paramMap.get('comp'));
    if (this.posAttr != "null") {
      this.getMatchesByComp(this.posAttr);
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
      this.matchService.createMatch(this.formCreate.value, token).subscribe(() => {
        window.alert("Match was inserted successfully");
      });
    } else if (opt === 'Update') {
      this.matchService.updateMatch(this.formUpdate.value, token).subscribe((response) => {
        window.alert("Match was updated successfully");
      });
    } else if (opt === 'Delete') {
      const matchId = this.formDelete.get('MatchId')?.value;
      this.matchService.deleteMatch(matchId, token).subscribe((response) => {
        window.alert("Match was deleted successfully");
      });
    }
  }

  getMatchesByComp(comp: string) {
    const token = localStorage.getItem('tok'); // Ensure the token is passed
    if (token) {
      this.matchService.getMatchesByComp(comp, token).subscribe(matches => {
        this.matches = matches;
      });
    } else {
      window.alert("You are not logged in.");
    }
  }
}
