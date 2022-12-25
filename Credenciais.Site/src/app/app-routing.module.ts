import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CredencialComponent } from './components/credencial/credencial.component';
import { LoginComponent } from './components/login/login.component';
import { MenuComponent } from './components/menu/menu.component';
import { RedefinirsenhaComponent } from './components/redefinirsenha/redefinirsenha.component';
import { UsuarioComponent } from './components/usuario/usuario.component';

const routes: Routes = [
  { path: '', redirectTo: '/menu', pathMatch: 'full' },
  { path: 'menu', component: MenuComponent },
  { path: 'login', component: LoginComponent },
  { path: 'redefinirsenha', component: RedefinirsenhaComponent },
  { path: 'usuario', component: UsuarioComponent },
  { path: 'menu/credencial/novo', component: CredencialComponent },
  { path: 'menu/credencial/alterar/:id', component: CredencialComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
