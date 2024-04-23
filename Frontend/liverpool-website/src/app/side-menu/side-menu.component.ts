import { Component, OnInit } from '@angular/core';
import { menuOptions } from './menu-options';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  // optiunile din side-menu
  options: menuOptions[] = [
    {optName: "Home", subOptions: [], selected: false},
    {optName: "Squad", subOptions: ["Goalkeeper", "Defender", "Midfielder", "Forward", "Admin"], selected: false},
    {optName: "Fixtures", subOptions: ["Premier League", "Uefa Champions League", "FA Cup", "EFL Cup", 'Admin'], selected: false},
    {optName: "Tickets", subOptions: ["Available Tickets", "Admin"], selected: false},
    {optName: "Authentication", subOptions: ["Register", "Login"], selected: false}
  ];

  clicked(option: menuOptions): void {
    option.selected = !option.selected;
  }
}
