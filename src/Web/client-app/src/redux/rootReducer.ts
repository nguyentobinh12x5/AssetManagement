import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';
import authReducer from '../features/auth/reducers/auth-slice';

export default {
  counter: counterReducer,
  todoItems: todoItemsReducer,
  auth: authReducer,
};
