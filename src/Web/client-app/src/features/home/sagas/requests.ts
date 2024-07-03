import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import {
  IMyAssignmentBrief,
  IMyAssignmentQuery,
} from '../interfaces/IMyAssignment';
import { IPagedModel } from '../../../interfaces/IPagedModel';

export function getMyAssignmentsRequest(
  query: IMyAssignmentQuery
): Promise<IPagedModel<IMyAssignmentBrief>> {
  return RequestService.axios.get(ENDPOINTS.MY_ASSIGNMENTS, {
    params: query,
  });
}
