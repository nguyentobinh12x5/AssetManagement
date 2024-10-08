import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAssetDetail } from '../interfaces/IAssetDetail';

interface AssetState {
  assetDetail: IAssetDetail | null;
  isLoading: boolean;
  error?: string | null;
}

const initialState: AssetState = {
  isLoading: false,
  error: null,
  assetDetail: null,
};

const assetDetailSlice = createSlice({
  name: 'assetDetail',
  initialState,
  reducers: {
    getAssetById: (state: AssetState, action: PayloadAction<string>) => {
      state.isLoading = true;
      state.error = null;
    },
    getAssetByIdSuccess: (
      state: AssetState,
      action: PayloadAction<IAssetDetail>
    ) => {
      state.isLoading = false;
      state.assetDetail = action.payload;
    },
    getAssetByIdFailure: (state: AssetState, action: PayloadAction<string>) => {
      state.isLoading = false;
      state.error = action.payload;
    },
    resetState: () => initialState,
  },
});

export const {
  getAssetById,
  getAssetByIdSuccess,
  getAssetByIdFailure,
  resetState,
} = assetDetailSlice.actions;

export default assetDetailSlice.reducer;
