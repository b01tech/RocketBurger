import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CurrencyPipe, NgIf } from '@angular/common';
import { ProductService } from '../../services/product-service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-list-product-page',
  standalone: true,
  imports: [RouterLink, CurrencyPipe, NgIf],
  templateUrl: './list-product-page.html',
  styleUrl: './list-product-page.css',
})
export class ListProductPage {
  private readonly _productService = inject(ProductService);

  products = signal<Product[]>([]);
  currentPage = signal(1);
  pageSize = signal(10);
  totalItems = signal(0);
  totalPages = signal(0);

  constructor() {
    this.loadProducts();
  }

  loadProducts() {
    this._productService.getAll(this.currentPage(), this.pageSize()).subscribe((response) => {
      this.products.set(response.items);
      this.totalItems.set(response.pagination.totalItems);
      this.totalPages.set(response.pagination.totalPages);
    });
  }

  onPageChange(page: number) {
    this.currentPage.set(page);
    this.loadProducts();
  }

  onDelete(id: number) {
    if (confirm('Tem certeza que deseja excluir este produto?')) {
      this._productService.delete(id).subscribe(() => {
        this.loadProducts();
      });
    }
  }
}
