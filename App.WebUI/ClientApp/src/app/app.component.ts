import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  title = 'ClientApp';

  isLoggedIn$ : Observable<boolean>;
  constructor(private _authService: AuthService){
   
    this._authService.showLoginPageIfTokenExpries();
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this._authService.isLoggedIn$;
  }

  getUsername() : string {
    return this._authService?.userInfo?.username ?? "";
  }

  userAvatar(): string {
    return `https://avatars.dicebear.com/api/bottts/${this.getUsername()}.svg?background=%230000ff`;
  }

}
