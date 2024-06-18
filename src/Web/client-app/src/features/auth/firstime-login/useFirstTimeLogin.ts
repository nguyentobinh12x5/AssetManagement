import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch } from '../../../redux/redux-hooks';

const ChangePasswordFirstimeSchema = Yup.object().shape({
  newPassword: Yup.string().required('Required'),
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
    // dispatch(login(values));
    actions.setSubmitting(false);
  };

  return { initialValues, handleSubmit };
};

export default useFirstTimeLogin;
