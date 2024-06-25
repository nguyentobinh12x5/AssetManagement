import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { login } from '../reducers/auth-slice';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const LoginSchema = Yup.object().shape({
  username: Yup.string().required(''),
  password: Yup.string().required(''),
});
export interface ILoginForm extends Yup.InferType<typeof LoginSchema> {}

const useLogin = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { isAuthenticated } = useAppState((state) => state.auth);
  const initialValues: ILoginForm = { username: '', password: '' };

  const handleSubmit = (
    values: ILoginForm,
    actions: FormikHelpers<ILoginForm>
  ) => {
    dispatch(login({ ...values, email: values.username }));
    actions.setSubmitting(false);
  };

  useEffect(() => {
    if (isAuthenticated) navigate('/');
  }, [isAuthenticated, navigate]);

  return { initialValues, handleSubmit, LoginSchema };
};

export default useLogin;
