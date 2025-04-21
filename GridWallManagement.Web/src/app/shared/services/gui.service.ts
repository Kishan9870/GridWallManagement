import { EventEmitter, Injectable, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuiService {

  public showToggleMenu = true;
  public showNavbar = true;
  public showProfileMenu = true;
  public navbarToggled = new Subject<any>();

  
  //#region Loader

  @Output() loader = new EventEmitter<boolean>();

  presentLoader(): void {
    this.loader.emit(true);
  }

  dismissLoader(): void {
    this.loader.emit(false);
  }

  //#endregion
  
  constructor() {
    // window.onresize = (() => {
    //   if (this.showToggleMenu) {
    //     if (window.screen.width < 768) {
    //       this.showNavbar = false;
    //     } else {
    //       this.showNavbar = true;
    //     }
    //   }
    // });
    // if (this.showToggleMenu) {
    //   if (window.screen.width < 768) {
    //     this.showNavbar = false;
    //   }
    // }
  }
}
