import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch } from '../../../redux/redux-hooks';
import { changePasswordFirstTime } from '../reducers/auth-slice';
import { ChangePasswordFirstimeSchema } from './change-password-first-time-schema';

export interface IChangePasswordFirstTime
  extends Yup.InferType<typeof ChangePasswordFirstimeSchema> {}

const useFirstTimeLogin = () => {
  const dispatch = useAppDispatch();
  const initialValues: IChangePasswordFirstTime = { newPassword: '' };

  const handleSubmit = (
    values: IChangePasswordFirstTime,
    actions: FormikHelpers<IChangePasswordFirstTime>
  ) => {
    const { setSubmitting, setTouched, setErrors } = actions;

    ChangePasswordFirstimeSchema.validate(values, { abortEarly: false })
      .then(() => {
        // Proceed with form submission
        dispatch(changePasswordFirstTime(values));

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
          newPassword: true,
        });
        setSubmitting(false);
      });
  };

  return { initialValues, handleSubmit };
};

export default useFirstTimeLogin;
