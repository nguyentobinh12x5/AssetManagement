import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import { ICreateAssetCommand } from '../interfaces/ICreateAssetCommand';
import { IEditAssetCommand } from '../interfaces/IEditAssetCommand';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { AxiosResponse } from 'axios';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IBriefAsset } from '../interfaces/IBriefAsset';
import { IAssetDetail } from '../interfaces/IAssetDetail';
import { paramsSerializer } from '../../../utils/appUtils';
import { ICheckHistoricalAssignment } from '../interfaces/ICheckHistoricalAssignment';

export function getAssetsRequest(
  assetQuery: IAssetQuery
): Promise<AxiosResponse<IPagedModel<IBriefAsset>>> {
  return RequestService.axios.get(`${ENDPOINTS.ASSETS}?`, {
    params: assetQuery,
    paramsSerializer: paramsSerializer,
  });
}

export function getAssetStatusesRequest(): Promise<AxiosResponse<string[]>> {
  return RequestService.axios.get(`${ENDPOINTS.ASSETS}/Status`);
}

export function getAssetCategoriesRequest(): Promise<AxiosResponse<string[]>> {
  return RequestService.axios.get(`${ENDPOINTS.ASSETS}/Categories`);
}

export function createAssetRequest(
  command: ICreateAssetCommand
): Promise<AxiosResponse<number>> {
  return RequestService.axios.post<number>(ENDPOINTS.ASSETS, command);
}

export function getAssetByIdRequest(
  id: number
): Promise<AxiosResponse<IAssetDetail>> {
  return RequestService.axios.get<IAssetDetail>(`${ENDPOINTS.ASSETS}/${id}`);
}

export function editAssetRequest(
  command: IEditAssetCommand
): Promise<AxiosResponse<IAssetDetail>> {
  return RequestService.axios.put<IAssetDetail>(
    `${ENDPOINTS.ASSETS}/${command.id}`,
    command
  );
}

export function deleteAssetRequest(
  id: number
): Promise<AxiosResponse<IAssetDetail>> {
  return RequestService.axios.delete(`${ENDPOINTS.ASSETS}/${id}`);
}

export function checkHistoricalAssignmentRequest(
  data: ICheckHistoricalAssignment
): Promise<AxiosResponse<number>> {
  return RequestService.axios.get(
    `${ENDPOINTS.HISTORYCAL_ASSIGNMENT}/${data.id}`
  );
}
