import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfirmaComponent } from './components/confirma/confirma.component';
import { LoginComponent } from './components/login/login.component';
import { MenuComponent } from './components/menu/menu.component';
import { RedefinirsenhaComponent } from './components/redefinirsenha/redefinirsenha.component';
import { UsuarioComponent } from './components/usuario/usuario.component';
import { Token } from './models/token.model';
import { MaterialModule } from './modules/material/material.module';
import { ListaCredenciaisComponent } from './components/lista-credenciais/lista-credenciais.component';
import { CredencialComponent } from './components/credencial/credencial.component';

export function tokenGetter() {
  return (localStorage.getItem('token') as Token).token || '';
}

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    LoginComponent,
    RedefinirsenhaComponent,
    UsuarioComponent,
    ConfirmaComponent,
    ListaCredenciaisComponent,
    CredencialComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FlexLayoutModule,
    FormsModule, 
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:4200"],
        disallowedRoutes: [],
      },
    }),   
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
