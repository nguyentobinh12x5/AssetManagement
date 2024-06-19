import { takeLatest } from "redux-saga/effects";
import { getUsers, deleteUser } from "../reducers/user-slice";
import { handleGetUsers, handleDeleteUser } from "./handles";

export default function* rootSaga() {
    yield takeLatest(getUsers.type, handleGetUsers);
    yield takeLatest(deleteUser.type, handleDeleteUser);
}

