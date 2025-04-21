import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthenticateRequest } from '../../../shared/models/request/account/AuthenticateRequest';
import { AuthenticateResponse } from '../../../shared/models/response/account/AuthenticateResponse';
import { Observable } from 'rxjs';
import { AuthApi } from '../../../shared/const/api.url';
import { ResponseBase } from '../../../shared/models/common/ResponseBase';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  httpClient = inject(HttpClient);

  constructor() {}

  authenticate(
    request: AuthenticateRequest
  ): Observable<ResponseBase<AuthenticateResponse>> {
    return this.httpClient.post<ResponseBase<AuthenticateResponse>>(
      AuthApi.authenticate,
      request
    );
  }
}
