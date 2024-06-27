import { useEffect, useState } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_ASSET_SORT_COLUMN } from '../constants/asset-sort';
import {
  getAssets,
  setAssetQuery,
  setIsDataFetched,
} from '../reducers/asset-slice';

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
    dispatch(setIsDataFetched(false));
  };

  const updateMainPagingState = (page: number) => {
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: page,
      })
    );
    dispatch(setIsDataFetched(false));
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_ASSET_SORT_COLUMN,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  const [filterStatus, setFilterStatus] = useState<string[] | null>(null);
  const [filterCategory, setFilterCategory] = useState<string[] | null>(null);
  const [searchTerm, setSearchTerm] = useState<string>('');

  const handleFilterByStatus = (status: string[]) => {
    setFilterStatus(status);
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: 1,
      })
    );
    dispatch(setIsDataFetched(false));
  };

  const handleFilterByCategory = (category: string[]) => {
    setFilterCategory(category);
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: 1,
      })
    );
    dispatch(setIsDataFetched(false));
  };

  const handleSearch = (searchTerm: string) => {
    setSearchTerm(searchTerm.trim());
    dispatch(
      setAssetQuery({
        ...assetQuery,
        pageNumber: 1,
      })
    );
    dispatch(setIsDataFetched(false));
  };

  // Fetch Data
  useEffect(() => {
    const fetchData = () => {
      dispatch(
        getAssets({
          ...assetQuery,
          category: filterCategory ?? [''],
          assetStatus: filterStatus ?? [
            'Assigned',
            'Available',
            'Not available',
          ],
          searchTerm: searchTerm,
        })
      );
    };

    if (!isDataFetched) {
      fetchData();
    }
  }, [
    dispatch,
    assetQuery,
    isDataFetched,
    filterStatus,
    filterCategory,
    searchTerm,
  ]);

  return {
    hasSortColumn,
    assets,
    searchTerm,
    handleSort,
    handlePaging,
    handleFilterByStatus,
    handleFilterByCategory,
    handleSearch,
  };
};

export default useAssetList;
