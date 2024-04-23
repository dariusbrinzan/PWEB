// guard care verifica daca un user are rolul de admin
import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './authentication/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CheckRoleGuard implements CanActivate {
  
  constructor(
    private authService: AuthService,
    private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
    const isAdmin = localStorage.getItem("Role") == "Admin";
    console.log(isAdmin);
    if(!isAdmin) {
      window.alert("You are not authorized");
      this.router.navigate(["/"]);
    }
    
    return isAdmin;
  }
  
}
