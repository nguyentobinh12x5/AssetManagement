import { useEffect } from 'react';
import { getAssets, setAssetQuery } from '../../../asset/reducers/asset-slice';
import useAppPaging from '../../../../hooks/paging/useAppPaging';
import { DEFAULT_MANAGE_ASSET_SORT_COLUMN } from '../../../asset/constants/asset-sort';
import useAppSort from '../../../../hooks/paging/useAppSort';
import { useAppDispatch, useAppState } from '../../../../redux/redux-hooks';

const useAssetRadioSelect = () => {
  const dispatch = useAppDispatch();
  const { assets, assetQuery, isDataFetched } = useAppState(
    (state) => state.assets
  );

  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    dispatch(
      setAssetQuery({
        ...assetQuery,
        sortColumnName,
        sortColumnDirection,
      })
    );
  };

  const updateMainPagingState = (page: number) => {
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: page,
      })
    );
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_ASSET_SORT_COLUMN,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  const handleSearch = (searchTerm: string) => {
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: 1,
        searchTerm: searchTerm.trim(),
      })
    );
  };

  // Fetch Data
  useEffect(() => {
    dispatch(
      getAssets({
        ...assetQuery,
        assetStatus: ['Available'],
      })
    );
  }, [dispatch, assetQuery]);

  return {
    hasSortColumn,
    assets,
    searchTerm: assetQuery.searchTerm,
    handleSort,
    handlePaging,
    handleSearch,
    sortState: {
      name: hasSortColumn.sortColumn,
      orderBy: hasSortColumn.sortOrder,
    },
  };
};

export default useAssetRadioSelect;
