import { FormikHelpers } from 'formik';
import * as Yup from 'yup';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { createUser, setSucceedStatus } from '../reducers/user-slice';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

// Function to check if a date is at least 18 years ago
const isOver18 = (date: Date) => {
  const now = new Date();
  const age = now.getFullYear() - date.getFullYear();
  const month = now.getMonth() - date.getMonth();
  if (month < 0 || (month === 0 && now.getDate() < date.getDate())) {
    return age > 18;
  }
  return age >= 18;
};

// Function to check if a date is a weekend
const isWeekend = (date: Date) => {
  const day = date.getDay();
  return day === 0 || day === 6;
};

const validateAlphabeticalWithSpaces = (value: string) =>
  /^[\p{L} ]+$/u.test(value);

const UserSchema = Yup.object().shape({
  firstName: Yup.string()
    .max(256, 'The First Name field must be at most 256 characters')
    .required('Required')
    .test(
      'is-alphabetical',
      'The First Name field allows only alphabetical characters. Please remove any numbers, special characters, or spaces',
      (value) => validateAlphabeticalWithSpaces(value)
    ),
  lastName: Yup.string()
    .max(256, 'The Last Name field must be at most 256 characters')
    .required('Required')
    .test(
      'is-alphabetical',
      'The Last Name field allows only alphabetical characters and spaces. Please remove any numbers or special characters',
      (value) => validateAlphabeticalWithSpaces(value)
    ),
  dateOfBirth: Yup.date()
    .required('Required')
    .test(
      'is-over-18',
      'User is under 18. Please select a different date',
      function (value) {
        if (!value) return false;
        return isOver18(new Date(value));
      }
    ),
  joinDate: Yup.date()
    .required('Required')
    .test(
      'is-valid-join-date',
      'User under the age of 18 may not join company. Please select a different date',
      function (value) {
        const { dateOfBirth } = this.parent;
        if (!dateOfBirth) return false;
        return (
          new Date(value) >=
          new Date(
            new Date(dateOfBirth).setFullYear(
              new Date(dateOfBirth).getFullYear() + 18
            )
          )
        );
      }
    )
    .test(
      'is-not-weekend',
      'Joined date is Saturday or Sunday. Please select a different date',
      (value) => {
        if (!value) return false;
        return !isWeekend(new Date(value));
      }
    )
    .test('dob-not-null', 'Please Select Date of Birth', function (value) {
      const { dateOfBirth } = this.parent;
      return !!dateOfBirth;
    }),
  type: Yup.string()
    .max(256, 'The Type field must be at most 256 characters')
    .required('Required'),
  gender: Yup.string()
    .max(256, 'The Gender field must be at most 256 characters')
    .required('Required'),
});

export interface IUserForm extends Yup.InferType<typeof UserSchema> {}

const minDobString = new Date();
const minJoinedDateString = new Date();

minDobString.setFullYear(minDobString.getFullYear() - 20);

minJoinedDateString.setFullYear(minDobString.getFullYear() + 18);

const useCreateForm = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { isLoading, succeed, error } = useAppState((state) => state.users);
  const user: IUserForm = {
    firstName: '',
    lastName: '',
    dateOfBirth: minDobString,
    joinDate: minJoinedDateString,
    type: 'Staff',
    gender: 'Male',
  };

  const handleSubmit = (
    values: IUserForm,
    actions: FormikHelpers<IUserForm>
  ) => {
    const payload = {
      ...values,
      dateOfBirth: new Date(values.dateOfBirth).toISOString(),
      joinDate: new Date(values.joinDate).toISOString(),
    };
    dispatch(createUser(payload));
    actions.setSubmitting(false);
  };

  useEffect(() => {
    if (succeed && !isLoading && !error) {
      dispatch(setSucceedStatus(false));
      navigate('/user');
    }
  }, [succeed, navigate, error, isLoading]);

  return { user, isLoading, handleSubmit, UserSchema };
};

export default useCreateForm;
