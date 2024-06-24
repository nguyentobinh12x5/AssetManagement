import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { AxiosResponse } from 'axios';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IBriefAsset } from '../interfaces/IBriefAsset';

export function getAssetsRequest(
  assetQuery: IAssetQuery
): Promise<AxiosResponse<IPagedModel<IBriefAsset>>> {
  return RequestService.axios.get(
    `${ENDPOINTS.ASSETS}?` +
      `CategoryName=${assetQuery.categoryName}` +
      `&AssetStatusName=${assetQuery.assetStatusName}` +
      `&PageNumber=${assetQuery.pageNumber}` +
      `&PageSize=${assetQuery.pageSize}` +
      `&SortColumnName=${assetQuery.sortColumnName}` +
      `&SortColumnDirection=${assetQuery.sortColumnDirection}` +
      `&SearchTerm=${assetQuery.searchTerm}`
  );
}

export function getAssetStatusesRequest(): Promise<AxiosResponse<string[]>> {
  return RequestService.axios.get(`${ENDPOINTS.ASSETS}/Status`);
}

export function getAssetCategoriesRequest(): Promise<AxiosResponse<string[]>> {
  return RequestService.axios.get(`${ENDPOINTS.ASSETS}/Categories`);
}
