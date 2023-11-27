export class PrescriptionPostModel {
    id: string = "00000000-0000-0000-0000-000000000000"
    issueDate: Date = new Date()
    patientId: string = ""
    healthcareProviderId: string = ""
    medication: string = ""
    dosage: string = ""
  }