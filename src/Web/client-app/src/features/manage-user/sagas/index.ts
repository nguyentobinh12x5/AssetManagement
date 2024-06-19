import { takeLatest } from 'redux-saga/effects';
import { getUsers } from '../reducers/user-slice';
import { handleGetUsers } from './handles';

export default function* authorizeSagas() {
  yield takeLatest(getUsers.type, handleGetUsers);
}
