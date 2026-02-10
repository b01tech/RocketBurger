import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  Category,
  CreateCategoryRequest,
  UpdateCategoryRequest,
} from '../../models/category.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './category-form.html',
  styleUrl: './category-form.css',
})
export class CategoryForm {
  initialData = input<Category | null>(null);
  isEditMode = input<boolean>(false);
  private _fb = inject(FormBuilder);

  save = output<CreateCategoryRequest | UpdateCategoryRequest>();
  cancel = output<void>();

  form = this._fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    description: [''],
  });

  constructor() {
    effect(() => {
      const data = this.initialData();
      if (data) {
        this.form.patchValue({
          name: data.name,
          description: data.description,
        });
        if (this.isEditMode()) {
          this.form.controls.name.disable();
        }
      }
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.getRawValue();
      if (this.isEditMode()) {
        this.save.emit({ description: formValue.description ?? '' } as UpdateCategoryRequest);
      } else {
        this.save.emit({
          name: formValue.name ?? '',
          description: formValue.description ?? '',
        } as CreateCategoryRequest);
      }
    }
  }

  onCancel() {
    this.cancel.emit();
  }
}
