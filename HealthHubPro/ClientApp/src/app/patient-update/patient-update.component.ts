import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, NgForm, FormControl } from '@angular/forms';
import { Allergy } from '../models/Allergy.model';
import { HealthcareProvider } from '../models/HealthcareProvider.model';
import { ActivatedRoute } from '@angular/router';
import { Person } from '../models/Person.model';
import { Address } from '../models/Address.model';
import { EmergencyContact } from '../models/EmergencyContact.model';
import { AppointmentPostModel } from '../models/AppointmentPostModel.model';
import { PrescriptionPostModel } from '../models/PrescriptionPostModel.model';
import { ProfileFormService } from '../services/profile-form.service';
import { Patient } from '../models/Patient.model';
import { Appointment } from '../models/Appointment.model';
import { PrescriptionHistory } from '../models/PrescriptionHistory.model';
import { Prescription } from '../models/Prescription.model';

@Component({
  selector: 'app-patient-update',
  templateUrl: './patient-update.component.html'
})
export class PatientUpdateComponent implements OnInit {

  personId: string = "";
  person: Person = new Person();
  address: Address = new Address();
  emergencyContact: EmergencyContact = new EmergencyContact();
  patient: Patient = new Patient();
  commonAllergies: Allergy[] = [];
  healthcareProviders: HealthcareProvider[] = [];
  appointments: Appointment[] = [];
  prescriptions: Prescription[] = [];
  prescriptionHistories: PrescriptionHistory[] = [];
  patientAllergiesForm!: FormGroup;
  appointment: AppointmentPostModel= new AppointmentPostModel();
  prescription: PrescriptionPostModel = new PrescriptionPostModel();
  prescriptionHistory: PrescriptionHistory = new PrescriptionHistory();

  constructor(
    private route: ActivatedRoute,
    private patientService: ProfileFormService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.personId = params['id'];
      this.loadData();
    });
  }

  loadData() {
      this.patientService.getPatientData(this.personId).subscribe((data) => {
      this.person = data.person;
      this.address = data.address;
      this.emergencyContact = data.emergencyContact;
      this.patient = data.patient;
      this.commonAllergies = data.commonAllergies;
      this.healthcareProviders = data.healthcareProviders;
      this.appointments = data.appointments;
      this.prescriptions = data.prescriptions;
      this.buildForm();
      this.appointment.patientId = this.patient.id;
      this.prescription.patientId = this.patient.id;
   });
  }

  buildForm() {
    this.patientAllergiesForm = this.formBuilder.group({
      commonAllergies: this.buildAllergyCheckboxes()
    });

    this.setInitialCheckboxValues();
  }

  buildAllergyCheckboxes() {
    const checkboxes = this.commonAllergies.map(allergy => {
      return this.formBuilder.control(false); 
    });

    return this.formBuilder.array(checkboxes);
  }

  setInitialCheckboxValues() {
    const allergyControls = this.patientAllergiesForm.get('commonAllergies') as FormArray;

    this.patient.allergies.forEach(allergy => {
      const index = this.commonAllergies.findIndex(a => a.id === allergy.id);
      if (index !== -1) {
        allergyControls.controls[index].setValue(true);
      }
    });
  }

  getCommonAllergiesControl(index: number): FormControl {
    const commonAllergiesArray = this.patientAllergiesForm.get('commonAllergies') as FormArray;
    return commonAllergiesArray.controls[index] as FormControl;
  }

  onSubmitAllergies() {
    this.patient.allergies = this.patientAllergiesForm.value.commonAllergies
      .map((checked: boolean, index: number) => checked ? this.commonAllergies[index] : null)
      .filter((allergy: Allergy | null): allergy is Allergy => allergy !== null);
    this.patientService.editPatient(this.patient)
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
      this.patientService.editPerson(this.person? this.person: {} as Person)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
    this.patientService.editAddress(this.address? this.address: {} as Address)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
    this.patientService.editEmergencyContact(this.emergencyContact? this.emergencyContact: {} as EmergencyContact)
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
  onAppointmentSubmit(appointmentInfo: NgForm){
   if(this.appointment.id == "00000000-0000-0000-0000-000000000000"){
    this.insertAppointment(appointmentInfo);
   }
   else{
    this.updateAppointment(appointmentInfo);
   }
  }
  insertAppointment(appointmentInfo: NgForm){
    this.patientService.postAppointment(this.appointment)
    .subscribe({
      next: res => {
        this.appointments = res as Appointment[];
        this.resetAppointmentForm(appointmentInfo);
      },
      error: err => {
        console.log(err);
      }
    });
  }
  updateAppointment(appointmentInfo: NgForm){
    this.patientService.editAppointment(this.appointment)
    .subscribe({
      next: res => {
        this.appointments = res as Appointment[];
        this.resetAppointmentForm(appointmentInfo);
      },
      error: err => {
        console.log(err);
      }
    });
  }
  deleteAppointment(id: string){
    this.patientService.deleteAppointment(id)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
  }
  populateAppointmentForm(appointment: AppointmentPostModel){
    this.appointment = Object.assign({}, appointment);
  }
  resetAppointmentForm(appointmentInfo: NgForm){
    appointmentInfo.form.reset();
    this.appointment = new AppointmentPostModel();
  }

  onPrescriptionSubmit(prescriptionInfo: NgForm){
    if(this.prescription.id == "00000000-0000-0000-0000-000000000000"){
      this.insertPrescription(prescriptionInfo);
     }
     else{
      this.updatePrescription(prescriptionInfo);
     }
  }

  insertPrescription(prescriptionInfo: NgForm){
    this.patientService.postPrescription(this.prescription)
    .subscribe({
      next: res => {
        this.prescriptions = res as Prescription[];
        this.resetPrescriptionForm(prescriptionInfo);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  updatePrescription(prescriptionInfo: NgForm){
    this.patientService.editPrescription(this.prescription)
    .subscribe({
      next: res => {
        this.prescriptions = res as Prescription[];
        this.resetPrescriptionForm(prescriptionInfo);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  deletePrescription(id: string){
    this.patientService.deletePrescription(id)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
  }
  populatePrescriptionForm(prescription: PrescriptionPostModel){
    this.prescription = Object.assign({}, prescription);
  }
  resetPrescriptionForm(prescriptionInfo: NgForm){
    prescriptionInfo.form.reset();
    this.prescription = new PrescriptionPostModel();
  }

  onPrescriptionHistorySubmit(prescriptionHistoryInfo: NgForm){
    if(this.prescriptionHistory.id == "00000000-0000-0000-0000-000000000000"){
      this.insertPrescriptionHistory(prescriptionHistoryInfo);
     }
     else{
      this.updatePrescriptionHistory(prescriptionHistoryInfo);
     }
  }

  insertPrescriptionHistory(prescriptionHistoryInfo: NgForm){
    this.prescriptionHistory.prescriptionId = this.prescription.id;
    this.patientService.postPrescriptionHistory(this.prescriptionHistory)
    .subscribe({
      next: res => {
        this.prescriptionHistories = res as PrescriptionHistory[];
        this.resetPrescriptionHistoryForm(prescriptionHistoryInfo);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  updatePrescriptionHistory(prescriptionHistoryInfo: NgForm){
    this.prescriptionHistory.prescriptionId = this.prescription.id;
    this.patientService.editPrescriptionHistory(this.prescriptionHistory)
    .subscribe({
      next: res => {
        this.prescriptionHistories = res as PrescriptionHistory[];
        this.resetPrescriptionHistoryForm(prescriptionHistoryInfo);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  deletePrescriptionHistory(id: string){
    this.patientService.deletePrescriptionHistory(id)
    .subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
  }
  populatePrescriptionHistoryForm(prescriptionHistory: PrescriptionHistory){
    this.prescriptionHistory = Object.assign({}, prescriptionHistory);
  }
  resetPrescriptionHistoryForm(prescriptionHistoryInfo: NgForm){
    prescriptionHistoryInfo.form.reset();
    this.prescriptionHistory = new PrescriptionHistory();
  }
  isPrescriptionIdEmpty(): boolean {
    return this.prescription.id === "00000000-0000-0000-0000-000000000000";
}

}
