import {Component, EventEmitter, Inject, inject, Output} from '@angular/core';
import {OrderFormModel} from './OrderFormModel';
import {HttpClient} from "@angular/common/http";

@Component({
    selector: 'order-form',
    templateUrl: './orderForm.component.html',
})

export class OrderFormComponent {

    @Output() submittedEvent = new EventEmitter();
    model = new OrderFormModel("");

    submitted = false;
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    onSubmit() {
        console.log("submitted");
        this.submittedEvent.emit();
        this.submitted = true;
    }

    get diagnostic() {
        return JSON.stringify(this.model);
    }

    public async createOrder() {
        console.log(JSON.stringify(this.model));
        await this.http.post(this.baseUrl + '/api/orders', this.model).subscribe(result => {
            console.log(result);
            this.onSubmit()
        });

    }
}