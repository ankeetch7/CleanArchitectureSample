import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { AuthService } from "../services/auth-service/auth-service.service";

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    constructor(private _authService: AuthService,
                private _router: Router) { }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | 
                Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

        return this._authService.isLoggedIn$.pipe(
            tap((isLoggedIn: any) =>{
                if(!isLoggedIn){
                    this._router.navigate(['/login']);
                }
        }));
    }
    
}