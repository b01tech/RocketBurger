import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-list-category-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './list-category-page.html',
  styleUrl: './list-category-page.css',
})
export class ListCategoryPage {
  private _categoryService = inject(CategoryService);
  categories$ = this._categoryService.getAll();
}
