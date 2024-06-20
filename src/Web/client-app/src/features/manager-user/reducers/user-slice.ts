import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/IUserQuery';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IUserCommand } from '../interfaces/IUserCommand';
import { IUserDetail} from "../interfaces/IUserDetail";

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
  users: undefined,
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
    setUserById: (state: UserState, action: PayloadAction<IUserDetail>) => ({
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
    editUser: (state: UserState, action: PayloadAction<IUserCommand>): UserState => ({
      ...state,
      isLoading: true,
      error: null,
      succeed: false,
    }),
    updateUser: (state: UserState, action: PayloadAction<IUserCommand>) => {
      const existingUser: IBriefUser = state.users!.items.find(u => u.id === action.payload.id)!;
      
      const updatedUser: IBriefUser = {
        ...existingUser,
        fullName: `${action.payload.firstName} ${action.payload.lastName}`,
        joinDate: action.payload.joinDate,
      };
      
      const updatedUsers = state.users?.items.filter(
        (user) => user.id !== updatedUser.id
      ) ?? [];
      
      return {
        ...state,
        users:  {
          ...state.users!,
          items: [updatedUser, ...updatedUsers],
        },
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
      action: PayloadAction<IUserCommand>
    ): UserState => ({
      ...state,
      isLoading: true,
      error: null,
      succeed: false,
    }),
    setCreateUser: (state: UserState, action: PayloadAction<IUserCommand>) => ({
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
