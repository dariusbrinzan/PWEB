import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { fixtureInterface } from './fixtureInteface';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private http: HttpClient) { }

  // Utilizează HttpHeaders pentru a adăuga header-ul de autorizare
  private getHeaders(token: string): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Adaugă token-ul în header-ul de autorizare
    });
  }

  // Serviciul care ia informații despre meciurile încă nedisputate și numărul biletelor disponibile
  getMatchTickets(token: string): Observable<fixtureInterface[]> {
    const url = `http://localhost:30711/api/Ticket/get_all_tickets`;
    return this.http.get<fixtureInterface[]>(url, { headers: this.getHeaders(token) });
  }

  // Serviciul care creează bilete
  createTickets(match_id: number, stand: string, nr: number, token: string) {
    const url = `http://localhost:30711/api/Ticket/add_tickets/${match_id}/${stand}/${nr}`;
    return this.http.post(url, "", { headers: this.getHeaders(token) });
  }

  // Serviciul care modifică bilete, returnează câte bilete au fost modificate cu succes
  updateTickets(match_id: number, oldStand: string, newStand: string, nr: number, token: string) {
    const url = `http://localhost:30711/api/Ticket/update_tickets/${match_id}/${oldStand}/${newStand}/${nr}`;
    return this.http.put(url, "", { headers: this.getHeaders(token) });
  }

  // Serviciul care șterge bilete, returnează câte bilete au fost șterse cu succes
  deleteTickets(match_id: number, stand: string, nr: number, token: string) {
    const url = `http://localhost:30711/api/Ticket/delete_tickets/${match_id}/${stand}/${nr}`;
    return this.http.delete(url, { headers: this.getHeaders(token) });
  }
}
