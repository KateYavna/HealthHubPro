import { Allergy } from './Allergy.model';
import { Person } from './Person.model';
import { Address } from './Address.model';
import { EmergencyContact } from './EmergencyContact.model';
import { HealthcareProvider } from './HealthcareProvider.model';
import { Patient } from './Patient.model';
import { Appointment } from './Appointment.model';
import { Prescription } from './Prescription.model';

export interface PatientData {
    person: Person;
    address: Address;
    emergencyContact: EmergencyContact;
    patient: Patient;
    commonAllergies: Allergy[];
    healthcareProviders: HealthcareProvider[];
    appointments: Appointment[];
    prescriptions: Prescription[];
  }