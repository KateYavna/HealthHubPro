import { Component, OnInit } from '@angular/core';
import { ProfileFormService } from '../services/profile-form.service';
import { Person } from '../models/Person.model';

@Component({
  selector: 'app-healthcare-providers',
  templateUrl: './healthcare-providers.component.html'
})
export class HealthcareProvidersComponent implements OnInit {
  healthcareProviders: Person[]=[];
  constructor(public profileFormService: ProfileFormService) { }
  ngOnInit() {
    this.profileFormService.getAllHealthcareProviders()
    .subscribe({
      next: res => {
        this.healthcareProviders = res;
      }
    })
  }
}