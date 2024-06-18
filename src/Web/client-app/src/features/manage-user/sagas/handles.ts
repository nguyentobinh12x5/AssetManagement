import { PayloadAction } from "@reduxjs/toolkit";
import { call, put } from 'redux-saga/effects';
import { IUserQuery } from "../interfaces/IUserQuery";
import { setUsers } from "../reducers/user-slice";
import { getUsers } from "./requests";


export function* handleGetUsers(action: PayloadAction<IUserQuery>) {
    const userQuery = action.payload;

    try {
        const { data } = yield call(getUsers, userQuery);
        yield put(setUsers(data));
    }
    catch (error: any) {
        const msg = error.response.data;
    }
}