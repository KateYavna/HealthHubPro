import { HealthcareProvider } from "./HealthcareProvider.model";
import { PrescriptionHistory } from "./PrescriptionHistory.model";

export class Prescription {
    id: string = "00000000-0000-0000-0000-000000000000"
    issueDate: Date = new Date()
    patientId: string = ""
    healthcareProviderId: string = ""
    medication: string = ""
    dosage: string = ""
    prescriptionHistory: PrescriptionHistory[] = [];
    healthcareProvider: HealthcareProvider = new HealthcareProvider()
    createdAt?: Date;
    updatedAt?: Date;
    isDeleted: boolean = false
  }