import { takeLatest } from 'redux-saga/effects';
import {
  editUser,
  getUserById,
  getUsers,
  deleteUser,
  getUsersByType,
} from '../reducers/user-slice';
import {
  handleDeleteUser,
  handleEditUser,
  handleGetUserById,
  handleGetUsers,
  handleGetUsersByType,
} from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
  yield takeLatest(getUserById.type, handleGetUserById);
  yield takeLatest(getUsers.type, handleGetUsers);
  yield takeLatest(deleteUser.type, handleDeleteUser);
  yield takeLatest(getUsersByType.type, handleGetUsersByType);
}
