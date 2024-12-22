import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AboutUsComponent } from './components/about-us/about-us.component';
import { UrlTableComponent } from './components/url/url-table/url-table.component';
import { UrlDetailComponent } from './components/url/url-detail/url-detail.component';
import { LoginComponent } from './components/login/login.component';
import { AuthentificatonGuard } from './shared/guard/authentificaton.guard';
import { NotFoundComponent } from './components/not-found/not-found.component';

export const routes: Routes = [
  { path: 'about-us', component: AboutUsComponent },
  { path: 'urls', component: UrlTableComponent },
  { path: 'urls/:id', component: UrlDetailComponent, canActivate: [AuthentificatonGuard] },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/urls', pathMatch: 'full' }, // main page
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
