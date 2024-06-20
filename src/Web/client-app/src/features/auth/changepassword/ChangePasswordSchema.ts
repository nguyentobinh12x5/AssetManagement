import * as Yup from 'yup';

export const ChangePasswordSchema = Yup.object().shape({
  currentPassword: Yup.string().required('Old password is required'),
  newPassword: Yup.string()
    .required('New password is required')
    .test(
      'password-complexity',
      'Password must be at least 6 characters long and include an uppercase letter, a lowercase letter, a digit, and a special character.',
      value => 
        value.length >= 6 && 
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])/.test(value)
    ),
});

export type ChangePasswordType = Yup.InferType<typeof ChangePasswordSchema>;
