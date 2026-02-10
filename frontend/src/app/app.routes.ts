import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/products',
    pathMatch: 'full',
  },
  {
    path: 'products',
    loadComponent: () =>
      import('./features/product/pages/list-product-page/list-product-page').then(
        (m) => m.ListProductPage,
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
