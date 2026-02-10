import { Component, effect, inject, input, signal } from '@angular/core';
import { Router } from '@angular/router';
import { ProductForm } from '../../components/product-form/product-form';
import { ProductService } from '../../services/product-service';
import { Product, UpdateProductRequest } from '../../models/product.model';

@Component({
  selector: 'app-update-product-page',
  standalone: true,
  imports: [ProductForm],
  templateUrl: './update-product-page.html',
  styleUrl: './update-product-page.css',
})
export class UpdateProductPage {
  private readonly _productService = inject(ProductService);
  private readonly _router = inject(Router);

  id = input.required<string>();

  product = signal<Product | undefined>(undefined);

  constructor() {
    effect(() => {
      const productId = Number(this.id());
      if (productId) {
        this.loadProduct(productId);
      }
    });
  }

  loadProduct(id: number) {
    this._productService.getById(id).subscribe((data) => {
      this.product.set(data);
    });
  }

  onSave(request: UpdateProductRequest | any) {
    const productId = Number(this.id());
    this._productService.update(productId, request as UpdateProductRequest).subscribe(() => {
      this._router.navigate(['/products']);
    });
  }
}
