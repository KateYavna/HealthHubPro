export class AppointmentPostModel {
    id: string = "00000000-0000-0000-0000-000000000000"
    startTime: Date = new Date()
    endTime: Date = new Date()
    patientId: string = ""
    healthcareProviderId: string = ""
    appointmentType: string = ""
  }