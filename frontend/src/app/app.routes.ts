import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/products',
    pathMatch: 'full',
  },
  {
    path: 'products',
    loadChildren: () =>
      import('./features/product/product.routes').then(
        (m) => m.productRoutes,
      ),
  },
  {
    path: 'categories',
    loadChildren: () =>
      import('./features/category/category.routes').then(
        (m) => m.categoryRoutes,
      ),
  },
  {
    path: 'promotions',
    loadComponent: () =>
      import('./features/promotion/pages/promotion-page/promotion-page').then(
        (m) => m.PromotionPage,
      ),
  },
  {
    path: 'about',
    loadComponent: () =>
      import('./features/about/pages/about-page/about-page').then((m) => m.AboutPage),
  },
  {
    path: 'contact',
    loadComponent: () =>
      import('./features/contact/pages/contact-page/contact-page').then((m) => m.ContactPage),
  },
];
