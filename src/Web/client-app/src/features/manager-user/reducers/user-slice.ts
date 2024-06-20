import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/IUserQuery';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IUser, IUserDetail } from '../interfaces/IUser';

interface UserState {
  isLoading: boolean;
  users?: IPagedModel<IBriefUser>;
  status?: number;
  user?: any;
  error?: string | null;
  succeed: boolean;
}

const initialState: UserState = {
  isLoading: false,
  user: null,
  error: null,
  succeed: false,
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
      action: PayloadAction<IPagedModel<IBriefUser>>
    ) => {
      let users = action.payload;
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
    createUser: (
      state: UserState,
      action: PayloadAction<IUser>
    ): UserState => ({
      ...state,
      isLoading: true,
      error: null,
      succeed: false,
    }),
    setCreateUser: (state: UserState, action: PayloadAction<IUser>) => ({
      ...state,
      user: action.payload,
      isLoading: false,
      error: null,
      succeed: true,
    }),
    setCreateUserError: (state: UserState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      succeed: false,
      error: action.payload,
    }),
  },
});

export const {
  getUsers,
  setUsers,
  getUserById,
  setUserById,
  setUserByIdError,
  editUser,
  updateUser,
  updateUserError,
  createUser,
  setCreateUser,
  setCreateUserError,
} = UserSlice.actions;

export default UserSlice.reducer;
