import { PayloadAction } from '@reduxjs/toolkit';
import { getReturingsRequest } from './requests';
import { call, put } from 'redux-saga/effects';
import {
  getReturningsFailure,
  getReturningsSuccess,
} from '../reducers/returning-slice';
import { ReturningState, ReturningStateKey } from '../constants/returning-state';
import { IReturningQuery } from '../interfaces/Common/IReturningQuery';

export function* handleGetReturnings(action: PayloadAction<IReturningQuery>) {
    try {
        let query = action.payload;
        if (query.state.includes('All')) {
            query = { ...query, state: [] };
        } else {
            query = {
                ...query,
                state: query.state.map((_) =>
                    ReturningState[_ as ReturningStateKey].toString()
                ),
            };
        }
        const { data } = yield call(getReturingsRequest, query);
        yield put(getReturningsSuccess(data));
    } catch (error: any) {
        console.error(error);
        yield put(getReturningsFailure(error.data.detail));
    }
}
