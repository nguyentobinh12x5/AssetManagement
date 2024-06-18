import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';
import usersReducer from '../features/manage-user/reducers/user-slice';

export default {
    counter: counterReducer,
    todoItems: todoItemsReducer,
    users: usersReducer
}