import React, { Suspense, useEffect } from "react";
import { useAppDispatch, useAppState } from "../../redux/redux-hooks";
import { getUserInfo } from "./reducers/auth-slice";
import FirtimeLoginChangePassword from "./firstime-login";

interface Props {
  children: React.ReactNode;
}

const AuthProvider: React.FC<Props> = ({ children }) => {
  const { user } = useAppState((state) => state.auth);
  const dispatch = useAppDispatch();

  useEffect(() => {
    const checkAuthSession = () => {
      dispatch(getUserInfo());
    };
    if (!user) checkAuthSession();
  }, [dispatch, user]);
  return (
    <Suspense>
      {user && user.mustChangePassword && <FirtimeLoginChangePassword />}
      {children}
    </Suspense>
  );
};

export default AuthProvider;
