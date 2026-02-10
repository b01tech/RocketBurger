import { Routes } from '@angular/router';

export const productRoutes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/list-product-page/list-product-page').then((m) => m.ListProductPage),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./pages/create-product-page/create-product-page').then((m) => m.CreateProductPage),
  },
  {
    path: 'edit/:id',
    loadComponent: () =>
      import('./pages/update-product-page/update-product-page').then((m) => m.UpdateProductPage),
  },
];
