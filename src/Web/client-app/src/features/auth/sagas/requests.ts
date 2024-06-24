import { AxiosResponse } from 'axios';
import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { ILoginCommand } from '../interfaces/ILoginCommand';
import { IChangePasswordFirstTimeCommand } from '../interfaces/IChangePasswordFirstTimeCommand';
import { IUserInfo } from '../interfaces/IUserInfo';

export function login(
  loginCommand: ILoginCommand
): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(ENDPOINTS.AUTHORIZE, loginCommand, {
    params: {
      useCookies: loginCommand.useCookies ?? true,
    },
  });
}

export function getUserInfo(): Promise<AxiosResponse<IUserInfo>> {
  return RequestService.axios.get<IUserInfo>(ENDPOINTS.USER_INFO);
}

export function changePasswordFirstTime(
  command: IChangePasswordFirstTimeCommand
): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(ENDPOINTS.CHANGE_PWD_FIRST_TIME, command);
}

export function logout(): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(ENDPOINTS.LOGOUT);
}
