import { HealthcareProvider } from "./HealthcareProvider.model"
import { Patient } from "./Patient.model"

export class Appointment {
    id: string = "00000000-0000-0000-0000-000000000000"
    startTime: Date = new Date()
    endTime: Date = new Date()
    patientId: string = ""
    patient: Patient = new Patient()
    healthcareProviderId: string = ""
    healthcareProvider: HealthcareProvider = new HealthcareProvider()
    appointmentType: string = ""
    createdAt?: Date
    updatedAt?: Date 
    isDeleted: boolean = false
  }