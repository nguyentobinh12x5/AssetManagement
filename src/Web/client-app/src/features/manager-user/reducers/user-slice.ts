import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IUser } from '../interfaces/IUser';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IUserTypeQuery } from '../interfaces/IUserTypeQuery';
import { IUserSearchQuery } from '../interfaces/IUserSearchQuery';

interface User {
  id: string;
  isDelete: boolean;
}
// ??? Ask Tam why he made separate User interface from IBriefUser

interface UserState {
  isLoading: boolean;
  users?: IPagedModel<IBriefUser & User>;
  status?: number;
  user?: any;
  error?: string | null;
  succeed: boolean;
  isDeleting: boolean;
}

const initialState: UserState = {
  isLoading: false,
  user: {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false,
  },
  error: null,
  succeed: false,
  isDeleting: false,
};

const UserSlice = createSlice({
  name: 'users',
  initialState,
  reducers: {
    getUsers: (
      state: UserState,
      action: PayloadAction<IUserQuery>
    ): UserState => ({
      ...state,
      isLoading: true,
    }),
    getUsersByType: (
      state: UserState,
      action: PayloadAction<IUserTypeQuery>
    ): UserState => ({
      ...state,
      isLoading: true,
    }),
    getUsersBySearchTerm: (
      state: UserState,
      action: PayloadAction<IUserSearchQuery>
    ): UserState => ({
      ...state,
      isLoading: true,
    }),
    getUserById: (
      state: UserState,
      action: PayloadAction<string>
    ): UserState => ({
      ...state,
      isLoading: true,
      error: null,
      succeed: false,
    }),
    setUserById: (state: UserState, action: PayloadAction<IUser>) => ({
      ...state,
      user: action.payload,
      isLoading: false,
      error: null,
    }),
    setUsers: (
      state: UserState,
      action: PayloadAction<IPagedModel<IBriefUser & User>>
    ) => {
      const users = action.payload;
      return {
        ...state,
        users: users,
      };
    },
    setUserByIdError: (state: UserState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    editUser: (state: UserState, action: PayloadAction<IUser>): UserState => ({
      ...state,
      isLoading: true,
      error: null,
      succeed: false,
    }),
    updateUser: (state: UserState, action: PayloadAction<IUser>) => {
      const updatedUser = action.payload;

      return {
        ...state,
        user: updatedUser,
        isLoading: false,
        error: null,
        succeed: true,
      };
    },
    updateUserError: (state: UserState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      succeed: false,
      error: action.payload,
    }),
    deleteUser: (state, action: PayloadAction<string>) => {
      state.isDeleting = true;
      const userId = action.payload;
      const user = state.users?.items.find((user) => user.id === userId);
      if (user) {
        user.isDelete = true;
      }
    },
    setDeleteStatus: (state, action: PayloadAction<boolean>) => {
      state.isDeleting = action.payload;
    },
  },
});

export const {
  getUsers,
  setUsers,
  getUsersByType,
  getUsersBySearchTerm,
  getUserById,
  setUserById,
  setUserByIdError,
  editUser,
  updateUser,
  updateUserError,
  deleteUser,
  setDeleteStatus,
} = UserSlice.actions;

export default UserSlice.reducer;
