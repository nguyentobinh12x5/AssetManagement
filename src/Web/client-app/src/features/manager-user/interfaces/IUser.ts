export interface IUser {
  id?: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  joinDate: string;
  type: string;
}

export interface IUserDetail {
    id?: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    gender: string;
    joinDate: string;
    type: string;
    username: string;
    location: string;
    staffCode: string;
}
