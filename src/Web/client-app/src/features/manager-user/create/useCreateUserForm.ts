import { FormikHelpers } from 'formik';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { createUser, setSucceedStatus } from '../reducers/user-slice';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { IUserForm } from '../components/validateUserSchemas';

const useCreateUserForm = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { isLoading, succeed, error } = useAppState((state) => state.users);
  const user: IUserForm = {
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    joinDate: '',
    type: '',
    gender: 'Female',
  };

  const handleSubmit = (
    values: IUserForm,
    actions: FormikHelpers<IUserForm>
  ) => {
    const payload = {
      ...values,
      dateOfBirth: new Date(values.dateOfBirth).toISOString(),
      joinDate: new Date(values.joinDate).toISOString(),
    };
    dispatch(createUser(payload));
    actions.setSubmitting(false);
  };

  useEffect(() => {
    if (succeed && !isLoading && !error) {
      dispatch(setSucceedStatus(false));
      navigate('/user');
    }
  }, [succeed, navigate, error, isLoading]);

  return { user, isLoading, handleSubmit };
};

export default useCreateUserForm;
