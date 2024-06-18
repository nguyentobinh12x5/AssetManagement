import React, { useEffect } from "react";
import { useAppDispatch, useAppState } from "../../redux/redux-hooks";
import { getUserInfo } from "./reducers/auth-slice";

interface Props {
  children: React.ReactNode;
}

const AuthProvider: React.FC<Props> = ({ children }) => {
  const { isAuthenticated } = useAppState((state) => state.auth);
  const dispatch = useAppDispatch();

  useEffect(() => {
    const checkAuthSession = () => {
      dispatch(getUserInfo());
    };

    checkAuthSession();
  }, [dispatch, isAuthenticated]);
  return <>{children}</>;
};

export default AuthProvider;
