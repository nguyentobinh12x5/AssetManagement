import { FormikHelpers } from 'formik';
import * as Yup from 'yup';

const LoginSchema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string().required('Required'),
});
export interface LoginType extends Yup.InferType<typeof LoginSchema> {}

const useLogin = () => {
  const initialValues: LoginType = { email: '', password: '' };

  const handleSubmit = (
    values: LoginType,
    actions: FormikHelpers<LoginType>
  ) => {
    console.log({ values, actions });
    alert(JSON.stringify(values, null, 2));
    actions.setSubmitting(false);
  };

  return { initialValues, handleSubmit, LoginSchema };
};

export default useLogin;
