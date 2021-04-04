import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {OrdersComponent} from "./orders/orders.component";
import {ProductsComponent} from "./products/products.component";
import {HomeComponent} from "./home/home.component";
import {PeopleComponent} from "./people/people.component";
import {PeopleFormComponent} from "./people/people-form/people-form.component";
import {PeopleDetailComponent} from "./people/people-detail/people-detail.component";
import {CoursesDetailComponent} from "./courses/courses-detail/courses-detail.component";
import {CoursesListComponent} from "./courses/courses-list/courses-list.component";
import {CoursesFormComponent} from "./courses/courses-form/courses-form.component";

const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'orders', component: OrdersComponent},
    {path: 'products', component: ProductsComponent},
    
    {path: 'people', component: PeopleComponent},
    {path: 'people/add', component: PeopleFormComponent},
    {path: 'people/edit/:id', component: PeopleFormComponent},
    {path: 'people/:id/:message', component: PeopleDetailComponent},
    {path: 'people/:id', component: PeopleDetailComponent},

    {path: 'courses', component: CoursesListComponent},
    {path: 'courses/add', component: CoursesFormComponent},
    {path: 'courses/:id', component: CoursesDetailComponent},
    {path: 'courses/:id/:message', component: CoursesDetailComponent},
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
