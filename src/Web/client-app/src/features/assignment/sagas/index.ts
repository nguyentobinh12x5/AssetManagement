import { takeLatest } from 'redux-saga/effects';
import {
  createAssignment,
  deleteAssginments,
  getAssignments,
  editAssignment,
} from '../reducers/assignment-slice';
import {
  handleCreateAssignment,
  handleDeleteAssignment,
  handleGetAssignmentById,
  handleGetAssignments,
  handleEditAssignment,
  handleGetAssignmentByIdWhenEdit,
} from './handles';
import {
  getAssignmentById,
  getAssignmentByIdWhenEdit,
} from '../reducers/assignment-detail-slice';

export default function* assignmentSagas() {
  yield takeLatest(getAssignments.type, handleGetAssignments);
  yield takeLatest(getAssignmentById.type, handleGetAssignmentById);
  yield takeLatest(createAssignment.type, handleCreateAssignment);
  yield takeLatest(editAssignment.type, handleEditAssignment);
  yield takeLatest(
    getAssignmentByIdWhenEdit.type,
    handleGetAssignmentByIdWhenEdit
  );
  yield takeLatest(deleteAssginments.type, handleDeleteAssignment);
}
