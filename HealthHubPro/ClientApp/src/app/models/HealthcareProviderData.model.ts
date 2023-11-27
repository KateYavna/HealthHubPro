import { Specialty } from '../models/Specialty.model';
import { Appointment } from './Appointment.model';
import { Person } from '../models/Person.model';
import { Address } from './Address.model';
import { EmergencyContact } from './EmergencyContact.model';
import { HealthcareProvider } from './HealthcareProvider.model';

export interface HealthcareProviderData {
  commonSpecialties: Specialty[];
  person: Person ;
  address: Address;
  emergencyContact: EmergencyContact;
  healthcareProvider: HealthcareProvider;
  appointments: Appointment[];
}