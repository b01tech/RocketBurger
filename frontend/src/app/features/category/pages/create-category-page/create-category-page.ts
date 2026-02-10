import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { CategoryForm } from '../../components/category-form/category-form';
import { CreateCategoryRequest, UpdateCategoryRequest } from '../../models/category.model';

@Component({
  selector: 'app-create-category-page',
  standalone: true,
  imports: [CategoryForm],
  templateUrl: './create-category-page.html',
  styleUrl: './create-category-page.css',
})
export class CreateCategoryPage {
  private _categoryService = inject(CategoryService);
  private _router = inject(Router);

  onSave(category: CreateCategoryRequest | UpdateCategoryRequest) {
    this._categoryService.create(category as CreateCategoryRequest).subscribe({
      next: () => {
        this._router.navigate(['/categories']);
      },
      error: (err) => {
        console.error('Error creating category:', err);
        // TODO: Show error message
      },
    });
  }

  onCancel() {
    this._router.navigate(['/categories']);
  }
}
