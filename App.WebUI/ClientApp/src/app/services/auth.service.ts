import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AccountService } from './web-api-clients';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // user info 
  private _user: UserInfo;
  // creating behaviourl subject
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private _accountService: AccountService) {
    const token = localStorage.getItem('token');
    this._isLoggedIn$.next(!!token);
   }

   

  public login(authenticateRequest: any){
    
    return this._accountService.authenticateUser(authenticateRequest).pipe(
      tap(response=>{
        this._isLoggedIn$.next(true);
        localStorage.setItem('token', response.token as string);

      })
    )
  }

  public decodeToken(): any {
    let rawToken = localStorage.getItem('token');
    if (rawToken != null)
      return jwt_decode(rawToken);
    else
      return null;
  }

  public showLoginPageIfTokenExpries(): void {
    if (this.isTokenExpired())
      this._isLoggedIn$.next(false); // push to subscribers of observable
    else
      this._isLoggedIn$.next(true);  // push to subscribers of observable
  }

  public isTokenExpired(): boolean {
    let rawToken = localStorage.getItem('token');
    if (rawToken == null)
      return true;

    const date = this.getTokenExpirationDate();
    if (date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

  
  public getTokenExpirationDate(): Date {
    let rawToken = localStorage.getItem('token');
    const decoded: any = jwt_decode(rawToken as string);

    if (decoded.exp === undefined)
      return null as any;

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  public logout(): void {
    localStorage.removeItem('token'); 
   
    // push to subscribers of observable
    this._isLoggedIn$.next(false);
  }

  get userInfo(): UserInfo {
    if (this._user)
      return this._user;

    return this.createUserFromToken(localStorage.getItem('token') as string);
  }

  private createUserFromToken(rawToken: string): UserInfo {
    let token: any = jwt_decode(rawToken);
    let user = new UserInfo();
    user.fullname = token.unique_name;
    user.email = token.email;
    user.type = token.usertype;
    user.userId = token.sub;
    user.username = token.username;

    return user;
  }

}

export class UserInfo {
  username: string;
  fullname: string;
  email: string;
  type: string;
  userId: string;
}