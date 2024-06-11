import { PayloadAction } from "@reduxjs/toolkit";
import { call, put } from 'redux-saga/effects';
import { ITodoQuery } from "../interfaces/ITodoQuery";
import { setTodoItems } from "../reducers/todo-item-slice";
import { getTodoItems } from "./requests";


export function* handleGetTodoItems(action: PayloadAction<ITodoQuery>) {
    const todoQuery = action.payload;

    try {
        const { data } = yield call(getTodoItems, todoQuery);
        yield put(setTodoItems(data));
    }
    catch (error: any) {
        const msg = error.response.data;
    }
}