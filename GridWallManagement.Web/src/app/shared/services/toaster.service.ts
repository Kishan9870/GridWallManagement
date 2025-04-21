import { Injectable } from '@angular/core';
declare var toastr: any;

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  constructor() {
    toastr.options = {
      closeButton: false,
      debug: false,
      newestOnTop: false,
      progressBar: false,
      positionClass: 'toastr-top-right',
      preventDuplicates: false,
      onclick: null,
      showDuration: '300',
      hideDuration: '1000',
      timeOut: '5000',
      extendedTimeOut: '1000',
      showEasing: 'swing',
      hideEasing: 'linear',
      showMethod: 'fadeIn',
      hideMethod: 'fadeOut',
    };
  }

  success(message: string) {
    toastr.success(message);
  }

  error(message: string) {
    toastr.error(message);
  }
}
