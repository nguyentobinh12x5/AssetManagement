import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import { IReturningQuery } from '../interfaces/Common/IReturningQuery';
import { IBriefReturning } from '../interfaces/IBriefReturning';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { AxiosResponse } from 'axios';
import { paramsSerializer } from '../../../utils/appUtils';

export function getReturingsRequest(returningQuery: IReturningQuery): Promise<AxiosResponse<IPagedModel<IBriefReturning>>> {
    return RequestService.axios.get(ENDPOINTS.RETURNINGS, {
        params: returningQuery,
        paramsSerializer: paramsSerializer,
    });
}
