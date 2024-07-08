import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface ReturningState {
  returnings: any[];
  isLoading: boolean;
  error?: string | null;
}

const initialState: ReturningState = {
  returnings: [],
  isLoading: false,
};
const ReturningSlice = createSlice({
  initialState,
  name: 'returning',
  reducers: {
    getReturnings: (state: ReturningState) => ({
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
  },
});

export const { getReturnings, getReturningsSuccess, getReturningsFailure } =
  ReturningSlice.actions;

export default ReturningSlice.reducer;
