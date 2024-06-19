import { takeLatest } from 'redux-saga/effects';
import { editUser, getUserById } from '../reducers/user-slice';
import { handleEditUser, handleGetUserById } from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
  yield takeLatest(getUserById.type, handleGetUserById);
}
