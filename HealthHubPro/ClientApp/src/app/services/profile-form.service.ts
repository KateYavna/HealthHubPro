import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProfileFormModel } from '../models/ProfileFormModel.model';
import { Person } from '../models/Person.model';
import { Address } from '../models/Address.model';
import { PatientData } from '../models/PatientData.model';
import { EmergencyContact } from '../models/EmergencyContact.model';
import { Patient } from '../models/Patient.model';
import { AppointmentPostModel } from '../models/AppointmentPostModel.model';
import { PrescriptionPostModel } from '../models/PrescriptionPostModel.model';
import { PrescriptionHistory } from '../models/PrescriptionHistory.model';
import { HealthcareProviderData } from '../models/HealthcareProviderData.model';
import { HealthcareProvider } from '../models/HealthcareProvider.model';
import { Specialty } from '../models/Specialty.model';
import { Login } from '../models/Login.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileFormService {

  url: string=""

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'weatherforecast'
  }

  getAllRoles(): Observable<string[]> {
   return this.http.get<string[]>(this.url + '/roles');
  }

  getAllPatients(): Observable<Person[]> {
    return this.http.get<Person[]>(this.url + '/patients');
   }

  getAllHealthcareProviders(): Observable<Person[]> {
    return this.http.get<Person[]>(this.url + '/healthcareProviders');
   }

   getPatientData(id: string): Observable<PatientData> {
    return this.http.get<PatientData>(this.url + '/patientUpdate/' + id)
   }

   getHealthcareProviderData(id: string): Observable<HealthcareProviderData> {
    return this.http.get<HealthcareProviderData>(this.url + '/healthcareProviderUpdate/' + id)
   }

   login(model: Login): Observable<any>{
    return this.http.post(this.url + '/auth', model)
   }

  postFormData(model: ProfileFormModel): Observable<any>{
   return this.http.post(this.url + '/registration', model)
  }

  postAppointment(model: AppointmentPostModel): Observable<any>{
    return this.http.post(this.url + '/appointment', model)
  }

  postSpecialty(model: Specialty): Observable<any>{
    return this.http.post(this.url + '/specialty', model)
  }

  postPrescription(model: PrescriptionPostModel): Observable<any>{
    return this.http.post(this.url + '/prescription', model)
  }

  postPrescriptionHistory(model: PrescriptionHistory): Observable<any>{
    return this.http.post(this.url + '/prescriptionHistory', model)
  }

  editAppointment(model: AppointmentPostModel): Observable<any>{
    return this.http.put(this.url + '/appointment', model)
  }

  editPrescription(model: PrescriptionPostModel): Observable<any>{
    return this.http.put(this.url + '/prescription', model)
  }

  editPrescriptionHistory(model: PrescriptionHistory): Observable<any>{
    return this.http.put(this.url + '/prescriptionHistory', model)
  }

  editPerson(model: Person): Observable<any>{
    return this.http.put(this.url + '/person', model)
  }

  editAddress(model: Address): Observable<any>{
    return this.http.put(this.url + '/address', model)
  }

  editEmergencyContact(model: EmergencyContact): Observable<any>{
    return this.http.put(this.url + '/emergencyContact', model)
  }

  editPatient(model: Patient): Observable<any>{
    return this.http.put(this.url + '/patient', model)
  }

  editHealthcareProvider(model: HealthcareProvider): Observable<any>{
    return this.http.put(this.url + '/healthcareProvider', model)
  }

  deleteAppointment(id: string): Observable<any>{
    return this.http.delete(this.url + '/appointment/'+ id)
  }

  deletePrescription(id: string): Observable<any>{
    return this.http.delete(this.url + '/prescription/'+ id)
  }

  deletePrescriptionHistory(id: string): Observable<any>{
    return this.http.delete(this.url + '/prescriptionHistory/' + id)
  }
}
