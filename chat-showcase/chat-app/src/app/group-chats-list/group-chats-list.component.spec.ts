import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupChatsListComponent } from './group-chats-list.component';

describe('GroupChatsComponent', () => {
  let component: GroupChatsListComponent;
  let fixture: ComponentFixture<GroupChatsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupChatsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GroupChatsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
