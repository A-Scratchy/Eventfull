import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {catchError, retry} from "rxjs/operators";
import {Guid} from "guid-typescript";

export interface ICourse {
    courseId: string;
    courseDate: Date,
    capacity: number,
    courseDelegates: IDelegate[],
    courseModules: IModule[],
    bookings: []
}

export interface ICourselistItem {
    availableSpaces: number;
    courseId: Guid;
    courseDate: Date;
    capacity: number;
    modules: string;
}

export interface IModule {
    name:string,
    startTime: Date,
}

export interface IDelegate {
    person: {
        firstName: string,
        lastName: string
    },
    attended: boolean | null,
    passed: boolean | null
}

export interface ICreateCourseCommand {
    courseDate: Date,
    courseId: string,
    capacity: number,
}

@Injectable({
    providedIn: 'root'
})

export class CoursesService {
    API_URL = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    httpHeader = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };

    public getCourses(): Observable<ICourselistItem[]> {
        console.log("calling " + this.API_URL + '/api/courses');
        return this.http.get<ICourselistItem[]>(this.API_URL + '/api/courses').pipe(
            retry(1),
            catchError(this.httpError)
        )
    }

    public getCourse(id: string) {
        return this.http.get<ICourse>(`${this.API_URL}/api/courses/${id}`).pipe(
            retry(1),
            catchError(this.httpError)
        )
    }

    public createCourse(product: ICreateCourseCommand): Observable<ICourse> {
        return this.http.post<ICourse>(this.API_URL + '/api/courses', JSON.stringify(product), this.httpHeader).pipe(
            retry(1),
            catchError(this.httpError))
    }

    public updateCourse(id: string, product: ICourse) {
        return this.http.put<ICourse>(`${this.API_URL}/api/courses/${id}`, JSON.stringify(product), this.httpHeader).pipe(
            retry(1),
            catchError(this.httpError))
    }

    public deleteCourse(id: string) {
        return this.http.delete(`${this.API_URL}/api/courses/${id}`).pipe(
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
