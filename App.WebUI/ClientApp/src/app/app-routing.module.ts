import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth-guard/auth-guard.component';
import { ProductComponent } from './components/product/product.component';
import { HomeComponent } from './components/shared/home/home.component';
import { LoginComponent } from './components/user/login/login.component';

const routes: Routes = [
  { path: "", redirectTo: "/", pathMatch: "full" },
  { path: "login", component: LoginComponent, data: { title: "Login" } },
  { path: "", component: HomeComponent, canActivate: [AuthGuard] ,data: { title: "Home" } },
  { path: "product-list", component: ProductComponent, canActivate: [AuthGuard] ,data: { title: "Product List" } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
