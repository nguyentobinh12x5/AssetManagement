import { AxiosResponse } from 'axios';
import RequestService from '../../../services/request';
import axios from 'axios';
import ENDPOINTS from '../../../constants/endpoints';
import { IUserQuery } from '../interfaces/IUserQuery';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUser } from '../interfaces/IUser';

// Function to get users based on the query
export function getUsers(
    userQuery: IUserQuery
): Promise<AxiosResponse<IPagedModel<IUser>>> {
    return RequestService.axios.get(
        `${ENDPOINTS.USER}?` +
        `PageNumber=${userQuery.pageNumber}` +
        `&PageSize=${userQuery.pageSize}` +
        `&SortColumnName=${userQuery.sortColumnName}` +
        `&SortColumnDirection=${userQuery.sortColumnDirection}`
    );
}

// Function to delete a user by ID
export function deleteUserRequest(userId: string) {
    return RequestService.axios.delete(`${ENDPOINTS.DELETE_USER}/${userId}`);
}
