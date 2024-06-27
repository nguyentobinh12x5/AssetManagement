import { FormikHelpers } from 'formik';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { createUser } from '../reducers/user-slice';
import { IUserForm } from '../components/validateUserSchemas';
import { utcToDateString } from '../../../utils/dateUtils';

const useCreateUserForm = () => {
  const dispatch = useAppDispatch();
  const { isLoading } = useAppState((state) => state.users);
  const user: IUserForm = {
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    joinDate: '',
    type: '',
    gender: 'Female',
  };

  const handleSubmit = (
    values: IUserForm,
    actions: FormikHelpers<IUserForm>
  ) => {
    const payload = {
      ...values,
      dateOfBirth: utcToDateString(values.dateOfBirth),
      joinDate: utcToDateString(values.joinDate),
    };
    dispatch(createUser(payload));
    actions.setSubmitting(false);
  };

  return { user, isLoading, handleSubmit };
};

export default useCreateUserForm;
