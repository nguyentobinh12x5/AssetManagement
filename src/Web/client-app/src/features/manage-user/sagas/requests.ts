import { AxiosResponse } from 'axios';
import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import { IUserQuery } from '../interfaces/IUserQuery';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUser } from '../interfaces/IUser';

export function getUsers(
  userQuery: IUserQuery
): Promise<AxiosResponse<IPagedModel<IUser>>> {
  return RequestService.axios.get(
    `${ENDPOINTS.USER}?` +
      `&PageNumber=${userQuery.pageNumber}` +
      `&PageSize=${userQuery.pageSize}` +
      `&SortColumnName=${userQuery.sortColumnName}` +
      `&SortColumnDirection=${userQuery.sortColumnDirection}`
  );
}
