import * as Yup from 'yup';
import { PASSWORD_VALIDATE_MSG } from '../constants/auth-first-time';

const ChangePasswordFirstimeSchema = Yup.object().shape({
  newPassword: Yup.string()
    .required('')
    .test(
      'password-complexity',
      PASSWORD_VALIDATE_MSG,
      (value) =>
        value.length >= 6 &&
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])/.test(value)
    ),
});

const ChangePasswordFirstimeRequiredSchema = Yup.object().shape({
  newPassword: Yup.string().required(''),
});

export { ChangePasswordFirstimeRequiredSchema, ChangePasswordFirstimeSchema };
