import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CoursesService, ICourse, IDelegate, IModule} from "../courses.service";
import {Subscription} from "rxjs";

class Person { 
    firstName: string = '';
    lastName: string = ''
}

class Delegate implements IDelegate {
    person: Person = new Person();
    attended: boolean | null = null;
    passed: boolean | null = null;
}

class Module implements IModule{
    name: string = '';
    startTime: Date = new Date();
}

class Course implements ICourse {
    capacity: number = 0;
    courseDate: Date = new Date();
    courseId: string = '';
    courseModules: Module[] = [];
    courseDelegates: Delegate[] = [];
    bookings: [] = [];
}

@Component({
    selector: 'app-courses-detail',
    templateUrl: './courses-detail.component.html',
    styleUrls: ['./courses-detail.component.scss']
})

export class CoursesDetailComponent implements OnInit {
    private id: string | null = null;
    message: string | null = null;
    course: Course = new Course();
    private sub: Subscription = new Subscription();

    constructor(private activatedRoute: ActivatedRoute, private coursesService: CoursesService) {
    }

    ngOnInit(): void {
        this.sub = this.activatedRoute.paramMap.subscribe(params => {
            console.log(params);
            this.id = params.get('id');
            this.message = params.get('message');
            if (this.id != null) {
                this.coursesService.getCourse(this.id).subscribe(data => {
                    console.log(JSON.stringify(data));
                    return this.course = data;
                });
            }
        });
    }

}
