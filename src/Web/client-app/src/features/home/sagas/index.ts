import { takeLatest } from 'redux-saga/effects';
import { handleGetMyAssignments } from './handles';
import { getMyAssignments } from '../reducers/my-assignment-slice';

export default function* authorizeSagas() {
  yield takeLatest(getMyAssignments.type, handleGetMyAssignments);
}
