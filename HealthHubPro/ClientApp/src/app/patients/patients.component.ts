import { Component, OnInit } from '@angular/core';
import { ProfileFormService } from '../services/profile-form.service';
import { Person } from '../models/Person.model';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html'
})
export class PatientsComponent implements OnInit {
  patients: Person[]=[];
  constructor(public profileFormService: ProfileFormService) { }
  ngOnInit() {
    this.profileFormService.getAllPatients()
    .subscribe({
      next: res => {
        this.patients = res;
      }
    })
  }
}
