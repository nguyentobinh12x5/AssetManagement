import todoItemSagas from '../features/todo-item/sagas';
import userSagas from '../features/manager-user/sagas';
import authSagas from '../features/auth/sagas';
import changePasswordSagas from '../features/auth/changepassword/sagas';
import assetSagas from '../features/asset/sagas';
import assignmentSagas from '../features/assignment/sagas';
import myAssignmentSagas from '../features/home/sagas';
import returningSagas from '../features/returning/sagas';
import { all } from 'redux-saga/effects';

export default function* rootSagas() {
  yield all([
    todoItemSagas(),
    authSagas(),
    changePasswordSagas(),
    userSagas(),
    assetSagas(),
    assignmentSagas(),
    myAssignmentSagas(),
    returningSagas(),
  ]);
}
