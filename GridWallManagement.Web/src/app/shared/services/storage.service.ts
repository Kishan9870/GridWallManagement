import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  // private _loggedInUserInfo: UserInfo = new UserInfo();
  // public get loggedInUserInfo(): UserInfo {
  //   return this._loggedInUserInfo;
  // }
  // public set loggedInUserInfo(value: UserInfo) {
  //   this._loggedInUserInfo = value;
  //   if (value) {
  //     this.setProfileImage();
  //     localStorage.setItem('LoggedInUserInfo', JSON.stringify(value));
  //   } else {
  //     localStorage.removeItem('LoggedInUserInfo');
  //   }
  // }
  // private _menus: any;
  // public get menus() {
  //   return this._menus;
  // }
  // public profileImage = '';
  // public data: any;
  // constructor() {
  //   console.log('LoggedInUserInfo', localStorage.getItem('LoggedInUserInfo'));
  //   const loggedInUser = localStorage.getItem('LoggedInUserInfo');
  //   // const loggedInUser = JSON.parse(localStorage.getItem('LoggedInUserInfo') || '');
  //   if (loggedInUser) {
  //     this._loggedInUserInfo = JSON.parse(loggedInUser);
  //     // this.setProfileImage();
  //   }
  // }
  // setProfileImage(): void {
  //   const avatar = this._loggedInUserInfo.avatar;
  //   this.profileImage = environment.BASE_URL + avatar;
  // }
  // clearStorage(): void {
  //   // this.loggedInUser = null;
  //   localStorage.clear();
  //   sessionStorage.clear();
  // }
}
