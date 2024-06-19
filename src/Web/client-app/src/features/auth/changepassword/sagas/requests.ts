import { AxiosResponse } from 'axios';
import RequestService from '../../../../services/request';
import ENDPOINTS from '../../../../../src/constants/endpoints';

interface ChangePasswordPayload {
  currentPassword: string;
  newPassword: string;
}

export function changePassword(
  payload: ChangePasswordPayload
): Promise<AxiosResponse<void>> {
  return RequestService.axios.post(`${ENDPOINTS.CHANGE_PASSWORD}`, payload);
}
