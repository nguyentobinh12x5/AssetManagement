import { takeLatest } from 'redux-saga/effects';
import { editUser, getUserById ,createUser} from '../reducers/user-slice';
import { handleCreateUser, handleEditUser, handleGetUserById } from './handles';

export default function* userSagas() {
  yield takeLatest(editUser.type, handleEditUser);
    yield takeLatest(getUserById.type, handleGetUserById);
    yield takeLatest(createUser.type, handleCreateUser);
}
