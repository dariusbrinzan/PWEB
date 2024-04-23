import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { authInterface } from './authInterface';
import { logResInterface } from './logResInterface';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly tokenName = 'tok';
  user: authInterface  = {email: "", role: ""};

  get token(): any {
    return localStorage.getItem(this.tokenName);
  } 
  
  constructor(private http: HttpClient) {
  }

  // serviciul care inregistreaza un user si returneaza daca acesta a avut succes sau nu
  registerUser(mail: string, pass: string): Observable<boolean>{
    const headers = { 'content-type': 'application/json'}
    const url = "http://localhost:30711/api/Auth/Register";
    return this.http.post<boolean>(url, {Email: mail, Password: pass, Role: "User"}, {'headers': headers});
  }

  // serviciul care logheaza un user si returneaza daca acesta a avut succes sau nu, plus informatii despre user-ul conectat
  logUser(mail: string, pass: string): Observable<logResInterface>{
    const url = "http://localhost:30711/api/Auth/Login";
    return this.http.post<logResInterface>(url, {Email: mail, Password: pass})
    .pipe(
      tap((response) => {
        if(response.success) {
          localStorage.clear();
          localStorage.setItem(this.tokenName, response.accessToken);
          this.user = this.getUser(response.accessToken);
          localStorage.setItem("Email", this.user.email);
          localStorage.setItem("Role", this.user.role);           
        }
      }));
  }

  // serviciul care delogheaza un user, sterge toate informatiile despre acesta din localStorage
  logOut() {
    localStorage.clear();
    window.alert("Logout was successful");
  }

  // functie care ia informatiile despre user din token
  public getUser(token: string): authInterface {
      if(!token)
        return {email: "", role: ""};
      
        return JSON.parse(atob(token.split('.')[1])) as authInterface;
  }

}
