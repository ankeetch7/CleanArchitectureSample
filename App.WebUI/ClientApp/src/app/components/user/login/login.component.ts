import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthenticationRequest } from 'src/app/models/customer/authenticate-request';
import { AuthService } from 'src/app/services/auth-service/auth-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  // reactive form
  loginForm: any;
  submitted: boolean = false;

  constructor(private _formBuilder: FormBuilder,
              private _authService: AuthService,
              private _toastrService: ToastrService,
              private _router: Router) { }

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      username: [null, Validators.required],
      password: [null, Validators.required],
    });
  }

  get getFormControl(){
    return this.loginForm.controls;
  }

  onLogin() : void {
    this._toastrService.info('Please wait....','info!');
    this.submitted = true;
    if(this.loginForm.invalid) return;

    let request : AuthenticationRequest = {
      username : this.loginForm.value.username,
      password : this.loginForm.value.password
    };
    this._authService.login(request).subscribe(res=>{
      this._toastrService.success('Logged in successfully.','Success!');
      this._router.navigate(['']);   
    },err=>{
      console.log(err);
      this._toastrService.error('Unauthorized user.','Error!');
    });
  }

  // onLogin() : void {
  //   this.submitted = true;
  //   if(this.loginForm.invalid) return;

  //   let request : AuthenticationRequest = {
  //     username : this.loginForm.value.username,
  //     password : this.loginForm.value.password
  //   };
  //   this._appAuthService.login(request).subscribe(res=>{
  //     this._toastrService.success('Logged in successfully!','Success!!!');
  //     this._router.navigate(['']);   
  //     this.isInvalidLogin = false;
  //   },err=>{
  //     console.log(err);
  //     this.isInvalidLogin = true;
  //     this._toastrService.error('Unauthorized user','Error!!!');
  //   });
  // }

}
