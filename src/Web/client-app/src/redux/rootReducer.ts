import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';
import authReducer from '../features/auth/reducers/auth-slice';
import changePasswordReducer from '../features/auth/changepassword/reducers/change-password-slice';
import usersReducer from '../features/manage-user/reducers/user-slice';

export default {
  counter: counterReducer,
  todoItems: todoItemsReducer,
  auth: authReducer,
  changePassword: changePasswordReducer,
  users: usersReducer,
};
