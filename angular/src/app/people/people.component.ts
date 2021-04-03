import {Component, OnInit} from '@angular/core';
import {IPerson, PeopleService} from "./people.service";

@Component({
    selector: 'app-people',
    templateUrl: './people.component.html',
    styleUrls: ['./people.component.scss']
})
export class PeopleComponent implements OnInit {
    People: IPerson[] = [];

    constructor(private peopleService: PeopleService) {
    }

    ngOnInit(): void {
        this.peopleService.getPeople().subscribe(data => {
            this.People = data;
        })
    }

}
