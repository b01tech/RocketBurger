import { Component } from '@angular/core';
import { Logo } from './logo/logo';
import { Menu } from '../menu/menu';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [Logo, Menu],
  templateUrl: './header.html',
})
export class Header {}
