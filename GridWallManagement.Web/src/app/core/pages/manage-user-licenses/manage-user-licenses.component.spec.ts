import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageUserLicensesComponent } from './manage-user-licenses.component';

describe('ManageUserLicensesComponent', () => {
  let component: ManageUserLicensesComponent;
  let fixture: ComponentFixture<ManageUserLicensesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageUserLicensesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageUserLicensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
