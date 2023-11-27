import { Specialty } from '../models/Specialty.model';
import { Appointment } from './Appointment.model';
import { Person } from '../models/Person.model';

export class HealthcareProvider {
  id: string = ""
  personId: string = ""
  specialties: Specialty[] = []
  person: Person = new Person()
  createdAt?: Date;
  updatedAt?: Date;
  isDeleted: boolean = false
}
