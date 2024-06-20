import { IUserQuery } from './common/IUserQuery';

export interface IUserTypeQuery extends IUserQuery {
  type: string[];
}
