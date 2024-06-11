import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import { ITodoQuery } from "../interfaces/ITodoQuery";
import { ITodoItem } from "../interfaces/ITodoItem";


interface TodoItemState {
    isLoading: boolean,
    todoItems?: IPagedModel<ITodoItem>
    status?: number;
}

const initialState: TodoItemState = {
    isLoading: false
}

const TodoItemSlice = createSlice({
    name: 'todo-items',
    initialState,
    reducers: {
        getTodoItems: (
            state: TodoItemState,
            action: PayloadAction<ITodoQuery>
        ): TodoItemState => ({
            ...state,
            isLoading: true
        }),
        setTodoItems: (
            state: TodoItemState,
            action: PayloadAction<IPagedModel<ITodoItem>>
        ) => {
            const todoItems = action.payload;
            return {
                ...state,
                todoItems
            }
        }
    }
})

export const {
    getTodoItems,
    setTodoItems
} = TodoItemSlice.actions;

export default TodoItemSlice.reducer;