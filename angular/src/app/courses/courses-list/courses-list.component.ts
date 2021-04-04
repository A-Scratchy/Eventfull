import { Component, OnInit } from '@angular/core';
import {CoursesService, ICourse, ICourselistItem} from "../courses.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.scss']
})

export class CoursesListComponent implements OnInit {
  Courses: ICourselistItem[] = [];

  constructor(private coursesService: CoursesService, private route: Router) { }

  ngOnInit(): void {
    this.coursesService.getCourses().subscribe(data => this.Courses = data)
  }

  onDeleteClicked(personId: any) {
    
  }

  OnEditClicked(personId: any) {
    
  }

  onAddClicked() {
    this.route.navigate(['/courses/add'])
  }
}
