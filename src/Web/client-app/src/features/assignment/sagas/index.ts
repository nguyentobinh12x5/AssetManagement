import { takeLatest } from 'redux-saga/effects';
import {
  createAssignment,
  deleteAssginments,
  getAssignments,
} from '../reducers/assignment-slice';
import {
  handleCreateAssignment,
  handleDeleteAssignment,
  handleGetAssignmentById,
  handleGetAssignments,
} from './handles';
import { getAssignmentById } from '../reducers/assignment-detail-slice';

export default function* assignmentSagas() {
  yield takeLatest(getAssignments.type, handleGetAssignments);
  yield takeLatest(getAssignmentById.type, handleGetAssignmentById);
  yield takeLatest(createAssignment.type, handleCreateAssignment);
  yield takeLatest(deleteAssginments.type, handleDeleteAssignment);
}
