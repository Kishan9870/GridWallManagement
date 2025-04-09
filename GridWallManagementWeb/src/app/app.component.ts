import { Component } from '@angular/core';
import { ThemeService } from './services/theme/theme.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    standalone: false
})
export class AppComponent {
  title = 'GridWallManagementWeb';

  constructor(public themeService: ThemeService) {}
}
