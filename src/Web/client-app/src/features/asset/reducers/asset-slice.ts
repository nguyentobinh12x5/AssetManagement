import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { IBriefAsset } from '../interfaces/IBriefAsset';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { DEFAULT_MANAGE_ASSET_SORT_COLUMN } from '../constants/asset-sort';
import { ICreateAssetCommand } from '../interfaces/ICreateAssetCommand';
import { IAssetDetail } from '../interfaces/IAssetDetail';
import { IEditAssetCommand } from '../interfaces/IEditAssetCommand';
import { it } from 'node:test';
import { IEditAssetForm } from '../edit/edit-asset-scheme';

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

    editAsset: (
      state: AssetState,
      action: PayloadAction<IEditAssetCommand>
    ) => ({
      ...state,
      isLoading: true,
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
    deleteAssets: (state: AssetState, action: PayloadAction<number>) => ({
      ...state,
      isLoading: true,
      error: null,
    }),
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
        category: categoryName,
        assetStatus: assetStatusName,
        code: code,
        name: name,
      };
      const items =
        state.assets.items.length === state.assetQuery.pageSize
          ? state.assets.items.slice(0, state.assetQuery.pageSize - 1)
          : state.assets.items;

      return {
        ...state,
        isLoading: false,
        succeed: true,
        assets: {
          ...state.assets,
          items: [newAsset, ...items],
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
  },
});

export const {
  createAsset,
  editAsset,
  getAssets,
  setAssets,
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
} = AssetSlice.actions;

export default AssetSlice.reducer;
