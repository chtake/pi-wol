import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IpAccessFormComponent } from './ip-access-form.component';

describe('IpAccessFormComponent', () => {
  let component: IpAccessFormComponent;
  let fixture: ComponentFixture<IpAccessFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IpAccessFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IpAccessFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
