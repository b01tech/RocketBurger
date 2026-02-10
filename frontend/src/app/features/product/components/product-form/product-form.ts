import { Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateProductRequest, Product, UpdateProductRequest } from '../../models/product.model';
import { CategoryService } from '../../../category/services/category.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './product-form.html',
  styleUrl: './product-form.css',
})
export class ProductForm {
  private readonly _fb = inject(FormBuilder);
  private readonly _categoryService = inject(CategoryService);

  initialData = input<Product>();
  save = output<CreateProductRequest | UpdateProductRequest>();

  categories$ = this._categoryService.getAll();

  form = this._fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    description: [''],
    price: [0, [Validators.required, Validators.min(0)]],
    stockQuantity: [0, [Validators.required, Validators.min(0)]],
    categoryId: [null as number | null, [Validators.required]],
  });

  constructor() {
    effect(() => {
      const data = this.initialData();
      if (data) {
        this.form.patchValue({
          name: data.name,
          description: data.description,
          price: data.price,
          stockQuantity: data.stockQuantity,
          categoryId: data.categoryId,
        });
      }
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const product: any = {
        name: formValue.name!,
        description: formValue.description!,
        price: formValue.price!,
        stockQuantity: formValue.stockQuantity!,
        categoryId: Number(formValue.categoryId!),
      };

      if (this.initialData()) {
        product.id = this.initialData()!.id;
      }

      this.save.emit(product);
    }
  }
}
