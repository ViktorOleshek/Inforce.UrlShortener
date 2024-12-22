import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import {routes} from './app/app.routes';
import {HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi} from '@angular/common/http';
import {AuthentificationInterceptor} from './app/interceptors/authentification.interceptor';
import {provideRouter} from '@angular/router';


bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(
      withInterceptorsFromDi()
    ),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthentificationInterceptor,
      multi: true, // Allows multiple interceptors if needed
    },
    provideRouter(routes)
  ]
}).catch(err => console.error(err));
