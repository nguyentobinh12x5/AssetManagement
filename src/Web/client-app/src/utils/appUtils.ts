import { ASCENDING, DESCENDING } from "../constants/paging";
import ISort from "../interfaces/ISort"

const getSortOrder = (hasQuery: ISort, sortColumn: string): string => {
    let sortOrder;
    if(hasQuery.sortColumn != sortColumn){
        sortOrder = ASCENDING
    }else{
        sortOrder = hasQuery.sortOrder === ASCENDING ? DESCENDING : ASCENDING;
    }
    return sortOrder;
}

export {
    getSortOrder
}