import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { matchInterface } from './matchInterface';
import { matchModel } from './matchModel';
import { looseInterface } from '../looseInterface';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  private formRes : looseInterface = {};

  constructor(private http: HttpClient) { }
  private headers = {'Content-Type': 'application/json'};

  // Serviciul care ia meciurile jucate intr-o competitie
  getMatchesByComp(pos: string, token: string): Observable<matchInterface[]> {
    const url = `http://localhost:30711/api/Match/${pos}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<matchInterface[]>(url, { headers: headers });
  }

  // Serviciul care creeaza un meci
  createMatch(match: matchModel, token: string) {
    const url = "http://localhost:30711/api/Match/add_match";
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const body = JSON.stringify(match);
    return this.http.post(url, body, { headers: headers });
  }


  // Serviciul care updateaza un meci, trimite doar campurile din form
  // care s-au introdus valori
  updateMatch(match: matchModel, token: string) {
    const url = "http://localhost:30711/api/Match/update_match";
    for (const [key, value] of Object.entries(match)) {
      if (value != null) {
        this.formRes[key] = value;
      }
    }
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const body = JSON.stringify(this.formRes);
    return this.http.put(url, body, { headers: headers });
  }

  // Serviciul care sterge un meci
  deleteMatch(id: any, token: string) {
    const url = `http://localhost:30711/api/Match/delete_match/${id}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete(url, { headers: headers });
  }
}
