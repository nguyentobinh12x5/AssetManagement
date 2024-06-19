import { takeLatest } from 'redux-saga/effects';
import { editUser, getUserById, getUsers } from '../reducers/user-slice';
import { handleEditUser, handleGetUserById, handleGetUsers } from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
  yield takeLatest(getUserById.type, handleGetUserById);
  yield takeLatest(getUsers.type, handleGetUsers);
}
