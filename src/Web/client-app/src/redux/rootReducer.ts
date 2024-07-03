import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';
import usersReducer from '../features/manager-user/reducers/user-slice';
import authReducer from '../features/auth/reducers/auth-slice';
import changePasswordReducer from '../features/auth/changepassword/reducers/change-password-slice';
import assetsReducer from '../features/asset/reducers/asset-slice';
import assetDetailReducer from '../features/asset/reducers/asset-detail-slice';
import assignmentReducer from '../features/assignment/reducers/assignment-slice';
import myAssignmentReducer from '../features/home/reducers/my-assignment-slice';
import assignmentDetailReducer from '../features/assignment/reducers/assignment-detail-slice';

const rootReducer = {
  counter: counterReducer,
  todoItems: todoItemsReducer,
  auth: authReducer,
  changePassword: changePasswordReducer,
  users: usersReducer,
  assets: assetsReducer,
  assetDetail: assetDetailReducer,
  assignments: assignmentReducer,
  myAssignments: myAssignmentReducer,
  assignmentDetail: assignmentDetailReducer,
};
export default rootReducer;
