import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PlayerComponent } from './players/player/player.component';
import { MatchComponent } from './matches/match/match.component';
import { AuthComponent } from './authentication/auth/auth.component';
import { CheckRoleGuard } from './check-role.guard';
// import { IsLoggedInGuard } from './is-logged-in.guard';
import { TicketComponent } from './tickets/ticket/ticket.component';

const routes: Routes = [
  {path: 'Home', component: HomeComponent},
  {path: '', redirectTo: '/Authentication/Login', pathMatch: 'full'},
  {path: 'Squad/Admin', redirectTo: '/Squad/Admin/Create', pathMatch: 'full'},
  {path: 'Squad/Admin/:opt', component: PlayerComponent, canActivate:[CheckRoleGuard]},
  {path: 'Squad/:pos', component: PlayerComponent},  
  {path: 'Fixtures/Admin', redirectTo: '/Fixtures/Admin/Create', pathMatch: 'full'},
  {path: 'Fixtures/Admin/:opt', component: MatchComponent, canActivate: [CheckRoleGuard]},
  {path: 'Fixtures/:comp', component: MatchComponent},
  {path: 'Tickets/Admin', redirectTo: '/Tickets/Admin/Create', pathMatch: 'full'},
  {path: 'Tickets/Admin/:opt', component: TicketComponent, canActivate:[CheckRoleGuard]},
  {path: 'Tickets/:param', component: TicketComponent},
  {path: 'Authentication/:opt', component: AuthComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
