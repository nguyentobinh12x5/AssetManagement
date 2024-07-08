import { takeLatest } from 'redux-saga/effects';
import {
  handleGetMyAssignments,
  handleUpdateStateAssignment,
  handleReturningAssignment,
} from './handles';
import {
  getMyAssignments,
  updateStateAssignment,
  returningAssignment,
} from '../reducers/my-assignment-slice';

export default function* authorizeSagas() {
  yield takeLatest(getMyAssignments.type, handleGetMyAssignments);
  yield takeLatest(updateStateAssignment.type, handleUpdateStateAssignment);
  yield takeLatest(returningAssignment.type, handleReturningAssignment);
}
