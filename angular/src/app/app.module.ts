import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {OrdersComponent} from './orders/orders.component';
import {ProductsComponent} from './products/products.component';
import {PeopleComponent} from './people/people.component';
import {HomeComponent} from './home/home.component';
import {NavbarComponent} from './navbar/navbar.component';
import {HttpClientModule} from "@angular/common/http";
import { PeopleFormComponent } from './people/people-form/people-form.component';
import {FormsModule} from "@angular/forms";
import { PeopleDetailComponent } from './people/people-detail/people-detail.component';
import { CoursesListComponent } from './courses/courses-list/courses-list.component';
import { CoursesDetailComponent } from './courses/courses-detail/courses-detail.component';
import { CoursesFormComponent } from './courses/courses-form/courses-form.component';

@NgModule({
    declarations: [
        AppComponent,
        OrdersComponent,
        ProductsComponent,
        PeopleComponent,
        HomeComponent,
        NavbarComponent,
        PeopleFormComponent,
        PeopleDetailComponent,
        CoursesListComponent,
        CoursesDetailComponent,
        CoursesFormComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
