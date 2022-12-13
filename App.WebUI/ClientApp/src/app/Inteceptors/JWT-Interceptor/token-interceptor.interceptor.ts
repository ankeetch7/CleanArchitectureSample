import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwtToken = localStorage.getItem("token"); 
    if (jwtToken) {
      const requestWithJwtToken = request.clone({
        headers: request.headers.set("Authorization", "Bearer " + jwtToken)
      });
      return next.handle(requestWithJwtToken)
    }
    else {
      return next.handle(request);
    }
  }
}
