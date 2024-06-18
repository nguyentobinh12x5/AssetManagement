import { takeLatest } from 'redux-saga/effects';
import { getUserInfo, login } from '../reducers/auth-slice';
import { handleGetUserInfo, handleLogin } from './handles';

export default function* authSagas() {
  yield takeLatest(login.type, handleLogin);
  yield takeLatest(getUserInfo.type, handleGetUserInfo);
}
