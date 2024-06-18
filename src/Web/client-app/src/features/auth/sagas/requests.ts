import ENDPOINTS from '../../../constants/endpoints';
import RequestService from '../../../services/request';

export function login() {
  return RequestService.axios.post(ENDPOINTS.AUTHORIZE);
}
