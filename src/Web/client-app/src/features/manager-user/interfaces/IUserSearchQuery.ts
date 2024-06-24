import { IUserQuery } from './common/IUserQuery';

export interface IUserSearchQuery extends IUserQuery {
  searchTerm: string;
}
