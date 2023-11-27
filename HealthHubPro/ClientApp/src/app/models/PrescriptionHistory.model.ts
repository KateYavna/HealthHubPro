export class PrescriptionHistory {
    id: string ="00000000-0000-0000-0000-000000000000"
    quantityDispensed: number = 0
    dispenseDate: Date = new Date()
    prescriptionId: string =""
    createdAt?: Date;
    updatedAt?: Date;
    isDeleted: boolean = false;
  }