export interface ICompleteProfilePayload {
    birthdate: string | null; 
    address?: string;
    phoneNo?: string;
    currentDesignationId: number;
    targetDesignationId: number;
    professionId: number;
    skillIds: number[];
  }