import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import {
  editUser,
  getUserById,
  setSucceedStatus,
} from '../reducers/user-slice';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect } from 'react';

const UserSchema = Yup.object().shape({
  firstName: Yup.string().required('Required'),
  lastName: Yup.string().required('Required'),
  dateOfBirth: Yup.string().required('Required'),
  joinDate: Yup.string().required('Required'),
  type: Yup.string().required('Required'),
  gender: Yup.string().required('Required'),
});
export interface IUserForm extends Yup.InferType<typeof UserSchema> {}

const useEditForm = () => {
  const { userId } = useParams<{ userId: string }>();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { user, isLoading, succeed, error } = useAppState(
    (state) => state.users
  );

  useEffect(() => {
    if (userId) {
      dispatch(getUserById(userId));
    }
  }, [userId]);

  const handleSubmit = (
    values: IUserForm,
    actions: FormikHelpers<IUserForm>
  ) => {
    dispatch(editUser(values));
    actions.setSubmitting(false);
  };

  useEffect(() => {
    if (succeed && !isLoading && !error) {
      dispatch(setSucceedStatus(false));
      navigate('/user');
    }
  }, [succeed, navigate]);

  return { user, isLoading, handleSubmit, UserSchema };
};

export default useEditForm;
