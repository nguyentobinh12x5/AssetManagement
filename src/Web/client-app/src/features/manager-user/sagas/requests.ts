import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { AxiosResponse } from 'axios';
import { IUser } from '../interfaces/IUser';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/IUserQuery';

export function editUser(user: IUser): Promise<AxiosResponse<IUser>> {
  return RequestService.axios.put(`${ENDPOINTS.USER}/${user.id}`, user);
}
export function CreateUser(user: IUser): Promise<AxiosResponse<IUser>> {
    return RequestService.axios.post(`${ENDPOINTS.USER}`, user);
}
export function getUserById(userId: string): Promise<AxiosResponse<IUser>> {
  return RequestService.axios.get(`${ENDPOINTS.USER}/${userId}`);
}
export function getUsers(
  userQuery: IUserQuery
): Promise<AxiosResponse<IPagedModel<IBriefUser>>> {
  return RequestService.axios.get(
    `${ENDPOINTS.USER}?` +
      `&PageNumber=${userQuery.pageNumber}` +
      `&PageSize=${userQuery.pageSize}` +
      `&SortColumnName=${userQuery.sortColumnName}` +
      `&SortColumnDirection=${userQuery.sortColumnDirection}`
  );
}
