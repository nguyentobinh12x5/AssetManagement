import { AxiosResponse } from 'axios';
import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';
import { ILoginCommand } from '../interfaces/ILoginCommand';

export function login(
  loginCommand: ILoginCommand
): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(ENDPOINTS.AUTHORIZE, loginCommand, {
    params: {
      useCookies: loginCommand.useCookies ?? true,
    },
    withCredentials: true,
  });
}

export function getUserInfo() {
  return RequestService.axios.get(ENDPOINTS.USER_INFO, {
    withCredentials: true,
  });
}
