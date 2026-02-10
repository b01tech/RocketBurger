import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionPage } from './promotion-page';

describe('PromotionPage', () => {
  let component: PromotionPage;
  let fixture: ComponentFixture<PromotionPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PromotionPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PromotionPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
