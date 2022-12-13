import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// Import from library
import {
  NgxAwesomePopupModule,
  DialogConfigModule,
  ConfirmBoxConfigModule,
  ToastNotificationConfigModule
} from '@costlydeveloper/ngx-awesome-popup';
import { LoginComponent } from './components/user/login/login.component';
import { RegisterComponent } from './components/user/register/register.component';
import { SidebarComponent } from './components/shared/sidebar/sidebar.component';
import { DashboardComponent } from './components/dashboard/dashboard/dashboard.component';
import { HomeComponent } from './components/shared/home/home.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { ProductComponent } from './components/product/product.component';
import { AuthService } from './services/auth-service/auth-service.service';
import { TokenInterceptorService } from './Inteceptors/JWT-Interceptor/token-interceptor.interceptor';
import { AuthGuard } from './auth-guard/auth-guard.component';
import { CreateProductComponent } from './components/product/create-product/create-product.component';
import { UpdateProductComponent } from './components/product/update-product/update-product.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    SidebarComponent,
    DashboardComponent,
    HomeComponent,
    HeaderComponent,
    ProductComponent, CreateProductComponent, UpdateProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      // closeButton: true,
      progressBar: true,
      tapToDismiss: true,
      preventDuplicates: true,
      countDuplicates: false,
      easeTime: 800,
      positionClass: 'toast-bottom-right'
    }) ,
    AgGridModule,
    FormsModule,
    NgxAwesomePopupModule.forRoot(), // Essential, mandatory main module.
    DialogConfigModule.forRoot(), // Needed for instantiating dynamic components.
    ConfirmBoxConfigModule.forRoot(), // Needed for instantiating confirm boxes.
    ToastNotificationConfigModule.forRoot() // Needed for instantiating toast notifications.
  ],
  providers: [
    AuthService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
