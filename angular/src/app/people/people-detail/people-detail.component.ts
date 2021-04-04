import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs";
import {IPerson, PeopleService} from "../people.service";

@Component({
    selector: 'app-people-detail',
    templateUrl: './people-detail.component.html',
    styleUrls: ['./people-detail.component.scss']
})
export class PeopleDetailComponent implements OnInit {
    private sub: Subscription = new Subscription();
    private id: string | null = null;
    person: IPerson = {email: "", firstName: "", lastName: "", personId: ""};
    message: string | null = null;

    constructor(private activatedRoute: ActivatedRoute, private peopleService: PeopleService) {

    }

    ngOnInit(): void {
        this.sub = this.activatedRoute.paramMap.subscribe(params => {
            console.log(params);
            this.id = params.get('id');
            this.message = params.get('message');
            if (this.id != null) {
                this.peopleService.getPerson(this.id).subscribe(data => this.person = data);
            }
        });
    }

}
