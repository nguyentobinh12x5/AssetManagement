import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import { AxiosResponse } from 'axios';
import { IAssignmentDetail } from '../interfaces/IAssignmentDetail';
import { ICreateAssignmentCommand } from '../interfaces/ICreateAssignmentCommand';
import { IAssignmentQuery } from '../interfaces/commom/IAssigmentQuery';
import { IBriefAssignment } from '../interfaces/IBriefAssignment';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { paramsSerializer } from '../../../utils/appUtils';

export function getAssignmentsRequest(
  assignmentQuery: IAssignmentQuery
): Promise<AxiosResponse<IPagedModel<IBriefAssignment>>> {
  return RequestService.axios.get(`${ENDPOINTS.ASSIGNMENTS}`, {
    params: assignmentQuery,
    paramsSerializer: paramsSerializer,
  });
}

export function getAssignmentByIdRequest(
  id: number
): Promise<AxiosResponse<IAssignmentDetail>> {
  return RequestService.axios.get<IAssignmentDetail>(
    `${ENDPOINTS.ASSIGNMENTS}/${id}`
  );
}

export function createAssignmentRequest(
  request: ICreateAssignmentCommand
): Promise<AxiosResponse<number>> {
  return RequestService.axios.post(`${ENDPOINTS.ASSIGNMENTS}`, request);
}

export function deleteAssigmentRequest(
  id: number
): Promise<AxiosResponse<IAssignmentDetail>> {
  return RequestService.axios.delete(`${ENDPOINTS.ASSIGNMENTS}/${id}`);
}
