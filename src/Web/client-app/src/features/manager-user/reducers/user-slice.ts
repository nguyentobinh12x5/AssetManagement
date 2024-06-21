import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IUserCommand } from '../interfaces/IUserCommand';
import { IUserDetail } from '../interfaces/IUserDetail';
import { IUserTypeQuery } from '../interfaces/IUserTypeQuery';
import { IUserSearchQuery } from '../interfaces/IUserSearchQuery';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
// ??? Ask Tam why he made separate User interface from IBriefUser
const defaultUserQuery: IUserQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnName: DEFAULT_MANAGE_USER_SORT_COLUMN,
  sortColumnDirection: ASCENDING,
};
interface UserState {
  isLoading: boolean;
  users: IPagedModel<IBriefUser>;
  status?: number;
  user?: any;
  error?: string | null;
  succeed: boolean;
  isDeleting: boolean;
  userQuery: IUserQuery;
}

const initialState: UserState = {
  isLoading: false,
  user: null,
  users: {
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
  userQuery: defaultUserQuery,
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
    setUserQuery: (state: UserState, action: PayloadAction<IUserQuery>) => {
      state.userQuery = action.payload;
    },
    setUserByIdError: (state: UserState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      error: action.payload,
    }),
    editUser: (
      state: UserState,
      action: PayloadAction<IUserCommand>
    ): UserState => ({
      ...state,
      isLoading: true,
      error: null,
      succeed: false,
    }),
    updateUser: (state: UserState, action: PayloadAction<IUserCommand>) => {
      const existingUser: IBriefUser = state.users!.items.find(
        (u) => u.id === action.payload.id
      )!;

      const updatedUser: IBriefUser = {
        ...existingUser,
        fullName: `${action.payload.firstName} ${action.payload.lastName}`,
        joinDate: action.payload.joinDate,
      };

      const updatedUsers =
        state.users?.items.filter((user) => user.id !== updatedUser.id) ?? [];

      return {
        ...state,
        users: {
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
    createUser: (state: UserState, action: PayloadAction<IUserCommand>) => {
      const existingUser: IBriefUser = state.users!.items.find(
        (u) => u.id === action.payload.id
      )!;

      const updatedUser: IBriefUser = {
        ...existingUser,
        fullName: `${action.payload.firstName} ${action.payload.lastName}`,
        joinDate: action.payload.joinDate,
      };

      const updatedUsers =
        state.users?.items.filter((user) => user.id !== updatedUser.id) ?? [];

      return {
        ...state,
        users: {
          ...state.users!,
          items: [updatedUser, ...updatedUsers],
        },
        isLoading: false,
        error: null,
        succeed: true,
      };
    },
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
  createUser,
  setCreateUser,
  setCreateUserError,
  deleteUser,
  setDeleteStatus,
  setUserQuery,
} = UserSlice.actions;

export default UserSlice.reducer;
