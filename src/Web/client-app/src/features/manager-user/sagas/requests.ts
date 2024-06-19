import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { AxiosResponse } from 'axios';
import { IUser } from '../interfaces/IUser';

export function editUser(user: IUser): Promise<AxiosResponse<IUser>> {
  return RequestService.axios.put(`${ENDPOINTS.USER}/${user.id}`, user);
}
export function CreateUser(user: IUser): Promise<AxiosResponse<IUser>> {
    return RequestService.axios.post(`${ENDPOINTS.USER}`, user);
}
export function getUserById(userId: string): Promise<AxiosResponse<IUser>> {
  return RequestService.axios.get(`${ENDPOINTS.USER}/${userId}`);
}
