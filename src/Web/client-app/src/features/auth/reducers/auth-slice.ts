import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ILoginCommand } from "../interfaces/ILoginCommand";
import { getCookie } from "../../../utils/cookiesUtils";

interface AuthState {
    isLoading: boolean;
    user?: any;
    isAuthenticated: boolean
    loginError?: string;
}

const initialState : AuthState = {
    isLoading: false,
    isAuthenticated: getCookie(".AspNetCore.Identity.Application") ? true : false
}

const AuthSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        // Saga actions
        login: (state: AuthState, action: PayloadAction<ILoginCommand>) => ({
            ...state,
            isLoading: true
        }),
        getUserInfo: (state: AuthState) => ({
            ...state,
            isLoading: true,
        }),


        // Set state 
        setUser:  (state:AuthState, action: PayloadAction) =>
            ({
                ...state,
                user: action.payload,
                isLoading: false,
                isAuthenticated: true
            }),
        setAuth: (state:AuthState, action: PayloadAction) =>
        ({
            ...state,
            isLoading: false,
            isAuthenticated: true
        }),
        loginFail: (state: AuthState, action: PayloadAction<string>) => ({
            ...state,
            isLoading: false,
            loginError: action.payload
        }),
        setLogout: () => initialState
    },
    extraReducers(builder) {
        
    },
})

export const {login, setAuth, loginFail, setUser, getUserInfo, setLogout} = AuthSlice.actions;

export default AuthSlice.reducer