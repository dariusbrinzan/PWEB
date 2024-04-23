import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { fixtureInterface } from './fixtureInteface';


@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private http: HttpClient) { }

  // serviciul care ia informatii despre meciurile inca nedisputate
  // si numarul biletelor disponibile
  getMatchTickets() : Observable<fixtureInterface[]> {
    const url = `http://localhost:30711/api/Ticket/get_all_tickets`;
    return this.http.get<fixtureInterface[]>(url);
  }

  // serviciul care creeaza bilete
  createTickets(match_id: number, stand: string, nr: number) {
    const url = `http://localhost:30711/api/Ticket/add_tickets/${match_id}/${stand}/${nr}`;
    return this.http.post(url, "");
  }

  // serviciul care modifica bilete, returneaza cate bilete au fost modificate cu succes
  updateTickets(match_id: number, oldStand: string, newStand: string, nr: number) {
    const url = `http://localhost:30711/api/Ticket/update_tickets/${match_id}/${oldStand}/${newStand}/${nr}`;
    return this.http.put(url, "");
  }

  // serviciul care sterge bilete, returneaza cate bilete au fost sterse cu succes
  deleteTickets(match_id: number, stand: string, nr: number) {
    const url = `http://localhost:30711/api/Ticket/delete_tickets/${match_id}/${stand}/${nr}`;
    return this.http.delete(url);
  }

}
