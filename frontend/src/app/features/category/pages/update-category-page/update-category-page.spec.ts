import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateCategoryPage } from './update-category-page';

describe('UpdateCategoryPage', () => {
  let component: UpdateCategoryPage;
  let fixture: ComponentFixture<UpdateCategoryPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateCategoryPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateCategoryPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
