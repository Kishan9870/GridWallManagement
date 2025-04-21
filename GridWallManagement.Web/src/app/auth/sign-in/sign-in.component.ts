import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticateRequest } from '../../shared/models/request/account/AuthenticateRequest';
import { AccountService } from '../shared/services/account.service';
import { AuthenticateResponse } from '../../shared/models/response/account/AuthenticateResponse';
import { ResponseBase } from '../../shared/models/common/ResponseBase';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss',
})
export class SignInComponent implements OnInit {
  signInForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.formDeclaration();
  }

  formDeclaration() {
    this.signInForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  signIn() {
    debugger;
    if (this.signInForm.invalid) {
      Object.keys(this.signInForm.controls).forEach((field) => {
        const control = this.signInForm.get(field);
        if (control && control.invalid) {
          console.error(`Field "${field}" is invalid. Errors:`, control.errors);
        }
      });
      return;
    }

    const request = new AuthenticateRequest();

    this.accountService.authenticate(request).subscribe(
      (response: ResponseBase<AuthenticateResponse>) => {
        if (response.responseStatusCodeValue === 200) {
          localStorage.setItem('token', response.result.jwtToken);
          sessionStorage.setItem('isLoggedIn', 'true');
          // alert('Login successful!');
          // this.toasterService.success('Welcome!!!');
          // this.router.navigateByUrl('/web');
        } else {
          // this.toasterService.error('Login fail!');
        }
      },
      (error) => {
        // this.toasterService.error('Login fail!');
      }
    );
  }
}
