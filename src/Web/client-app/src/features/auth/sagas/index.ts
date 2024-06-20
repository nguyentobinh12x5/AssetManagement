import { takeLatest } from 'redux-saga/effects';
import {
  changePasswordFirstTime,
  getUserInfo,
  login,
  logout,
} from '../reducers/auth-slice';
import {
  handleChangePasswordFirstTime,
  handleGetUserInfo,
  handleLogin,
  handleLogout,
} from './handles';

export default function* authSagas() {
  yield takeLatest(login.type, handleLogin);
  yield takeLatest(getUserInfo.type, handleGetUserInfo);
  yield takeLatest(changePasswordFirstTime.type, handleChangePasswordFirstTime);
  yield takeLatest(logout.type, handleLogout);
}
