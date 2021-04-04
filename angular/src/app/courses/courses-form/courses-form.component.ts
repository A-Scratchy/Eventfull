import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {CoursesService, ICreateCourseCommand} from "../courses.service";
import {Guid} from "guid-typescript";
import {Subscription} from "rxjs";

@Component({
    selector: 'app-courses-form',
    templateUrl: './courses-form.component.html',
    styleUrls: ['./courses-form.component.scss']
})
export class CoursesFormComponent implements OnInit {

    course: ICreateCourseCommand = {courseId: Guid.create().toString(), courseDate: new Date, capacity: 0};
    private sub: Subscription = new Subscription();
    private id: string | null = null;

    constructor(private activatedRoute: ActivatedRoute,
                private coursesService: CoursesService,
                private route: Router) {
    }

    onSubmit() {
        console.log(JSON.stringify(this.course));
        this.coursesService.createCourse(this.course).subscribe(data => {
            console.log(JSON.stringify(this.course));
            this.route.navigate(['/courses', this.course.courseId, "Created Ok"])
        })
    }


    ngOnInit(): void {
    }

    onCancelClicked() {

        this.route.navigate(['/courses'])
    }
}
