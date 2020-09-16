import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor() { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retry(1),
      catchError((err) => {
        if (err.status === 401) {
          alert('error 401');
        }
        else if (err.status === 404) {
          alert('error 404');
        }
        else if (err.status === 400) {
          alert('error 400');
        }
        else {
          alert('Unknown error');
        }
        return throwError(err);
      })
    )
  }
}