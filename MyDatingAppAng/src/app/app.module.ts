import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from "./home/home.component";
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessageComponent } from './message/message.component';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
// import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { EditMemberComponent } from './members/edit-member/edit-member.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    HomeComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessageComponent,
    MemberCardComponent,
    EditMemberComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
