import {Component, OnInit} from '@angular/core';
import {IPerson, PeopleService} from "./people.service";
import {Router} from "@angular/router";

@Component({
    selector: 'app-people',
    templateUrl: './people.component.html',
    styleUrls: ['./people.component.scss']
})

export class PeopleComponent implements OnInit {
    People: IPerson[] = [];

    constructor(private peopleService: PeopleService, private route: Router) {
    }

    ngOnInit(): void {
        this.peopleService.getPeople().subscribe(data => {
            this.People = data;
        })
    }

    onAddClicked() {
        this.route.navigate(['/people/add']);
    }

    onDeleteClicked(id: string) {
        this.peopleService.deletePerson(id).subscribe(_ => this.People = this.People.filter(p => p.personId != id));
    }

    OnEditClicked(personId: string) {
        this.route.navigate(['/people/edit', personId]);
    }
}
