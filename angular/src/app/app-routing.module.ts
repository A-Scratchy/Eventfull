import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {OrdersComponent} from "./orders/orders.component";
import {ProductsComponent} from "./products/products.component";
import {HomeComponent} from "./home/home.component";
import {PeopleComponent} from "./people/people.component";
import {PeopleFormComponent} from "./people/people-form/people-form.component";

const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'orders', component: OrdersComponent},
    {path: 'products', component: ProductsComponent},
    {path: 'people', component: PeopleComponent},
    {path: 'people/add', component: PeopleFormComponent},
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
