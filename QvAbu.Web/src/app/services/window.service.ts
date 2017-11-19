import { Injectable } from '@angular/core';

@Injectable()
export class WindowService {
  public scrollTo(x: number, y: number): void {
    window.scrollTo(x, y);
  }

  public print(): void {
    window.print();
  }
}
