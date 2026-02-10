import { Component, inject, input, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { CategoryForm } from '../../components/category-form/category-form';
import {
  Category,
  CreateCategoryRequest,
  UpdateCategoryRequest,
} from '../../models/category.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-category-page',
  standalone: true,
  imports: [CategoryForm, CommonModule],
  templateUrl: './update-category-page.html',
  styleUrl: './update-category-page.css',
})
export class UpdateCategoryPage implements OnInit {
  private _categoryService = inject(CategoryService);
  private _router = inject(Router);
  private _route = inject(ActivatedRoute);

  category = signal<Category | null>(null);
  categoryId: number = 0;

  ngOnInit() {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.categoryId = +id;
      this._categoryService.getById(this.categoryId).subscribe({
        next: (category) => this.category.set(category),
        error: (err) => {
          console.error('Error fetching category:', err);
          this._router.navigate(['/categories']);
        },
      });
    }
  }

  onSave(category: CreateCategoryRequest | UpdateCategoryRequest) {
    this._categoryService.update(this.categoryId, category as UpdateCategoryRequest).subscribe({
      next: () => {
        this._router.navigate(['/categories']);
      },
      error: (err) => {
        console.error('Error updating category:', err);
        // TODO: Show error message
      },
    });
  }

  onCancel() {
    this._router.navigate(['/categories']);
  }
}
