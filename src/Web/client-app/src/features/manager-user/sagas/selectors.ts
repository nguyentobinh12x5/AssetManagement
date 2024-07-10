import { RootState } from '../../../redux/store';

export const getUserQuery = (state: RootState) => state.users.userQuery;

export const getAssetQuery = (state: RootState) => state.assets.assetQuery;
