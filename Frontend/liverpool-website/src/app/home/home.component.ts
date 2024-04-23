import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  images: String[] = [
    "/assets/images/PL.png", 
    "/assets/images/UCL.png",
    "/assets/images/ClubWc.png",
    "/assets/images/UefaCup.png",
    "/assets/images/UefaSuperCup.png",
    "/assets/images/FA.png",
    "/assets/images/LeagueCup.png",
    "/assets/images/ComShield.png"
  ];
  
  constructor() { }

  ngOnInit(): void {
  }

}
