import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AssetState {
  assets: any[];
  isLoading: boolean;
  error?: string | null;
}

const initialState: AssetState = {
  assets: [],
  isLoading: false,
};
const AssetSlice = createSlice({
  initialState,
  name: 'asset',
  reducers: {
    getAssets: (state: AssetState) => ({
      ...state,
      isLoading: true,
    }),

    // Success handles
    getAssetsSuccess: (state: AssetState, action: PayloadAction<any>) => ({
      ...state,
      isLoading: true,
      assets: action.payload,
    }),

    // Failure handles
    getAssetsFailure: (state: AssetState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: true,
      error: action.payload,
    }),
  },
});

export const { getAssets, getAssetsSuccess, getAssetsFailure } =
  AssetSlice.actions;

export default AssetSlice.reducer;
