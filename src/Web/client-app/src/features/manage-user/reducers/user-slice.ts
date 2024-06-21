import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { IUserQuery } from '../interfaces/IUserQuery';
import { IUser } from '../interfaces/IUser';

interface User {
  id: string;
  isDelete: boolean;
}

interface UserState {
  isLoading: boolean;
  isDeleting: boolean;
  users?: IPagedModel<IUser & User>;
  status?: number;
}

const initialState: UserState = {
  isLoading: false,
  isDeleting: false,
  users: {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false,
  },
};

const usersSlice = createSlice({
  name: 'users',
  initialState,
  reducers: {
    getUsers: (state, action: PayloadAction<IUserQuery>) => {
      state.isLoading = true;
    },
    setUsers: (state, action: PayloadAction<IPagedModel<IUser & User>>) => {
      state.isLoading = false;
      state.users = action.payload;
    },
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

export const { getUsers, setUsers, deleteUser, setDeleteStatus } =
  usersSlice.actions;
export default usersSlice.reducer;
