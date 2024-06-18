import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';
import changePasswordReducer from '../features/auth/changepassword/reducers/change-password-slice';

export default {
  counter: counterReducer,
  todoItems: todoItemsReducer,
  changePassword: changePasswordReducer,
};
