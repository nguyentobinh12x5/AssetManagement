import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';

import {
  IMyAssignmentBrief,
  IMyAssignmentQuery,
  IMySelectedAssignment,
} from '../interfaces/IMyAssignment';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { AxiosResponse } from 'axios';

export function getMyAssignmentsRequest(
  query: IMyAssignmentQuery
): Promise<IPagedModel<IMyAssignmentBrief>> {
  return RequestService.axios.get(ENDPOINTS.MY_ASSIGNMENTS, {
    params: query,
  });
}

export function updateStateAssignmentRequest(
  assignment: IMySelectedAssignment
): Promise<AxiosResponse<IMySelectedAssignment>> {
  return RequestService.axios.patch(
    `${ENDPOINTS.UPDATE_STATE_ASSIGNMENT}/${assignment.id}`,
    assignment
  );
}

export function returningAssignmentRequest(
  assignment: number
): Promise<AxiosResponse<number>> {
  return RequestService.axios.post(
    ENDPOINTS.CREATE_RETURNING_REQUEST, {
      "assignmentId": assignment
    }
  );
}