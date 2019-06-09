import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HostsFormComponent } from './hosts-form.component';

describe('HostsFormComponent', () => {
  let component: HostsFormComponent;
  let fixture: ComponentFixture<HostsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HostsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HostsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
