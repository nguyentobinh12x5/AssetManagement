import { takeLatest } from "redux-saga/effects";
import { getTodoItems } from "../reducers/todo-item-slice";
import { handleGetTodoItems } from "./handles";

export default function* authorizeSagas() {
    yield takeLatest(getTodoItems.type, handleGetTodoItems)
}

