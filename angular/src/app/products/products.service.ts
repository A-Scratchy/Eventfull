import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, throwError} from "rxjs";
import {catchError, retry} from "rxjs/operators";
import {environment} from "../../environments/environment";

export interface IProduct {
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
  
  constructor(private http: HttpClient) {
  }

  httpHeader = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  public getProducts(): Observable<IProduct[]> {
    console.log("calling " + this.API_URL + '/api/products');
    return this.http.get<IProduct[]>(this.API_URL + '/api/products').pipe(
        retry(1),
        catchError(this.httpError)
    )
  }

  public getProduct(id: string) {
    return this.http.get<IProduct>(`${this.API_URL}/api/products/${id}`).pipe(
        retry(1),
        catchError(this.httpError)
    )
  }

  public createProducts(product: IProduct): Observable<IProduct> {
    return this.http.post<IProduct>(this.API_URL + '/api/products', JSON.stringify(product), this.httpHeader).pipe(
        retry(1),
        catchError(this.httpError))
  }

  public updateProduct(id: string, product: IProduct) {
    return this.http.put<IProduct>(`${this.API_URL}/api/products/${id}`, JSON.stringify(product), this.httpHeader).pipe(
        retry(1),
        catchError(this.httpError))
  }

  httpError(error: any) {
    let msg = '';
    if (error.error instanceof ErrorEvent) {
      msg = 'Error event: ' + error.error.message;
    } else {
      msg = `Error Code: ${error.status}\nMessage: ${error.message} `;
    }
    console.log(msg);
    return throwError(msg);
  }
}
