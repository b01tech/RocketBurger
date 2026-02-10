import { Routes } from '@angular/router';

export const categoryRoutes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/list-category-page/list-category-page').then((m) => m.ListCategoryPage),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./pages/create-category-page/create-category-page').then((m) => m.CreateCategoryPage),
  },
  {
    path: 'edit/:id',
    loadComponent: () =>
      import('./pages/update-category-page/update-category-page').then((m) => m.UpdateCategoryPage),
  },
];
