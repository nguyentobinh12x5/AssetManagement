import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface User {
    id: string;
    isDelete: boolean;
}

interface UserState {
    isDeleting: boolean;
    users: User[];
}

const initialState: UserState = {
    isDeleting: false,
    users: [] // Initialize the users array
};

const userSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        deleteUser: (state, action: PayloadAction<string>) => {
            state.isDeleting = true;
            const userId = action.payload;
            // Mark the user as deleted (soft delete)
            const user = state.users.find(user => user.id === userId);
            if (user) {
                user.isDelete = true;
            }
        },
        setUsers: (state, action: PayloadAction<User[]>) => {
            state.users = action.payload;
        },
        setDeleteStatus: (state, action: PayloadAction<boolean>) => {
            state.isDeleting = action.payload;
        }
    },
});

export const { deleteUser, setUsers, setDeleteStatus } = userSlice.actions;
export default userSlice.reducer;
