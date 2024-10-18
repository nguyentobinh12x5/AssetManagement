import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { IBriefAsset } from '../interfaces/IBriefAsset';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { DEFAULT_MANAGE_ASSET_SORT_COLUMN } from '../constants/asset-sort';
import { ICreateAssetCommand } from '../interfaces/ICreateAssetCommand';
import { IAssetDetail } from '../interfaces/IAssetDetail';
import { IEditAssetCommand } from '../interfaces/IEditAssetCommand';
import { ICheckHistoricalAssignment } from '../interfaces/ICheckHistoricalAssignment';

const defaultAssetQuery: IAssetQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnName: DEFAULT_MANAGE_ASSET_SORT_COLUMN,
  sortColumnDirection: ASCENDING,
  category: ['All'],
  assetStatus: ['Assigned', 'Available', 'Not available'],
  searchTerm: '',
};

interface AssetState {
  assets: IPagedModel<IBriefAsset>;
  isLoading: boolean;
  error?: string | null;
  statuses: string[];
  categories: string[];
  assetQuery: IAssetQuery;
  isDataFetched: boolean;
  isBelongToHistoricalAssignment: boolean;
}

const initialState: AssetState = {
  assets: {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false,
  },
  isLoading: false,
  statuses: ['All'],
  categories: ['All'],
  assetQuery: defaultAssetQuery,
  isDataFetched: false,
  error: null,
  isBelongToHistoricalAssignment: false,
};
const AssetSlice = createSlice({
  initialState,
  name: 'asset',
  reducers: {
    getAssets: (state: AssetState, action: PayloadAction<IAssetQuery>) => ({
      ...state,
      isLoading: true,
      isDataFetched: false,
      isBelongToHistoricalAssignment: false,
    }),
    createAsset: (
      state: AssetState,
      action: PayloadAction<ICreateAssetCommand>
    ) => ({
      ...state,
      isLoading: true,
      succeed: false,
    }),

    editAsset: (
      state: AssetState,
      action: PayloadAction<IEditAssetCommand>
    ) => ({
      ...state,
      isLoading: true,
    }),

    deleteAssets: (state: AssetState, action: PayloadAction<number>) => ({
      ...state,
      isLoading: true,
      error: null,
    }),
    // Success handles
    getAssetsSuccess: (
      state: AssetState,
      action: PayloadAction<IPagedModel<IBriefAsset>>
    ) => ({
      ...state,
      assets: action.payload,
      isDataFetched: true,
    }),
    createAssetSuccess: (
      state: AssetState,
      action: PayloadAction<IAssetDetail>
    ) => {
      const { id, categoryName, assetStatusName, code, name } = action.payload;
      const newAsset: IBriefAsset = {
        id: id,
        category: categoryName,
        assetStatus: assetStatusName,
        code: code,
        name: name,
        isEnableAction: true,
      };

      const restAssets =
        state.assets.items.length < state.assetQuery.pageSize
          ? state.assets.items
          : state.assets.items.slice(1);

      return {
        ...state,
        isLoading: false,
        succeed: true,
        assets: {
          ...state.assets,
          items: [newAsset, ...restAssets],
        },
      };
    },

    editAssetSuccess: (
      state: AssetState,
      action: PayloadAction<IAssetDetail>
    ) => {
      const { id, categoryName, assetStatusName, code, name } = action.payload;
      const updatedAsset: IBriefAsset = {
        id: id,
        category: categoryName,
        assetStatus: assetStatusName,
        code: code,
        name: name,
        isEnableAction: true,
      };

      const items =
        state.assets?.items.filter((asset) => asset.id !== updatedAsset.id) ??
        [];

      return {
        ...state,
        isLoading: false,
        assets: {
          ...state.assets,
          items: [updatedAsset, ...items],
        },
      };
    },
    setDeleteAsset: (state: AssetState, action: PayloadAction<number>) => {
      state.assets.items = state.assets.items.filter(
        (asset) => asset.id !== action.payload.toString()
      );
      state.isLoading = false;
      state.isDataFetched = false;
    },
    // Failure handles
    getAssetsFailure: (state: AssetState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: true,
      succeed: false,
      error: action.payload,
    }),
    createAssetFailure: (state: AssetState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      succeed: false,
      error: action.payload,
    }),
    editAssetFailure: (state: AssetState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    getAssetStatuses: (state: AssetState) => {
      state.isLoading = true;
    },
    setAssetStatuses: (state: AssetState, action: PayloadAction<string[]>) => {
      state.statuses = ['All', ...action.payload];
      state.isLoading = false;
    },
    getAssetCategories: (state: AssetState) => {
      state.isLoading = true;
    },
    setAssetCategories: (
      state: AssetState,
      action: PayloadAction<string[]>
    ) => {
      state.categories = ['All', ...action.payload];
      state.isLoading = false;
    },
    setAssetQuery: (state: AssetState, action: PayloadAction<IAssetQuery>) => {
      state.assetQuery = action.payload;
      state.isDataFetched = false;
    },
    deleteAssetFailure: (state: AssetState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    resetAssetSlice: () => initialState,
    checkHistoricalAssignment: (
      state: AssetState,
      action: PayloadAction<ICheckHistoricalAssignment>
    ) => ({
      ...state,
      isLoading: true,
    }),
    checkHistoricalAssignmentSuccess: (state: AssetState) => ({
      ...state,
      isBelongToHistoricalAssignment: true,
      isLoading: false,
    }),
    checkHistoricalAssignmentFail: (state: AssetState) => ({
      ...state,
      isBelongToHistoricalAssignment: false,
      isLoading: false,
    }),
    resetHistoricalAssignment: (state: AssetState) => ({
      ...state,
      isBelongToHistoricalAssignment: false,
    }),
  },
});

export const {
  createAsset,
  editAsset,
  getAssets,
  getAssetsSuccess,
  getAssetsFailure,
  getAssetStatuses,
  setAssetStatuses,
  getAssetCategories,
  setAssetCategories,
  setAssetQuery,
  createAssetFailure,
  createAssetSuccess,
  editAssetSuccess,
  editAssetFailure,
  deleteAssets,
  setDeleteAsset,
  deleteAssetFailure,
  resetAssetSlice,
  checkHistoricalAssignment,
  resetHistoricalAssignment,
  checkHistoricalAssignmentSuccess,
  checkHistoricalAssignmentFail,
} = AssetSlice.actions;

export default AssetSlice.reducer;
