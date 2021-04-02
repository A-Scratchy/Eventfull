import {Component, OnInit} from '@angular/core';
import {ProductsService, Product} from "../../services/products.service";

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css'],
    providers: [ProductsService]
})
export class ProductsComponent implements OnInit {
    Products: Product[] = [];

    constructor(private productsService: ProductsService) {
    }

    ngOnInit() {
        this.productsService.getProducts().subscribe((data) => {
            this.Products = data
        })
    }

}
