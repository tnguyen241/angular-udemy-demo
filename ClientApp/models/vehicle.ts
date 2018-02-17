export interface Vehicle {
    id: number;
    model: Model;
    make: Make;
    isRegistered: boolean;
    contact: Contact;
    features: Feature[];
  }
  
  interface Feature {
    vehicleId: number;
    featureId: number;
  }
  
  interface Contact {
    name: string;
    email: string;
    phone: string;
  }
  
 export interface Make {
    id: number;
    name: string;
    models: Model[];
  }
  
  interface Model {
    id: number;
    name: string;
  }
  export interface Filter{
    makeId?:number;
    modelId?:number;
    sortBy:string;
    isSortAscending:boolean;
  }
