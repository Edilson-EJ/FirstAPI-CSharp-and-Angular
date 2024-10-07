import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalUpdatePersonComponent } from './modal-update-person.component';

describe('ModalUpdatePersonComponent', () => {
  let component: ModalUpdatePersonComponent;
  let fixture: ComponentFixture<ModalUpdatePersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalUpdatePersonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalUpdatePersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
