import { takeLatest } from 'redux-saga/effects';
import {
  editUser,
  getUserById,
  getUsers,
  deleteUser,
} from '../reducers/user-slice';
import {
  handleDeleteUser,
  handleEditUser,
  handleGetUserById,
  handleGetUsers,
} from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
  yield takeLatest(getUserById.type, handleGetUserById);
  yield takeLatest(getUsers.type, handleGetUsers);
  yield takeLatest(deleteUser.type, handleDeleteUser);
}
