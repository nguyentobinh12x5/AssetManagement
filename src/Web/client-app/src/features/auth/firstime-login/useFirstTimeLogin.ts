import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch } from '../../../redux/redux-hooks';
import { changePasswordFirstTime } from '../reducers/auth-slice';
import { PASSWORD_VALIDATE_MSG } from '../constants/auth-first-time';

const ChangePasswordFirstimeSchema = Yup.object().shape({
  newPassword: Yup.string()
    .required('Required')
    .min(6, PASSWORD_VALIDATE_MSG)
    .matches(/[A-Z]/, PASSWORD_VALIDATE_MSG)
    .matches(/[a-z]/, PASSWORD_VALIDATE_MSG)
    .matches(/[0-9]/, PASSWORD_VALIDATE_MSG)
    .matches(/[^a-zA-Z0-9]/, PASSWORD_VALIDATE_MSG),
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
