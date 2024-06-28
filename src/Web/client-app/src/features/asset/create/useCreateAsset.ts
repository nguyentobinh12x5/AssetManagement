import { FormikHelpers } from 'formik';
import { ICreateAssetForm } from './create-asset-scheme';
import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import {
  createAsset,
  getAssetCategories,
  getAssetStatuses,
} from '../reducers/asset-slice';
import { useEffect } from 'react';
import { utcToDateString } from '../../../utils/dateUtils';

const useCreateAsset = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { statuses, categories } = useAppState((state) => state.assets);

  const initialValues: ICreateAssetForm = {
    name: '',
    specification: '',
    category: '',
    state: 'Available',
    installedDate: '',
  };

  const handleSubmit = (
    values: ICreateAssetForm,
    actions: FormikHelpers<ICreateAssetForm>
  ) => {
    if (!values.installedDate) return;
    dispatch(
      createAsset({
        ...values,
        installedDate: utcToDateString(values.installedDate),
      })
    );
    actions.setSubmitting(false);
  };

  useEffect(() => {
    if (statuses.length <= 1) dispatch(getAssetStatuses());

    if (categories.length <= 1) dispatch(getAssetCategories());
  }, [dispatch, categories, statuses]);

  return {
    initialValues,
    handleSubmit,
    navigate,
    categories: categories.filter((c) => c !== 'All'),
    statuses: statuses.filter((c) =>
      ['Available', 'Not available'].includes(c)
    ),
  };
};

export default useCreateAsset;
