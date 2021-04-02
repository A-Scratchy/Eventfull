import {Inject, Injectable} from '@angular/core';
import {environment} from '../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, throwError} from "rxjs";
import {catchError, retry} from "rxjs/operators";

export class Product {
    productId: string;
    category: string;
    name: string;
    quantity: number;
    price: number;
    cost: number;
    imageUri: string;
}

@Injectable({
    providedIn: 'root'
})

export class ProductsService {
    API_URL = environment.apiUrl;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.API_URL = baseUrl
    }

    httpHeader = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };

    public getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(this.API_URL + '/api/products').pipe(
            retry(1),
            catchError(this.httpError)
        )
    }

    public getProduct(id: string) {
        return this.http.get<Product>(`${this.API_URL}/api/products/${id}`).pipe(
            retry(1),
            catchError(this.httpError)
        )
    }

    public createProducts(product: Product): Observable<Product> {
        return this.http.post<Product>(this.API_URL + '/api/products', JSON.stringify(product), this.httpHeader).pipe(
            retry(1),
            catchError(this.httpError))
    }

    public updateProduct(id: string, product: Product) {
        return this.http.put<Product>(`${this.API_URL}/api/products/${id}`, JSON.stringify(product), this.httpHeader).pipe(
            retry(1),
            catchError(this.httpError))
    }

    httpError(error) {
        let msg = '';
        if (error.error instanceof ErrorEvent) {
            msg = error.error.message;
        } else {

            msg = `Error Code: ${error.status}\nMessage: ${error.message} `;
        }
        console.log(msg);
        return throwError(msg);
    }
}
