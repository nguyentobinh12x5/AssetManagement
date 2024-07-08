import { takeLatest } from "redux-saga/effects";
import { getReturnings } from "../reducers/returning-slice";
import { handleGetReturnings } from "./handles";

export default function* authorizeSagas() {
  yield takeLatest(getReturnings.type, handleGetReturnings);
}
