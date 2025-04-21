import {
  HttpEvent,
  HttpEventType,
  HttpHandler,
  HttpHandlerFn,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Injectable } from '@angular/core';
import { GuiService } from '../app/shared/services/gui.service';

@Injectable()
export class DefaultInterceptor implements HttpInterceptor {
  constructor(private guiService: GuiService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.guiService.presentLoader();

    if (!req.url.startsWith(environment.API_URL)) {
      // Clone the request and modify its URL
      const modifiedReq = req.clone({
        url: `${environment.API_URL}${req.url}`,
      });

      if (environment.CONSOLELOG) {
        if (req.method === 'GET') {
          // Log query parameters
          const params = req.params;
          console.log('Query Params:', params);

          // Log full URL with params
          console.log('Request URL:', req.urlWithParams);
        }

        console.log(
          `--------------------------------------------Request Start------------------------------------------------------`
        );
        console.log(
          `Requested for ${modifiedReq.url} | ${JSON.stringify(
            modifiedReq.body
          )}`
        );
      }
      return next.handle(modifiedReq).pipe(
        tap(
          (event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
              if (environment.CONSOLELOG) {
                console.log(
                  `Response with status ${event.status} | ${JSON.stringify(
                    event.body
                  )}`
                );
                console.log(
                  `--------------------------------------------Request End------------------------------------------------------`
                );
              }
              this.guiService.dismissLoader();
            }
          },
          (err: any) => {
            this.guiService.dismissLoader();
          }
        )
      );
    }

    return next.handle(req).pipe(
      tap(
        (event: HttpEvent<any>) => {
          if (event instanceof HttpResponse) {
            this.guiService.dismissLoader();
          }
        },
        (err: any) => {
          this.guiService.dismissLoader();
        }
      )
    );
  }
}
