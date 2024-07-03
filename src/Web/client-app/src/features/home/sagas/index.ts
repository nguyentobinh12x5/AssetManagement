import { takeLatest } from 'redux-saga/effects';
import { handleGetMyAssignments, handleUpdateStateAssignment } from './handles';
import {
  getMyAssignments,
  updateStateAssignment,
} from '../reducers/my-assignment-slice';

export default function* authorizeSagas() {
  yield takeLatest(getMyAssignments.type, handleGetMyAssignments);
  yield takeLatest(updateStateAssignment.type, handleUpdateStateAssignment);
}
