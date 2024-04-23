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

  // form pentru creare sau stergere de bilete,
  // nu creez sau sterg un bilet anume, 
  // creez sau sterg mai multe bilete cu aceleasi
  // caracteristici
  formCreateDelete = new FormGroup({
    MatchId: new FormControl(null, Validators.required),
    Stand: new FormControl(null, Validators.required),
    Nr: new FormControl(null, Validators.required)
  });

  // form pentru modificarea unor bilete,
  // proprietatea modificata este cea a stand-ului
  // din care fac parte
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
    // verific parametrii din ruta pentru a afisa continutul corespunzator
    this.router.events.subscribe((ev) => {
      if (ev instanceof NavigationEnd) {
        this.attr = String(this.route.snapshot.paramMap.get('param'));
        if (this.attr != "null") {
          this.getNextFixtures();
        }
        else {
          this.adminOptParam = String(this.route.snapshot.paramMap.get('opt'));
        }
      }
    })
  }


  ngOnInit(): void {
    // verific parametrii din ruta pentru a afisa continutul corespunzator
    this.attr = String(this.route.snapshot.paramMap.get('param'));
    if (this.attr != "null")
      this.getNextFixtures();
    else
      this.adminOptParam = String(this.route.snapshot.paramMap.get('opt'));
  }

   // functia care trimite datele introduse in form serviciului
  onSubmit() {
    const opt = String(this.route.snapshot.paramMap.get('opt'));
    window.alert(opt);
    // admin-ul modifica bilete, daca pentru un meci nu exista bilete in 
    // oldStand cate vrea sa transforme in newStand, se vor modifica doar
    // toate biletele oldStand care exista si se va afisa numarul biletelor
    // modificate
    if (opt == 'Update') {
      var mId = this.formUpdate.get('MatchId')?.value;
      var oldStand = this.formUpdate.get('OldStand')?.value;
      var newStand = this.formUpdate.get('NewStand')?.value;
      var nr = this.formUpdate.get('Nr')?.value;

      return this.ticketService.updateTickets(mId, oldStand, newStand, nr).subscribe((response) => {
          window.alert(`${response} tickets were updated out of ${nr}`);
      })
    }
    else {
      var mId = this.formCreateDelete.get('MatchId')?.value;
      var stand = this.formCreateDelete.get('Stand')?.value;
      var nr = this.formCreateDelete.get('Nr')?.value;

      // admin-ul creeaza sau sterge bilete
      // pentru stergere, se intampla la fel ca pentru update,
      // se sterg toate biletele deja existente in stand-ul respectiv,
      // chiar daca numarul de bilete sterse dorit este mai mare
      if (opt == 'Create') {
        return this.ticketService.createTickets(mId, stand, nr).subscribe((response) => {
            window.alert("Tickets were inserted successfully");
        })
      }
      else {
        return this.ticketService.deleteTickets(mId, stand, nr).subscribe((response) => {
            window.alert(`${response} tickets were deleted out of ${nr}`);
        })
      }
    }
  }

  // functie care ia meciurile care inca nu s-au disputat
  getNextFixtures() {
    this.ticketService.getMatchTickets().subscribe(fixtures => this.fixtures = fixtures);
  }

}
