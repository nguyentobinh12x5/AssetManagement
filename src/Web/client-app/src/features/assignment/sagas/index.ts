import { takeLatest } from 'redux-saga/effects';
import { createAssignment, getAssignments } from '../reducers/assignment-slice';
import {
  handleCreateAssignment,
  handleGetAssignmentById,
  handleGetAssignments,
} from './handles';
import { getAssignmentById } from '../reducers/assignment-detail-slice';

export default function* authorizeSagas() {
  yield takeLatest(getAssignments.type, handleGetAssignments);
  yield takeLatest(getAssignmentById.type, handleGetAssignmentById);
  yield takeLatest(createAssignment.type, handleCreateAssignment);
}
