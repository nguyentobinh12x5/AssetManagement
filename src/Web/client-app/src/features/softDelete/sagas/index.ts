import { takeLatest } from "redux-saga/effects";
import { deleteUser } from "../reducers/user-slice";
import { handleDeleteUser } from "./handles";

export default function* rootSaga() {
    yield takeLatest(deleteUser.type, handleDeleteUser);
}