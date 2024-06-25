import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { IBriefAsset } from '../interfaces/IBriefAsset';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { DEFAULT_MANAGE_ASSET_SORT_COLUMN } from '../constants/asset-sort';
import { ICreateAssetCommand } from '../interfaces/ICreateAssetCommand';
import { IAssetDetail } from '../interfaces/IAssetDetail';

const defaultAssetQuery: IAssetQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnName: DEFAULT_MANAGE_ASSET_SORT_COLUMN,
  sortColumnDirection: ASCENDING,
  categoryName: ['All'],
  assetStatusName: ['All'],
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
};
const AssetSlice = createSlice({
  initialState,
  name: 'asset',
  reducers: {
    getAssets: (state: AssetState, action: PayloadAction<IAssetQuery>) => ({
      ...state,
      isLoading: true,
      isDataFetched: false,
    }),
    createAsset: (
      state: AssetState,
      action: PayloadAction<ICreateAssetCommand>
    ) => ({
      ...state,
      isLoading: true,
      succeed: false,
    }),

    setAssets: (
      state: AssetState,
      action: PayloadAction<IPagedModel<IBriefAsset>>
    ) => {
      let assets = action.payload;
      return {
        ...state,
        assets: assets,
        isDataFetched: true,
      };
    },

    // Success handles
    getAssetsSuccess: (state: AssetState, action: PayloadAction<any>) => ({
      ...state,
      isLoading: true,
      assets: action.payload,
    }),
    createAssetSuccess: (
      state: AssetState,
      action: PayloadAction<IAssetDetail>
    ) => {
      const { id, categoryName, assetStatusName, code, name } = action.payload;
      const newAsset: IBriefAsset = {
        id: id,
        categoryName: categoryName,
        assetStatusName: assetStatusName,
        code: code,
        name: name,
      };

      return {
        ...state,
        isLoading: false,
        succeed: true,
        assets: {
          ...state.assets,
          items: [newAsset, ...state.assets.items],
        },
      };
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
    },
    setIsDataFetched: (state: AssetState, action: PayloadAction<boolean>) => {
      state.isDataFetched = action.payload;
    },
  },
});

export const {
  createAsset,
  getAssets,
  setAssets,
  getAssetsSuccess,
  getAssetsFailure,
  getAssetStatuses,
  setAssetStatuses,
  getAssetCategories,
  setAssetCategories,
  setAssetQuery,
  setIsDataFetched,
  createAssetFailure,
  createAssetSuccess,
} = AssetSlice.actions;

export default AssetSlice.reducer;
