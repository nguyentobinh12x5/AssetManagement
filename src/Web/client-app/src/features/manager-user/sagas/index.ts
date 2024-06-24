import { takeLatest } from 'redux-saga/effects';
import {
  editUser,
  getUserById,
  getUsers,
  deleteUser,
  getUsersByType,
  getUsersBySearchTerm,
  createUser,
} from '../reducers/user-slice';
import {
  handleCreateUser,
  handleDeleteUser,
  handleEditUser,
  handleGetUserById,
  handleGetUsers,
  handleGetUsersBySearchTerm,
  handleGetUsersByType,
} from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
  yield takeLatest(getUserById.type, handleGetUserById);
  yield takeLatest(getUsers.type, handleGetUsers);
  yield takeLatest(deleteUser.type, handleDeleteUser);
  yield takeLatest(getUsersByType.type, handleGetUsersByType);
  yield takeLatest(getUsersBySearchTerm.type, handleGetUsersBySearchTerm);
  yield takeLatest(createUser.type, handleCreateUser);
}
