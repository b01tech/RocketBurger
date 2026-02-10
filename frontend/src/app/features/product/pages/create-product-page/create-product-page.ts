import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ProductForm } from '../../components/product-form/product-form';
import { ProductService } from '../../services/product-service';
import { CreateProductRequest } from '../../models/product.model';

@Component({
  selector: 'app-create-product-page',
  standalone: true,
  imports: [ProductForm],
  templateUrl: './create-product-page.html',
  styleUrl: './create-product-page.css',
})
export class CreateProductPage {
  private readonly _productService = inject(ProductService);
  private readonly _router = inject(Router);

  onSave(product: CreateProductRequest | any) {
    this._productService.create(product as CreateProductRequest).subscribe(() => {
      this._router.navigate(['/products']);
    });
  }
}
