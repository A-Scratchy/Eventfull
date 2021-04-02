import {Component} from '@angular/core';
import {Product, ProductsService} from "../../../services/products.service";

interface Category {
    name: string,
    code: string
}

@Component({
    selector: 'app-product-form',
    templateUrl: './product-form.component.html',
    styleUrls: ['./product-form.component.css']
})

export class ProductFormComponent {

    product: Product = new Product();
    
    Categories: Category[];
    
    constructor(private productsService: ProductsService) {
        this.Categories = [
            {name: "Physical", code: 'Physical'},
            {name: "Event Ticket", code: 'Physical'},
            {name: "Service", code: 'Service'}
        ]
    }

    onSubmitTemplateBased() {
        console.log(JSON.stringify(this.product));
        this.productsService.createProducts(this.product).subscribe(() => {
            this.productsService.getProducts().subscribe((data) => {
            })
        });
    }
}


