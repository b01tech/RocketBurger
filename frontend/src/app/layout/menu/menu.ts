import { Component } from '@angular/core';
import { MenuItem } from './menu-item/menu-item';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [MenuItem],
  templateUrl: './menu.html',
})
export class Menu {}
