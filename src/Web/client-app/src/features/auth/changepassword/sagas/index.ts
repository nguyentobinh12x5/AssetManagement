import { takeLatest } from 'redux-saga/effects';
import { changePasswordRequest } from '../reducers/change-password-slice';
import { handleChangePassword } from './handles';

export default function* changePasswordSaga() {
  yield takeLatest(changePasswordRequest.type, handleChangePassword);
}
