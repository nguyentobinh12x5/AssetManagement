import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import {
  IMyAssignmentBrief,
  IMyAssignmentQuery,
} from '../interfaces/IMyAssignment';
import { DEFAULT_MY_ASSIGNMENT_SORT_COL } from '../constants/my-assignment-query';

const defaultAssetQuery: IMyAssignmentQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnName: DEFAULT_MY_ASSIGNMENT_SORT_COL,
  sortColumnDirection: ASCENDING,
};

interface MyAssignmentState {
  assignments: IPagedModel<IMyAssignmentBrief>;
  isLoading: boolean;
  query: IMyAssignmentQuery;
  error?: string | null;
}

const initialState: MyAssignmentState = {
  assignments: {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false,
  },
  query: defaultAssetQuery,
  isLoading: false,
};
const AssignmentSlice = createSlice({
  initialState,
  name: 'my-assignment',
  reducers: {
    getMyAssignments: (
      state: MyAssignmentState,
      action: PayloadAction<IMyAssignmentQuery>
    ) => ({
      ...state,
      isLoading: true,
    }),
    setMyAssignmentQuery: (
      state: MyAssignmentState,
      action: PayloadAction<IMyAssignmentQuery>
    ) => ({
      ...state,
      query: action.payload,
    }),

    // Success handles
    getMyAssignmentsSuccess: (
      state: MyAssignmentState,
      action: PayloadAction<IPagedModel<IMyAssignmentBrief>>
    ) => ({
      ...state,
      isLoading: false,
      assignments: action.payload,
    }),

    // Failure handles
    getMyAssignmentsFailure: (
      state: MyAssignmentState,
      action: PayloadAction<string>
    ) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
  },
});

export const {
  setMyAssignmentQuery,
  getMyAssignments,
  getMyAssignmentsSuccess,
  getMyAssignmentsFailure,
} = AssignmentSlice.actions;

export default AssignmentSlice.reducer;
