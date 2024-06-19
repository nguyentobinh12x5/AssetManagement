import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IUser } from '../interfaces/IUser';

interface UserState {
    isLoading: boolean;
    users?: IUser[];
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
        // Add the createUser actions and reducers
        createUser: (state: UserState, action: PayloadAction<IUser>): UserState => ({
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
