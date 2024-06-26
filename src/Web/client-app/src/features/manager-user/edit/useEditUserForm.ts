import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import {
  editUser,
  getUserById,
  setSucceedStatus,
} from '../reducers/user-slice';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { IUserForm } from '../components/validateUserSchemas';

const useEditForm = () => {
  const { userId } = useParams<{ userId: string }>();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { user, isLoading, succeed, error } = useAppState(
    (state) => state.users
  );

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

  useEffect(() => {
    if (succeed && !isLoading && !error) {
      dispatch(setSucceedStatus(false));
      navigate('/user');
    }
  }, [succeed, navigate, isLoading, error, dispatch]);

  return { user, isLoading, handleSubmit };
};

export default useEditForm;
