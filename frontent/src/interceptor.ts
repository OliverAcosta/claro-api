import { HttpEvent, HttpHandler, HttpHandlerFn, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

  export function BaseInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn):Observable<HttpEvent<unknown>> {
    const newReq = req.clone({
        headers: req.headers.append('Access-Control-Allow-Origin', "*"),
      });
    
      return next(newReq);
  }
