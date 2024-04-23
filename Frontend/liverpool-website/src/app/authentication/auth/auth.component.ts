import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  regResponse: boolean = false;

  form = new FormGroup( {
    email: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required)
  });


  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
    ) { }

  ngOnInit(): void {
  }

  // functia care trimite datele introduse in form serviciului
  onSubmit() {
      const opt = String(this.route.snapshot.paramMap.get('opt'));
      // daca incerc sa ma loghez, verific daca acesta a avut succes si redirectionez catre Home 
      if(opt == "Login") { 
         return this.authService.logUser(this.form.get('email')?.value, this.form.get('password')?.value).subscribe((response) => {
          if(response.success) {
            window.alert("Logged successfully!");
            this.router.navigate(['/Home']);
          }
          else
           window.alert("Login failed!"); 
         });
      }

      // daca incerc sa ma inregistrez, verific daca acesta a avut succes si redirectionez catre Login
      else if(opt == "Register") {
        return this.authService.registerUser(this.form.get('email')?.value, this.form.get('password')?.value).subscribe((response) => {
          if (response) {
            window.alert("Registration was successful!");
            this.router.navigate(['/Authentication/Login']);
          }
          else
            window.alert("Registration failed!");
        });
      }

      return false;
  }

  
  logOut() {
    this.authService.logOut();
  }

}
