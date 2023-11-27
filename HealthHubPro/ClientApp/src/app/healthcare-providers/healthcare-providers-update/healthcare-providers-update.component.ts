import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, NgForm, FormControl } from '@angular/forms';
import { HealthcareProvider } from 'src/app/models/HealthcareProvider.model';
import { ActivatedRoute } from '@angular/router';
import { Person } from 'src/app/models/Person.model';
import { Address } from 'src/app/models/Address.model';
import { EmergencyContact } from 'src/app/models/EmergencyContact.model';
import { ProfileFormService } from 'src/app/services/profile-form.service';
import { Appointment } from 'src/app/models/Appointment.model';
import { Specialty } from 'src/app/models/Specialty.model';

@Component({
  selector: 'app-healthcare-providers-update',
  templateUrl: './healthcare-providers-update.component.html'
})
export class HealthcareProvidersUpdateComponent implements OnInit {

  personId: string = "";
  person: Person = new Person();
  address: Address = new Address();
  emergencyContact: EmergencyContact = new EmergencyContact();
  healthcareProvider: HealthcareProvider = new HealthcareProvider();
  commonSpecialties: Specialty[] = [];
  specialty: Specialty = new Specialty();
  appointments: Appointment[] = [];
  specialtiesForm!: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private service: ProfileFormService,
    private formBuilder: FormBuilder
  ) {}
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.personId = params['id'];
      this.loadData();
    });
  }
  loadData(){
    this.service.getHealthcareProviderData(this.personId).subscribe((data) => {
      this.person = data.person;
      this.address = data.address;
      this.emergencyContact = data.emergencyContact;
      this.healthcareProvider = data.healthcareProvider;
      this.commonSpecialties = data.commonSpecialties;
      this.appointments = data.appointments;
      this.buildForm();
    });
  }
  buildForm() {
    this.specialtiesForm = this.formBuilder.group({
      commonSpecialties: this.buildSpecialtyCheckboxes()
    });
    this.setInitialCheckboxValues();
  }

  buildSpecialtyCheckboxes() {
    const checkboxes = this.commonSpecialties.map(() => {
      return this.formBuilder.control(false);
    });
  
    return this.formBuilder.array(checkboxes);
  }

  setInitialCheckboxValues() {
    const specialtyControls = this.specialtiesForm.get('commonSpecialties') as FormArray;

    this.healthcareProvider.specialties.forEach(specialty => {
      const index = this.commonSpecialties.findIndex(s => s.id === specialty.id);
      if (index !== -1) {
        specialtyControls.controls[index].setValue(true);
      }
    });
  }

  getCommonSpecialtiesControl(index: number): FormControl {
    const commonSpecialtiesArray = this.specialtiesForm.get('commonSpecialties') as FormArray;
    return commonSpecialtiesArray.controls[index] as FormControl;
  }
  

  onSubmitSpecialties(){
    this.healthcareProvider.specialties = this.specialtiesForm.value.commonSpecialties
    .map((checked: boolean, index: number) => checked ? this.commonSpecialties[index] : null)
    .filter((specialty: Specialty | null): specialty is Specialty => specialty !== null);
    this.service.editHealthcareProvider(this.healthcareProvider)
  .subscribe({
    next: res => {
      console.log(res);
    },
    error: err => {
      console.log(err);
    }
  });
  }

  onSubmit(personalInfo: NgForm) {
    if(personalInfo.valid){
      this.service.editPerson(this.person? this.person: {} as Person)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
    this.service.editAddress(this.address? this.address: {} as Address)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
    this.service.editEmergencyContact(this.emergencyContact? this.emergencyContact: {} as EmergencyContact)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
    }    
  }
  onSpecialtySubmit(addSpecialty: NgForm){
    this.service.postSpecialty(this.specialty)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
  }
}