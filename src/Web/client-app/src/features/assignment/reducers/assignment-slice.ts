import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAssignmentQuery } from '../interfaces/commom/IAssigmentQuery';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IBriefAssignment } from '../interfaces/IBriefAssignment';
import { DEFAULT_MANAGE_ASSIGNMENT_SORT_COLUMN } from '../../assignment/constants/assignment-sort';
import { ICreateAssignmentCommand } from '../interfaces/ICreateAssignmentCommand';
import { IAssignmentDetail } from '../interfaces/IAssignmentDetail';

const defaultAssignmentQuery: IAssignmentQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnDirection: ASCENDING,
  sortColumnName: DEFAULT_MANAGE_ASSIGNMENT_SORT_COLUMN,
  state: ['All'],
  assignedDate: '',
  searchTerm: '',
};

interface AssignmentState {
  assignments: IPagedModel<IBriefAssignment>;
  isLoading: boolean;
  error?: string | null;
  assignmentQuery: IAssignmentQuery;
  isDataFetched: boolean;
  states: string[];
}

const initialState: AssignmentState = {
  assignments: {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false,
  },
  states: ['All'],
  isLoading: false,
  assignmentQuery: defaultAssignmentQuery,
  isDataFetched: false,
  error: null,
};

const AssignmentSlice = createSlice({
  initialState,
  name: 'assignment',
  reducers: {
    getAssignments: (
      state: AssignmentState,
      action: PayloadAction<IAssignmentQuery>
    ) => ({
      ...state,
      isLoading: true,
    }),
    createAssignment: (
      state: AssignmentState,
      action: PayloadAction<ICreateAssignmentCommand>
    ) => ({
      ...state,
      isLoading: true,
    }),

    // Success handles
    getAssignmentsSuccess: (
      state: AssignmentState,
      action: PayloadAction<any>
    ) => ({
      ...state,
      isLoading: false,
      assignments: action.payload,
    }),
    createAssignmentSuccess: (
      state: AssignmentState,
      action: PayloadAction<IAssignmentDetail>
    ) => {
      const newAssignment: IBriefAssignment = {
        ...action.payload,
        id: action.payload.id.toString(),
      };
      return {
        ...state,
        isLoading: false,
        assignments: {
          ...state.assignments,
          items: [newAssignment, ...state.assignments.items],
        },
      };
    },

    // Failure handles
    getAssignmentsFailure: (
      state: AssignmentState,
      action: PayloadAction<string>
    ) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    getAssignmentStatuses: (state: AssignmentState) => {
      state.isLoading = true;
    },
    setAssignmentStatuses: (
      state: AssignmentState,
      action: PayloadAction<string[]>
    ) => {
      state.states = ['All', ...action.payload];
      state.isLoading = false;
    },
    setAssignmentQuery: (
      state: AssignmentState,
      action: PayloadAction<IAssignmentQuery>
    ) => {
      return {
        ...state,
        assignmentQuery: action.payload,
        isDataFetched: false,
      };
    },
    createAssignmentFailure: (
      state: AssignmentState,
      action: PayloadAction<string>
    ) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
  },
});

export const {
  getAssignments,
  getAssignmentsSuccess,
  getAssignmentsFailure,
  setAssignmentQuery,
  createAssignment,
  createAssignmentSuccess,
  createAssignmentFailure,
} = AssignmentSlice.actions;

export default AssignmentSlice.reducer;
