import { PayloadAction, createSlice } from '@reduxjs/toolkit';

interface ChangePasswordState {
  isLoading: boolean;
  status?: number;
  error?: string;
}

const initialState: ChangePasswordState = {
  isLoading: false,
};

const ChangePasswordSlice = createSlice({
  name: 'change-password',
  initialState,
  reducers: {
    changePasswordRequest: (
      state: ChangePasswordState,
      action: PayloadAction<{
        values: { currentPassword: string; newPassword: string };
        actions: any;
        setApiError: any;
      }>
    ) => ({
      ...state,
      isLoading: true,
    }),
    changePasswordSuccess: (state: ChangePasswordState) => ({
      ...state,
      isLoading: false,
      status: 204,
    }),
    changePasswordFailure: (
      state: ChangePasswordState,
      action: PayloadAction<string>
    ) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    resetChangePasswordState: (state: ChangePasswordState) => initialState,
  },
});

export const {
  changePasswordRequest,
  changePasswordSuccess,
  changePasswordFailure,
  resetChangePasswordState,
} = ChangePasswordSlice.actions;

export default ChangePasswordSlice.reducer;
