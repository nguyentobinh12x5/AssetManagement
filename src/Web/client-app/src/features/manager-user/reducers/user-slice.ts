import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { IBriefUser } from '../interfaces/IBriefUser';
import { IUserCommand } from '../interfaces/IUserCommand';
import { IUserDetail } from '../interfaces/IUserDetail';
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from '../../../constants/paging';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
const defaultUserQuery: IUserQuery = {
  pageNumber: 1,
  pageSize: APP_DEFAULT_PAGE_SIZE,
  sortColumnName: DEFAULT_MANAGE_USER_SORT_COLUMN,
  sortColumnDirection: ASCENDING,
  searchTerm: '',
  types: ['All'],
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
  isDataFetched: boolean;
  types: string[];
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
  types: ['All'],
  isDataFetched: false,
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
        isDataFetched: true,
      };
    },
    setIsDataFetched: (state: UserState, action: PayloadAction<boolean>) => {
      state.isDataFetched = action.payload;
    },
    setUserQuery: (state: UserState, action: PayloadAction<IUserQuery>) => {
      state.userQuery = action.payload;
      state.isDataFetched = false;
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
      isDataFetched: true,
    }),
    updateUser: (state: UserState, action: PayloadAction<IUserCommand>) => {
      const existingUser: IBriefUser = state.users!.items.find(
        (u) => u.id === action.payload.id
      )!;

      const updatedUser: IBriefUser = {
        ...existingUser,
        fullName: `${action.payload.firstName} ${action.payload.lastName}`,
        joinDate: action.payload.joinDate,
        type: action.payload.type,
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
    createUser: (state: UserState, action: PayloadAction<IUserCommand>) => {
      return {
        ...state,
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

    setCreateUser: (state: UserState, action: PayloadAction<IUserDetail>) => {
      const newUser: IBriefUser = {
        id: action.payload.id ?? 'something clearly wrong here',
        fullName: `${action.payload.firstName} ${action.payload.lastName}`,
        joinDate: action.payload.joinDate,
        staffCode: action.payload.staffCode,
        type: action.payload.type,
        userName: action.payload.username,
        isDelete: false,
      };
      const restUsers =
        state.users.items.length < state.userQuery.pageSize
          ? state.users.items
          : state.users.items.slice(1);

      return {
        ...state,
        users: {
          ...state.users!,
          items: [newUser, ...restUsers],
        },
        isLoading: false,
        error: null,
        succeed: true,
      };
    },
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
    removeUser: (state, action: PayloadAction<string>) => {
      state.users.items = state.users.items.filter(
        (user) => user.id !== action.payload
      );
      state.isDataFetched = false;
    },
    setDeleteStatus: (state, action: PayloadAction<boolean>) => {
      state.isDeleting = action.payload;
    },
    setSucceedStatus: (state, action: PayloadAction<boolean>) => {
      state.succeed = action.payload;
    },
    getUserTypes: (state: UserState) => {
      state.isLoading = true;
    },
    setUserTypes: (state: UserState, action: PayloadAction<string[]>) => {
      state.types = ['All', ...action.payload];
      state.isLoading = false;
    },
    resetUserSlice: () => initialState,
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
  deleteUser,
  setDeleteStatus,
  setUserQuery,
  setIsDataFetched,
  setSucceedStatus,
  getUserTypes,
  setUserTypes,
  removeUser,
  resetUserSlice,
} = UserSlice.actions;

export default UserSlice.reducer;
