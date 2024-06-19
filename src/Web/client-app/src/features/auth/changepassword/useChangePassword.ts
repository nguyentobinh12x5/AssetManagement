import { useEffect, useState, useCallback } from 'react';
import { FormikHelpers } from 'formik';
import { useDispatch, useSelector } from 'react-redux';
import {
  changePasswordRequest,
  resetChangePasswordState,
} from './reducers/change-password-slice';
import {
  ChangePasswordType,
  ChangePasswordSchema,
} from './ChangePasswordSchema';
import { RootState } from '../../../redux/store';

const useChangePassword = () => {
  const dispatch = useDispatch();
  const [success, setSuccess] = useState(false);
  const { status } = useSelector((state: RootState) => state.changePassword);
  const [apiError, setApiError] = useState<{
    currentPassword: string;
    newPassword: string;
  }>({ currentPassword: '', newPassword: '' });

  const initialValues: ChangePasswordType = {
    currentPassword: '',
    newPassword: '',
  };

  const handleSubmit = (
    values: ChangePasswordType,
    actions: FormikHelpers<ChangePasswordType>
  ) => {
    dispatch(changePasswordRequest({ values, actions, setApiError }));
  };

  const resetState = useCallback(() => {
    setSuccess(false);
    setApiError({ currentPassword: '', newPassword: '' });
    dispatch(resetChangePasswordState());
  }, [dispatch]);

  useEffect(() => {
    if (status === 204) {
      setSuccess(true);
    }
  }, [status]);

  return {
    initialValues,
    handleSubmit,
    ChangePasswordSchema,
    success,
    apiError,
    resetState,
  };
};

export default useChangePassword;
