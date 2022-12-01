// import { Injectable } from '@angular/core';
// import { Router } from '@angular/router';
// import { JwtHelperService } from '@auth0/angular-jwt';
// import { tap } from 'rxjs/operators';
// import { AccountService } from '../web-api-clients';
// @Injectable({
//     providedIn: 'root'
//   })

//   export class AppAuthService {
    
//     constructor(private _router: Router,
//                 private _jwtHelper : JwtHelperService,
//                 private _accountService: AccountService){}

//     public login(authenticateRequest: any){

//         return this._accountService.authenticateUser(authenticateRequest).pipe(
//             tap(response=>{
//                 localStorage.setItem('token', response.token as string);
//              })
//         );
//     }

//     public isUserAuthenticated() : boolean {
//         const token = localStorage.getItem("jwt");
//         if (token && !this._jwtHelper.isTokenExpired(token)) {
//           return true;
//         }
//         else {
//           return false;
//         }
//       }

//       public logout(): void {
//         localStorage.removeItem('token'); 
//       }
                
//   }