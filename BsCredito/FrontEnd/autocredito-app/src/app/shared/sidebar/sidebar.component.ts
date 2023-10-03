import { Component, OnInit } from '@angular/core';
import { Menu } from 'src/app/modelos/Menu';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  menu: Menu[];
  constructor() { }

  ngOnInit(): void {
  }

}
