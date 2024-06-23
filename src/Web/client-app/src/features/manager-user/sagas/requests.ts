import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { AxiosResponse } from 'axios';
import { IUserCommand } from '../interfaces/IUserCommand';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { IUserTypeQuery } from '../interfaces/IUserTypeQuery';
import { IUserSearchQuery } from '../interfaces/IUserSearchQuery';
import { IUserDetail } from '../interfaces/IUserDetail';

export function editUser(
  user: IUserCommand
): Promise<AxiosResponse<IUserCommand>> {
  return RequestService.axios.put(`${ENDPOINTS.USER}/${user.id}`, user);
}
export function CreateUser(
  user: IUserCommand
): Promise<AxiosResponse<IUserCommand>> {
  return RequestService.axios.post(`${ENDPOINTS.USER}`, user);
}
export function getUserById(
  userId: string
): Promise<AxiosResponse<IUserDetail>> {
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
      `/Type?` +
      `&Types=${userQuery.type}` +
      `&PageNumber=${userQuery.pageNumber}` +
      `&PageSize=${userQuery.pageSize}` +
      `&SortColumnName=${userQuery.sortColumnName}` +
      `&SortColumnDirection=${userQuery.sortColumnDirection}`
  );
}

export function getUserBySearchTerm(
  userQuery: IUserSearchQuery
): Promise<AxiosResponse<IPagedModel<IBriefUser>>> {
  return RequestService.axios.get(
    `${ENDPOINTS.USER}` +
      `/Search?` +
      `&SearchTerm=${userQuery.searchTerm}` +
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
