import { FormikHelpers } from 'formik';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { editUser, getUserById } from '../reducers/user-slice';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { IUserForm } from '../components/validateUserSchemas';

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
    dispatch(editUser(values));
    actions.setSubmitting(false);
  };

  return { user, isLoading, handleSubmit };
};

export default useEditForm;
