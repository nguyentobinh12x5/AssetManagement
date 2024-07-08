import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';

export function getReturingsRequest(query: any): Promise<any> {
  return RequestService.axios.get(ENDPOINTS.RETURNINGS);
}
