import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { AxiosResponse } from 'axios';
import { IUser } from '../interfaces/IUser';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/IUserQuery';
import { IUserTypeQuery } from '../interfaces/IUserTypeQuery';

export function editUser(user: IUser): Promise<AxiosResponse<IUser>> {
  return RequestService.axios.put(`${ENDPOINTS.USER}/${user.id}`, user);
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

export function getUsersByType(
  userQuery: IUserTypeQuery
): Promise<AxiosResponse<IPagedModel<IBriefUser>>> {
  return RequestService.axios.get(
    `${ENDPOINTS.USER}` +
      `/type?` +
      `&Type=${userQuery.type}` +
      `&PageNumber=${userQuery.pageNumber}` +
      `&PageSize=${userQuery.pageSize}` +
      `&SortColumnName=${userQuery.sortColumnName}` +
      `&SortColumnDirection=${userQuery.sortColumnDirection}`
  );
}

// Function to delete a user by ID
export function deleteUserRequest(userId: string) {
  return RequestService.axios.delete(`${ENDPOINTS.DELETE_USER}/${userId}`);
}
