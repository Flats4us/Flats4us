import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivateChatsListComponent } from './private-chats-list.component';

describe('PrivateChatsListComponent', () => {
  let component: PrivateChatsListComponent;
  let fixture: ComponentFixture<PrivateChatsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrivateChatsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrivateChatsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
