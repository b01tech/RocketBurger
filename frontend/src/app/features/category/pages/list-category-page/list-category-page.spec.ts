import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCategoryPage } from './list-category-page';

describe('ListCategoryPage', () => {
  let component: ListCategoryPage;
  let fixture: ComponentFixture<ListCategoryPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListCategoryPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListCategoryPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
