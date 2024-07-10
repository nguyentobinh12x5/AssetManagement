import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAssignmentDetail } from '../interfaces/IAssignmentDetail';

interface AssignmentState {
  AssignmentDetail: IAssignmentDetail | null;
  isLoading: boolean;
  error?: string | null;
}

const initialState: AssignmentState = {
  isLoading: false,
  error: null,
  AssignmentDetail: null,
};

const assignmentDetailSlice = createSlice({
  name: 'assignmentDetail',
  initialState,
  reducers: {
    getAssignmentById: (
      state: AssignmentState,
      action: PayloadAction<number>
    ) => {
      state.isLoading = true;
      state.error = null;
    },
    getAssignmentByIdWhenEdit: (
      state: AssignmentState,
      action: PayloadAction<number>
    ) => {
      state.isLoading = true;
      state.error = null;
    },
    getAssignmentByIdSuccess: (
      state: AssignmentState,
      action: PayloadAction<IAssignmentDetail>
    ) => {
      state.isLoading = false;
      state.AssignmentDetail = action.payload;
    },
    getAssignmentByIdFailure: (
      state: AssignmentState,
      action: PayloadAction<string>
    ) => {
      state.isLoading = false;
      state.error = action.payload;
    },
    resetState: () => initialState,
  },
});

export const {
  getAssignmentById,
  getAssignmentByIdWhenEdit,
  getAssignmentByIdSuccess,
  getAssignmentByIdFailure,
  resetState,
} = assignmentDetailSlice.actions;

export default assignmentDetailSlice.reducer;
