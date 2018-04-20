import { Component } from '@angular/core';
import { WindowService } from './services/window.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private window: WindowService) { }

  public scrollToTop(): void {
    this.window.scrollTo(0, 0);
  }
}
