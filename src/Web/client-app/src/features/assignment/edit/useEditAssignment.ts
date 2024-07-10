import { FormikHelpers } from 'formik';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { utcToDateString } from '../../../utils/dateUtils';
import { IEditAssignmentCommand } from '../interfaces/IEditAssignmentCommand';
import {
  EditAssignmentScheme,
  IEditAssignmentForm,
} from './edit-assignment-scheme';
import { editAssignment } from '../reducers/assignment-slice';
import { useEffect, useState } from 'react';
import { getAssignmentByIdWhenEdit } from '../reducers/assignment-detail-slice';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import { resetUserSlice } from '../../manager-user/reducers/user-slice';
import { resetAssetSlice } from '../../asset/reducers/asset-slice';

const useEditAssignment = (assignmentId: number) => {
  const dispatch = useAppDispatch();

  const { users, userQuery } = useAppState((state) => state.users);

  const { AssignmentDetail, isLoading } = useSelector(
    (state: RootState) => state.assignmentDetail
  );

  const [initialValues, setInitialValues] = useState<IEditAssignmentForm>({
    id: '',
    user: {
      id: '',
      fullName: '',
    },
    asset: {
      id: '',
      name: '',
    },
    assignedDate: '',
    note: '',
  });

  useEffect(() => {
    dispatch(getAssignmentByIdWhenEdit(assignmentId));

    return () => {
      dispatch(resetUserSlice());
      dispatch(resetAssetSlice());
    };
  }, [dispatch, assignmentId]);

  useEffect(() => {
    if (AssignmentDetail && userQuery.searchTerm) {
      setInitialValues({
        id: AssignmentDetail.id ?? '0',
        user: {
          id: users.items[0]?.id ?? '',
          fullName: users.items[0]?.fullName ?? '',
        },
        asset: {
          id: AssignmentDetail.assetId ?? '',
          name: AssignmentDetail.assetName ?? '',
        },
        assignedDate: new Date().toUTCString(),
        note: AssignmentDetail.note ?? '',
      });
    }
  }, [AssignmentDetail, dispatch, userQuery.searchTerm, users]);

  // useEffect(() => {
  //   if(AssignmentDetail && userQuery.searchTerm)
  //     dispatch(setUserQuery({
  //       ...userQuery,
  //       searchTerm: users.items[0]?.fullName
  //     }))

  //   return () => {

  //   }
  // }, [AssignmentDetail, dispatch, userQuery, userQuery.searchTerm])

  const handleSubmit = (
    values: IEditAssignmentForm,
    actions: FormikHelpers<IEditAssignmentForm>
  ) => {
    if (!values.assignedDate) return;

    const command: IEditAssignmentCommand = {
      id: values.id,
      assetId: values.asset.id,
      assignedDate: utcToDateString(values.assignedDate),
      note: values.note,
      userId: values.user.id,
    };
    dispatch(editAssignment(command));
    actions.setSubmitting(false);
  };

  return {
    isLoading,
    initialValues,
    handleSubmit,
    valdationSchema: EditAssignmentScheme,
  };
};

export default useEditAssignment;
