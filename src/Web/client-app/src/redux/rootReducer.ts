import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';
import usersReducer from '../features/manager-user/reducers/user-slice';
import authReducer from '../features/auth/reducers/auth-slice';
import changePasswordReducer from '../features/auth/changepassword/reducers/change-password-slice';

export default {
  counter: counterReducer,
  todoItems: todoItemsReducer,
  users: usersReducer,
  auth: authReducer,
  changePassword: changePasswordReducer,
};
