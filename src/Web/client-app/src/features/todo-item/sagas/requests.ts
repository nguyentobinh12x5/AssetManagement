import { AxiosResponse } from 'axios';
import RequestService from '../../../services/request';
import ENDPOINTS from '../../../constants/endpoints';
import { ITodoQuery } from '../interfaces/ITodoQuery';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { ITodoItem } from '../interfaces/ITodoItem';

export function getTodoItems(
    todoQuery: ITodoQuery
): Promise<AxiosResponse<IPagedModel<ITodoItem>>> {
    return RequestService.axios.get(
        `${ENDPOINTS.TODOITEM}?` +
        `ListId=${todoQuery.listId}` +
        `&PageNumber=${todoQuery.pageNumber}` +
        `&PageSize=${todoQuery.pageSize}` +
        `&SortColumnName=${todoQuery.sortColumnName}` +
        `&SortColumnDirection=${todoQuery.sortColumnDirection}`

    );
}