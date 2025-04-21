import { BaseModel } from '../../common/BaseModel';

export class AuthenticateResponse extends BaseModel {
  username: string;
  email: string;
  mobileNumber: string;
  passwordHash: string;
  roleId: string;
  jwtToken: string;
  refreshToken: string;

  constructor(
    username: string,
    email: string,
    mobileNumber: string,
    passwordHash: string,
    roleId: string,
    jwtToken: string = '',
    refreshToken: string = ''
  ) {
    super();
    this.username = username;
    this.email = email;
    this.mobileNumber = mobileNumber;
    this.passwordHash = passwordHash;
    this.roleId = roleId;
    this.jwtToken = jwtToken;
    this.refreshToken = refreshToken;
  }
}
