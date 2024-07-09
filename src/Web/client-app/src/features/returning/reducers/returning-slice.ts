import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IReturningQuery } from '../interfaces/Common/IReturningQuery';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { DEFAULT_MANAGE_RETURNING_SORT_COLUMN } from '../constants/returning-sort';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IBriefReturning } from '../interfaces/IBriefReturning';

const defaultReturningQuery: IReturningQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnDirection: ASCENDING,
  sortColumnName: DEFAULT_MANAGE_RETURNING_SORT_COLUMN,
  state: ['All'],
  returnedDate: '',
  searchTerm: '',
};
interface ReturningState {
  returnings: IPagedModel<IBriefReturning>;
  isLoading: boolean;
  error?: string | null;
  returningQuery: IReturningQuery;
  isDataFetched: boolean;
  states: string[];
}

const initialState: ReturningState = {
  returnings: {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false,
  },
  states: ['All'],
  isLoading: false,
  returningQuery: defaultReturningQuery,
  isDataFetched: false,
  error: null,
};
const ReturningSlice = createSlice({
  initialState,
  name: 'returning',
  reducers: {
    setReturningQuery: (
      state: ReturningState,
      action: PayloadAction<IReturningQuery>
    ) => {
      return {
        ...state,
        returningQuery: action.payload,
        isDataFetched: false,
      };
    },
    getReturnings: (
      state: ReturningState,
      action: PayloadAction<IReturningQuery>
    ) => ({
      ...state,
      isLoading: true,
    }),

    // Success handles
    getReturningsSuccess: (
      state: ReturningState,
      action: PayloadAction<any>
    ) => ({
      ...state,
      isLoading: false,
      isDataFetched: true,
      returnings: action.payload,
    }),

    // Failure handles
    getReturningsFailure: (
      state: ReturningState,
      action: PayloadAction<string>
    ) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    resetReturningSlice: () => initialState,
  },
});

export const {
  getReturnings,
  setReturningQuery,
  getReturningsSuccess,
  getReturningsFailure,
} = ReturningSlice.actions;

export default ReturningSlice.reducer;
