import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { login } from '../reducers/auth-slice';
import { useEffect } from 'react';
import { useToast } from '../../../components/toastify/ToastContext';
import { getCookie } from '../../../utils/cookiesUtils';
import { useNavigate } from 'react-router-dom';

const LoginSchema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string().required('Required'),
});
export interface ILoginForm extends Yup.InferType<typeof LoginSchema> {}

const useLogin = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { isAuthenticated } = useAppState((state) => state.auth);
  const initialValues: ILoginForm = { email: '', password: '' };

  const handleSubmit = (
    values: ILoginForm,
    actions: FormikHelpers<ILoginForm>
  ) => {
    dispatch(login(values));
    actions.setSubmitting(false);
  };

  useEffect(() => {
    if (isAuthenticated) navigate('/');
  }, [isAuthenticated, navigate]);

  return { initialValues, handleSubmit, LoginSchema };
};

export default useLogin;
