import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduledMatchesComponent } from './scheduled-matches.component';

describe('ScheduledMatchesComponent', () => {
  let component: ScheduledMatchesComponent;
  let fixture: ComponentFixture<ScheduledMatchesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduledMatchesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduledMatchesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
