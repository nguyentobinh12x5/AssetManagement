import { PayloadAction } from "@reduxjs/toolkit";
import { call, put } from 'redux-saga/effects';
import { deleteUserRequest } from "./requests";
import { setDeleteStatus } from "../reducers/user-slice";

export function* handleDeleteUser(action: PayloadAction<string>) {
    try {
        yield call(deleteUserRequest, action.payload);
        yield put(setDeleteStatus(false));
    } catch (error: any) {
        yield put(setDeleteStatus(false));
    }
}