import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  //for greeting
  greet: string ;
  constructor(private _authService: AuthService) { }

  ngOnInit(): void {
    var myDate = new Date();
    var hrs = myDate.getHours();

    if (hrs < 12)
      this.greet = 'Good Morning ,';

    else if (hrs >= 12 && hrs <= 16)
      this.greet = 'Good Afternoon ,';

    else if (hrs >= 17 && hrs <= 20)
      this.greet = 'Good Evening ,';

    else if (hrs >= 21 && hrs <= 24)
      this.greet = 'Hello ,';
  }

  logout() :void {
    this._authService.logout();
  }

  getUsername() : string {
    return this._authService?.userInfo?.username ?? "";
  }

  getFullname() : string {
    return this._authService?.userInfo?.fullname ?? "";
  }

  userAvatar(): string {
    return `https://avatars.dicebear.com/api/bottts/${this.getUsername()}.svg?background=%230000ff`;
  }
}
