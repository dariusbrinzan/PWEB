import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { TicketService } from '../ticket.service';
import { fixtureInterface } from '../fixtureInteface';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {

  formCreateDelete = new FormGroup({
    MatchId: new FormControl(null, Validators.required),
    Stand: new FormControl(null, Validators.required),
    Nr: new FormControl(null, Validators.required)
  });

  formUpdate = new FormGroup({
    MatchId: new FormControl(null, Validators.required),
    OldStand: new FormControl(null, Validators.required),
    NewStand: new FormControl(null, Validators.required),
    Nr: new FormControl(null, Validators.required)
  });

  attr: string = "";
  adminOptParam: string = "";
  adminOpt: string[] = ["Create", "Update", "Delete"];
  fixtures: fixtureInterface[] = [];

  constructor(
    private ticketService: TicketService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.router.events.subscribe((ev) => {
      if (ev instanceof NavigationEnd) {
        this.attr = String(this.route.snapshot.paramMap.get('param'));
        if (this.attr !== "null") {
          this.getNextFixtures();
        } else {
          this.adminOptParam = String(this.route.snapshot.paramMap.get('opt'));
        }
      }
    });
  }

  ngOnInit(): void {
    this.attr = String(this.route.snapshot.paramMap.get('param'));
    if (this.attr !== "null") {
      this.getNextFixtures();
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

    if (opt === 'Update') {
      this.ticketService.updateTickets(
        this.formUpdate.get('MatchId')?.value,
        this.formUpdate.get('OldStand')?.value,
        this.formUpdate.get('NewStand')?.value,
        this.formUpdate.get('Nr')?.value,
        token
      ).subscribe(response => {
        window.alert(`${response} tickets were updated out of ${this.formUpdate.get('Nr')?.value}`);
      });
    } else {
      if (opt === 'Create') {
        this.ticketService.createTickets(
          this.formCreateDelete.get('MatchId')?.value,
          this.formCreateDelete.get('Stand')?.value,
          this.formCreateDelete.get('Nr')?.value,
          token
        ).subscribe(() => {
          window.alert("Tickets were inserted successfully");
        });
      } else if (opt === 'Delete') {
        this.ticketService.deleteTickets(
          this.formCreateDelete.get('MatchId')?.value,
          this.formCreateDelete.get('Stand')?.value,
          this.formCreateDelete.get('Nr')?.value,
          token
        ).subscribe(response => {
          window.alert(`${response} tickets were deleted out of ${this.formCreateDelete.get('Nr')?.value}`);
        });
      }
    }
  }

  getNextFixtures() {
    const token = localStorage.getItem('tok'); // Retrieve the token from local storage
    if (token) {
      this.ticketService.getMatchTickets(token).subscribe(fixtures => this.fixtures = fixtures);
    } else {
      window.alert("You are not logged in.");
    }
  }
}
