import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import { IUserQuery } from "../interfaces/IUserQuery";
import { IUser } from "../interfaces/IUser";


interface UserState {
    isLoading: boolean,
    users?: IPagedModel<IUser>
    status?: number;
}

const initialState: UserState = {
    isLoading: false
}

const UsersSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        getUsers: (
            state: UserState,
            action: PayloadAction<IUserQuery>
        ): UserState => ({
            ...state,
            isLoading: true
        }),
        setUsers: (
            state: UserState,
            action: PayloadAction<IPagedModel<IUser>>
        ) => {
            const users = action.payload;
            return {
                ...state,
                users
            }
        }
    }
})

export const {
    getUsers,
    setUsers
} = UsersSlice.actions;

export default UsersSlice.reducer;