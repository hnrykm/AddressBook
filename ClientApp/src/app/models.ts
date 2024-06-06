export interface Person {
  id: string;
  firstName: string;
  lastName: string;
  dob?: Date;
  address?: Address;
}

export interface Address {
  line1: string;
  line2?: string;
  city: string;
  state: string;
  zipCode: string;
}
