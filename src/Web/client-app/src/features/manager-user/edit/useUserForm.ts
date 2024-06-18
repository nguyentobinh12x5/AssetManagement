import { FormikHelpers } from 'formik';
import * as Yup from 'yup';

const UserSchema = Yup.object().shape({
  firstName: Yup.string().required('Required'),
  lastName: Yup.string().required('Required'),
  dateOfBirth: Yup.date().required('Required'),
  joinDate: Yup.date().required('Required'),
  type: Yup.string().required('Required'),
  gender: Yup.string().required('Required'),
});
export interface UserType extends Yup.InferType<typeof UserSchema> {}

const useUserForm = () => {
  const initialValues: UserType = {
    firstName: '',
    lastName: '',
    gender: '',
    dateOfBirth: new Date(),
    joinDate: new Date(),
    type: '',
  };

  const handleSubmit = (values: UserType, actions: FormikHelpers<UserType>) => {
    console.log({ values, actions });
    alert(JSON.stringify(values, null, 2));
    actions.setSubmitting(false);
  };

  return { initialValues, handleSubmit, UserSchema };
};

export default useUserForm;
