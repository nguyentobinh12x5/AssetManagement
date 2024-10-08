import * as Yup from 'yup';

export const isOver18 = (date: Date) => {
  const now = new Date();
  const age = now.getFullYear() - date.getFullYear();
  const month = now.getMonth() - date.getMonth();
  if (month < 0 || (month === 0 && now.getDate() < date.getDate())) {
    return age > 18;
  }
  return age >= 18;
};

export const isWeekend = (date: Date) => {
  const day = date.getDay();
  return day === 0 || day === 6;
};

export const UserSchema = Yup.object().shape({
  firstName: Yup.string()
    .required('')
    .test(
      'is-alphabetical',
      'The First Name field allows only alphabetical characters. Please remove any numbers, special characters, or spaces',
      (value) => /^[\p{L}][\p{L}]*[\p{L}]$/u.test(value)
    ),
  lastName: Yup.string()
    .required('')
    .test(
      'is-alphabetical',
      'The Last Name field allows only alphabetical characters and spaces. Please remove any numbers or special characters',
      (value) => /^\p{L}+(?:\p{Zs}*\p{L})?(?:\p{Zs}\p{L}+)*$/u.test(value)
    ),
  dateOfBirth: Yup.string()
    .required('')
    .test(
      'is-over-18',
      'User is under 18. Please select a different date',
      function (value) {
        if (!value) return false;
        return isOver18(new Date(value));
      }
    ),
  joinDate: Yup.string()
    .required('')
    .test(
      'is-date-of-birth-selected',
      'Please Select Date of Birth',
      function (value) {
        const { dateOfBirth } = this.parent;
        return !!dateOfBirth;
      }
    )
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
    ),
  type: Yup.string()
    .max(256, 'The Type field must be at most 256 characters')
    .required(''),
  gender: Yup.string()
    .max(256, 'The Gender field must be at most 256 characters')
    .required(''),
});

export interface IUserForm extends Yup.InferType<typeof UserSchema> {}
export {};
