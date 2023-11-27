export class Person {
    id: string =""
    firstName: string =""
    lastName: string =""
    dateOfBirth: Date = new Date();
    gender: string =""
    addressId: string =""
    emergencyContactId: string =""
    passwordId: string =""
    phoneNumber: string =""
    email: string =""
    roles: string[] = [];
    createdAt?: Date;
    updatedAt?: Date;
    isDeleted: boolean = false;
  }