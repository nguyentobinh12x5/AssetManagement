import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { changePasswordFirstTime } from '../reducers/auth-slice';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const ChangePasswordFirstimeSchema = Yup.object().shape({
  newPassword: Yup.string()
    .required('Required')
    .min(6, 'New password must be at least 6 characters long.')
    .matches(
      /[A-Z]/,
      'New password must contain at least one uppercase letter.'
    )
    .matches(
      /[a-z]/,
      'New password must contain at least one lowercase letter.'
    )
    .matches(/[0-9]/, 'New password must contain at least one digit.')
    .matches(
      /[^a-zA-Z0-9]/,
      'New password must contain at least one non-alphanumeric character.'
    ),
});
export interface IChangePasswordFirstTime
  extends Yup.InferType<typeof ChangePasswordFirstimeSchema> {}

const useFirstTimeLogin = () => {
  const dispatch = useAppDispatch();
  const initialValues: IChangePasswordFirstTime = { newPassword: '' };

  const handleSubmit = (
    values: IChangePasswordFirstTime,
    actions: FormikHelpers<IChangePasswordFirstTime>
  ) => {
    dispatch(changePasswordFirstTime(values));
    actions.setSubmitting(false);
  };

  return { initialValues, handleSubmit, ChangePasswordFirstimeSchema };
};

export default useFirstTimeLogin;
