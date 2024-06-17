import { PayloadAction } from "@reduxjs/toolkit";
import { call, put } from 'redux-saga/effects';
import { IUserQuery } from "../interfaces/IUserQuery";
import { setTodoItems } from "../reducers/todo-item-slice";
import { getTodoItems } from "./requests";


export function* handleGetTodoItems(action: PayloadAction<IUserQuery>) {
    const userQuery = action.payload;

    try {
        const { data } = yield call(getTodoItems, userQuery);
        yield put(setTodoItems(data));
    }
    catch (error: any) {
        const msg = error.response.data;
    }
}