import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { playerInterface } from './playerInterface';
import { Observable } from 'rxjs';
import { playerModel } from './playerModel';
import { looseInterface } from '../looseInterface';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  private formRes : looseInterface = {};

  constructor(private http: HttpClient) { }
  private headers = {'Content-Type': 'application/json'};

  // Serviciul care ia jucatorii de pe o pozitie
  getPlayersByPos(pos: string, token: string): Observable<playerInterface[]> {
    const url = `http://localhost:30711/api/Player/${pos}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<playerInterface[]>(url, { headers });
  }

  // Serviciul care creeaza un jucator
  createPlayer(player: playerModel, token: string) {
    const url = "http://localhost:30711/api/Player/add_player";
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const body = JSON.stringify(player);
    return this.http.post(url, body, { headers });
  }

  // Serviciul care updateaza un jucator
  updatePlayer(player: playerModel, token: string) {
    const url = "http://localhost:30711/api/Player/update_player";
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    for (const [key, value] of Object.entries(player)) {
      if(value != null) {
        this.formRes[key] = value;
      }
    }
    const body = JSON.stringify(this.formRes);
    return this.http.put(url, body, { headers });
  }

  // Serviciul care sterge un jucator
  deletePlayer(id: any, token: string) {
    const url = `http://localhost:30711/api/Player/delete_player/${id}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete(url, { headers });
  }
}
