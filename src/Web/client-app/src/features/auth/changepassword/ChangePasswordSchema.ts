import * as Yup from 'yup';

export const ChangePasswordSchema = Yup.object().shape({
  currentPassword: Yup.string().required('Old password is required'),
  newPassword: Yup.string()
    .required('New password is required')
    .min(6, 'Password must be at least 6 characters long')
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])/,
      'Password must be at least 6 characters long and include an uppercase letter, a lowercase letter, a digit, and a special character.'
    ),
});

export type ChangePasswordType = Yup.InferType<typeof ChangePasswordSchema>;
