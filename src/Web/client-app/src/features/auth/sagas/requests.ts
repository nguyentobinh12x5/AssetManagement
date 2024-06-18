import { AxiosResponse } from 'axios';
import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { ILoginCommand } from '../interfaces/ILoginCommand';
import { IChangePasswordFirstTimeCommand } from '../interfaces/IChangePasswordFirstTimeCommand';

export function login(
  loginCommand: ILoginCommand
): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(ENDPOINTS.AUTHORIZE, loginCommand, {
    params: {
      useCookies: loginCommand.useCookies ?? true,
    }
  });
}

export function getUserInfo() {
  return RequestService.axios.get(ENDPOINTS.USER_INFO);
}


export function changePasswordFirstTime(
  command: IChangePasswordFirstTimeCommand
): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(ENDPOINTS.CHANGE_PWD_FIRST_TIME, command);
}