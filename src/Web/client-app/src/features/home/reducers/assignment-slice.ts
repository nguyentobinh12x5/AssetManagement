import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AssignmentState {
  assignments: any[];
  isLoading: boolean;
  error?: string | null;
}

const initialState: AssignmentState = {
  assignments: [],
  isLoading: false,
};
const AssignmentSlice = createSlice({
  initialState,
  name: 'my-assignment',
  reducers: {
    getAssignments: (state: AssignmentState) => ({
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

    // Failure handles
    getAssignmentsFailure: (
      state: AssignmentState,
      action: PayloadAction<string>
    ) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
  },
});

export const { getAssignments, getAssignmentsSuccess, getAssignmentsFailure } =
  AssignmentSlice.actions;

export default AssignmentSlice.reducer;
