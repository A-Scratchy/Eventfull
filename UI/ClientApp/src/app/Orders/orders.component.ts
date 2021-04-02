import {Component, EventEmitter, Inject, OnInit, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html'
})

export class OrdersComponent implements OnInit {
    public orders: Order[];
    private http: HttpClient;
    private readonly baseUrl: string;

    @Output()
    change = new EventEmitter();
    
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.getOrders();
    }
    
    ngOnInit() {
        this.getOrders();
    } 

    private getOrders() {
        this.http.get<Order[]>(this.baseUrl + '/api/orders').subscribe(result => {
            this.orders = result;
        }, error => console.error(error));
    }

    onFormSubmitted() {
        console.log("reached parent");
        this.getOrders();
    }
}

interface Order {
    description: string;
    id: string;
    customerId: string;
}