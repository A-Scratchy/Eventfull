import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {catchError, retry} from "rxjs/operators";
import {environment} from "../../environments/environment";

export interface IPerson {
    personId: string;
    firstName: string;
    lastName: string;
    email: string;
}

@Injectable({
    providedIn: 'root'
})
export class PeopleService {
    API_URL = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    httpHeader = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };

    public getPeople(): Observable<IPerson[]> {
        console.log("calling " + this.API_URL + '/api/products');
        return this.http.get<IPerson[]>(this.API_URL + '/api/people').pipe(
            retry(1),
            catchError(this.httpError)
        )
    }

    public getPerson(id: string) {
        return this.http.get<IPerson>(`${this.API_URL}/api/people/${id}`).pipe(
            retry(1),
            catchError(this.httpError)
        )
    }

    public createPerson(product: IPerson): Observable<IPerson> {
        return this.http.post<IPerson>(this.API_URL + '/api/people', JSON.stringify(product), this.httpHeader).pipe(
            retry(1),
            catchError(this.httpError))
    }

    public updatePerson(id: string, product: IPerson) {
        return this.http.put<IPerson>(`${this.API_URL}/api/people/${id}`, JSON.stringify(product), this.httpHeader).pipe(
            retry(1),
            catchError(this.httpError))
    }

    public deletePerson(id: string) {
        return this.http.delete(`${this.API_URL}/api/people/${id}`).pipe(
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
