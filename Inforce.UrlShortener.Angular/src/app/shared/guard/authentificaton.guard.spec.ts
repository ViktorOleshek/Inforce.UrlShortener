import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { authentificatonGuard } from './authentificaton.guard';

describe('authentificatonGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authentificatonGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
