import { FormikHelpers } from 'formik';
import { IEditAssetForm } from './edit-asset-scheme';
import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import {
  editAsset,
  getAssetCategories,
  getAssetStatuses,
} from '../reducers/asset-slice';
import { getAssetById, resetState } from '../reducers/asset-detail-slice';
import { useEffect } from 'react';
import { utcToDateString } from '../../../utils/dateUtils';

const useEditAsset = (assetId: string) => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const { categories, statuses } = useAppState((state) => ({
    assets: state.assets,
    categories: state.assets.categories,
    statuses: state.assets.statuses,
  }));

  const { assetDetail } = useSelector((state: RootState) => state.assetDetail);

  useEffect(() => {
    dispatch(getAssetById(assetId));
    dispatch(getAssetCategories());
    dispatch(getAssetStatuses());

    return () => {
      dispatch(resetState());
    };
  }, [dispatch, assetId]);

  const handleSubmit = (
    values: IEditAssetForm,
    actions: FormikHelpers<IEditAssetForm>
  ) => {
    if (!values.installedDate || !assetId) return;

    dispatch(
      editAsset({
        ...values,
        id: assetId ? parseInt(assetId) : 0,
        installedDate: utcToDateString(values.installedDate),
      })
    );
    actions.setSubmitting(false);
  };

  return {
    assetDetail,
    handleSubmit,
    navigate,
    categories: categories.filter((c) => c !== 'All'),
    statuses: statuses.filter((c) =>
      [
        'Available',
        'Not available',
        'Recycled',
        'Waiting for Recycling',
      ].includes(c)
    ),
  };
};

export default useEditAsset;
