import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { getAssets } from '../reducers/asset-slice';

const useFetchAssetList = () => {
  const dispatch = useAppDispatch();
  const { assets, assetQuery, isDataFetched } = useAppState(
    (state) => state.assets
  );

  // Fetch Data
  useEffect(() => {
    if (!isDataFetched) {
      dispatch(getAssets(assetQuery));
    }
  }, [dispatch, assetQuery, isDataFetched]);

  return { assets, assetQuery };
};

export default useFetchAssetList;
