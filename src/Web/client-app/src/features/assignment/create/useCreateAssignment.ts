import { FormikHelpers } from 'formik';
import { useAppDispatch } from '../../../redux/redux-hooks';
import {
  ICreateAssignmentForm,
  CreateAssignmentScheme,
} from './create-assignment-scheme';
import { ICreateAssignmentCommand } from '../interfaces/ICreateAssignmentCommand';
import { createAssignment } from '../reducers/assignment-slice';
import { utcToDateString } from '../../../utils/dateUtils';
import { useEffect } from 'react';
import { resetUserSlice } from '../../manager-user/reducers/user-slice';
import { resetAssetSlice } from '../../asset/reducers/asset-slice';

const useCreateAssignment = () => {
  const dispatch = useAppDispatch();

  const initialValues: ICreateAssignmentForm = {
    user: {
      id: '',
      fullName: '',
    },
    asset: {
      id: '',
      name: '',
    },
    assignedDate: new Date().toISOString(),
    note: '',
  };

  const handleSubmit = (
    values: ICreateAssignmentForm,
    actions: FormikHelpers<ICreateAssignmentForm>
  ) => {
    if (!values.assignedDate) return;

    const command: ICreateAssignmentCommand = {
      assetId: values.asset.id,
      assignedDate: utcToDateString(values.assignedDate),
      note: values.note,
      userId: values.user.id,
    };
    dispatch(createAssignment(command));
    actions.setSubmitting(false);
  };

  useEffect(() => {
    return () => {
      dispatch(resetUserSlice());
      dispatch(resetAssetSlice());
    };
  }, [dispatch]);

  return {
    initialValues,
    handleSubmit,
    valdationSchema: CreateAssignmentScheme,
  };
};

export default useCreateAssignment;
