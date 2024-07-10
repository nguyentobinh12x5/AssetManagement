import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_ASSET_SORT_COLUMN } from '../constants/asset-sort';
import { getAssets, setAssetQuery } from '../reducers/asset-slice';

const useAssetList = () => {
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

  const handleFilterByStatus = (status: string[]) => {
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: 1,
        assetStatus: status,
      })
    );
  };

  const handleFilterByCategory = (category: string[]) => {
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: 1,
        category: category,
      })
    );
  };

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
    if (!isDataFetched) {
      dispatch(getAssets(assetQuery));
    }
  }, [dispatch, assetQuery, isDataFetched]);

  return {
    hasSortColumn,
    assets,
    searchTerm: !isDataFetched ? assetQuery.searchTerm : ``,
    handleSort,
    handlePaging,
    handleFilterByStatus,
    handleFilterByCategory,
    handleSearch,
  };
};

export default useAssetList;
