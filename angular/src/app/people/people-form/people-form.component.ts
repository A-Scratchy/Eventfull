import {Component, OnInit} from '@angular/core';
import {IPerson, PeopleService} from "../people.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Guid} from 'guid-typescript';
import {Subscription} from "rxjs";

@Component({
    selector: 'app-people-form',
    templateUrl: './people-form.component.html',
    styleUrls: ['./people-form.component.scss']
})

export class PeopleFormComponent implements OnInit {

    person: IPerson = {personId: "", email: "", firstName: "", lastName: ""};
    private sub: Subscription = new Subscription();
    private id: string | null = null;


    constructor(private activatedRoute: ActivatedRoute, private peopleService: PeopleService, private route: Router) {
    }

    onSubmit() {
        console.log(JSON.stringify(this.person));
        if (!Guid.isGuid(this.person.personId)) {
            this.person.personId = Guid.create().toString();
            this.peopleService.createPerson(this.person).subscribe(data => {
                console.log(JSON.stringify(this.person));
                this.route.navigate(['/people', this.person.personId, "Created OK"]);
            });
        } else {
            this.peopleService.updatePerson(this.person.personId, this.person).subscribe(data => {
                console.log(JSON.stringify(this.person));
                this.route.navigate(['/people', this.person.personId]);
            });
        }
    }

    ngOnInit(): void {
        this.sub = this.activatedRoute.paramMap.subscribe(params => {
            console.log(params);
            this.id = params.get('id');
            if (this.id != null) {
                this.peopleService.getPerson(this.id).subscribe(data => this.person = data);
            }
        });
    }

    onCancelClicked() {
        this.route.navigate(['/people']);
    }
}
