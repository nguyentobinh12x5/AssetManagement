import counterReducer from '../features/counter/counter-slice';
import todoItemsReducer from '../features/todo-item/reducers/todo-item-slice';

export default {
    counter: counterReducer,
    todoItems: todoItemsReducer
}