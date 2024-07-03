import { FormikHelpers } from 'formik';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { editUser, getUserById } from '../reducers/user-slice';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { IUserForm } from '../components/validateUserSchemas';
import { utcToDateString } from '../../../utils/dateUtils';

const useEditForm = () => {
  const { userId } = useParams<{ userId: string }>();
  const dispatch = useAppDispatch();
  const { user, isLoading } = useAppState((state) => state.users);

  useEffect(() => {
    if (userId) {
      dispatch(getUserById(userId));
    }
  }, [dispatch, userId]);

  const handleSubmit = (
    values: IUserForm,
    actions: FormikHelpers<IUserForm>
  ) => {
    dispatch(
      editUser({
        ...values,
        dateOfBirth: utcToDateString(values.dateOfBirth),
        joinDate: utcToDateString(values.joinDate),
      })
    );
    actions.setSubmitting(false);
  };

  return { user, isLoading, handleSubmit };
};

export default useEditForm;
