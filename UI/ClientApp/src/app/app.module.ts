import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {OrdersComponent} from "./Orders/orders.component";
import {OrderFormComponent} from "./Orders/OrderForm.Component";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {TableModule} from "primeng/table";
import {CardModule} from "primeng/card";
import {TabMenuModule} from "primeng/tabmenu";
import {ProductsService} from "../services/products.service";
import {ProductsComponent} from './products/products.component';
import { ProductFormComponent } from './products/product-form/product-form.component';
import {InputNumberModule} from "primeng/inputnumber";
import {SelectButtonModule} from "primeng/selectbutton";
import {DataViewModule} from "primeng/dataview";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        OrdersComponent,
        OrderFormComponent,
        ProductsComponent,
        ProductFormComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', component: HomeComponent, pathMatch: 'full'},
            {path: 'counter', component: CounterComponent},
            {path: 'fetch-data', component: FetchDataComponent},
            {path: 'orders', component: OrdersComponent},
            {path: 'products', component: ProductsComponent},
        ]),
        InputTextModule,
        ButtonModule,
        TableModule,
        CardModule,
        TabMenuModule,
        InputNumberModule,
        SelectButtonModule,
        DataViewModule,
    ],
    providers: [
        ProductsService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
