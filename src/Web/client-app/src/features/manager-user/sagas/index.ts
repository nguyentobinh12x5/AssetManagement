import { takeLatest } from 'redux-saga/effects';
import {
  editUser,
  getUserById,
  getUsers,
  deleteUser,
  createUser,
  getUserTypes,
} from '../reducers/user-slice';
import {
  handleCreateUser,
  handleDeleteUser,
  handleEditUser,
  handleGetUserById,
  handleGetUserTypes,
  handleGetUsers,
} from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
  yield takeLatest(getUserById.type, handleGetUserById);
  yield takeLatest(getUsers.type, handleGetUsers);
  yield takeLatest(deleteUser.type, handleDeleteUser);
  yield takeLatest(createUser.type, handleCreateUser);
  yield takeLatest(getUserTypes.type, handleGetUserTypes);
}
