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

const useCreateAsset = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { statuses, categories } = useAppState((state) => state.assets);
  const { user } = useAppState((state) => state.auth);

  const initialValues: ICreateAssetForm = {
    name: '',
    specification: '',
    category: '',
    installedDate: new Date(),
    state: 'Available',
  };

  const handleSubmit = (
    values: ICreateAssetForm,
    actions: FormikHelpers<ICreateAssetForm>
  ) => {
    dispatch(
      createAsset({
        ...values,
        installedDate: values.installedDate.toUTCString(),
        location: user?.location ?? '',
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
