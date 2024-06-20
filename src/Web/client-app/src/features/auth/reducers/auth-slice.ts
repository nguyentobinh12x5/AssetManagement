import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ILoginCommand } from '../interfaces/ILoginCommand';
import { IChangePasswordFirstTimeCommand } from '../interfaces/IChangePasswordFirstTimeCommand';

interface AuthState {
  isLoading: boolean;
  user?: any;
  isAuthenticated: boolean;
  loginError?: string;
}

const initialState: AuthState = {
  isLoading: false,
  isAuthenticated: false,
};

const AuthSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    // Saga actions
    login: (state: AuthState, action: PayloadAction<ILoginCommand>) => ({
      ...state,
      isLoading: true,
    }),
    getUserInfo: (state: AuthState) => ({
      ...state,
      isLoading: true,
    }),
    changePasswordFirstTime: (
      state: AuthState,
      action: PayloadAction<IChangePasswordFirstTimeCommand>
    ) => ({
      ...state,
      isLoading: true,
    }),
    logout: (state: AuthState) => ({
      ...state,
      isLoading: false,
    }),

    // Set state
    setUser: (state: AuthState, action: PayloadAction) => ({
      ...state,
      user: action.payload,
      isLoading: false,
      isAuthenticated: true,
    }),
    setAuth: (state: AuthState, action: PayloadAction) => ({
      ...state,
      isLoading: false,
      isAuthenticated: true,
    }),
    loginFail: (state: AuthState, action: PayloadAction<string>) => ({
      ...state,
      isLoading: false,
      loginError: action.payload,
    }),
    changePasswordFirstTimeSuccess: (state: AuthState) => ({
      ...state,
      isLoading: false,
      user: {
        ...state.user,
        mustChangePassword: false,
      },
    }),
    setLogout: () => initialState,
  },
  extraReducers(builder) {},
});

export const {
  login,
  setAuth,
  loginFail,
  logout,
  setUser,
  getUserInfo,
  setLogout,
  changePasswordFirstTime,
  changePasswordFirstTimeSuccess,
} = AuthSlice.actions;

export default AuthSlice.reducer;
