import { Appointment } from './Appointment.model';
import { Allergy } from './Allergy.model';
import { Prescription } from './Prescription.model';
import { Person } from './Person.model';

export class Patient {
    id: string =""
    personId: string =""
    allergies: Allergy[] =[];
    prescriptions: Prescription[] =[];
    person: Person = new Person()
  }