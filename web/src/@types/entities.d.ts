export interface IUserSignUp {
  name: string;
  email: string;
  role: 1 | 2;
  phoneNumber: string;
}

export interface IUser {
  id: number;
  name: string;
  email: string;
  role: 'Negotiator' | 'Customer';
  phoneNumber: string;
}

export interface IDebtParcelList {
  id?: number;
  debtId?: number;
  parcel: number;
  value?: number;
  dueDate?: string;
  paid: boolean;
}

export interface IDebtList {
  id: number;
  customerName: string;
  negotiatorName: string;
  negotiatorPhone: string;
  description: string;
  originalValue: number;
  recalculatedValue: number;
  dueDate: string;
  calculationDate: string;
  parcelsQty: number;
  interestType: number;
  interestInterval: number;
  interestPercentage: number;
  negotiatorComissionPercentage: number;
  parcels: IDebtParcelList[];
  paid: boolean;
}

export interface IDebtUpdateParcel {
  parcel: number;
  paid: boolean;
}

export interface IDebtUpdate {
  id: number;
  customerName: string;
  negotiatorName: string;
  negotiatorPhone: string;
  description: string;
  negotiatorComissionPercentage: number;
  paid: boolean;
  parcels: IDebtUpdateParcel[];
}

export interface INewDebt {
  customerId: number;
  description: string;
  originalValue: number;
  dueDate: string;
  parcelsQty: number;
  interestType: number;
  interestInterval: number;
  interestPercentage: number;
  negotiatorComissionPercentage: number;
}
