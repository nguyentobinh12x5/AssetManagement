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
    const { setSubmitting, setTouched, setErrors } = actions;
    ChangePasswordSchema.validate(values, { abortEarly: false })
      .then(() => {
        // Proceed with form submission
        dispatch(changePasswordRequest({ values, actions, setApiError }));

        // Form submission logic
        setSubmitting(false);
      })
      .catch((errors) => {
        const formErrors = errors.inner.reduce((acc: any, err: any) => {
          acc[err.path] = err.message;
          return acc;
        }, {});
        setErrors(formErrors);
        setTouched({
          currentPassword: true,
          newPassword: true,
        });
        setSubmitting(false);
      });
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
