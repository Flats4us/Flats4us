import { TestBed } from '@angular/core/testing';

import { PrivateChatsListService } from './private-chats-list.service';

describe('PrivateChatsListService', () => {
  let service: PrivateChatsListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PrivateChatsListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
