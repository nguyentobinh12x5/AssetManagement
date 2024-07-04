import React, { useEffect, useMemo } from "react";
import { Suspense } from "react";
import { useAppDispatch, useAppState } from "../../redux/redux-hooks";
import { getUserInfo } from "./reducers/auth-slice";
import FirstTimeLoginChangePassword from "./firstime-login";

interface Props {
  children: React.ReactNode;
}

const AuthProvider: React.FC<Props> = ({ children }) => {
  const { user } = useAppState((state) => state.auth);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!user) {
      dispatch(getUserInfo());
    }
  }, [dispatch, user]);

  const shouldRenderChangePassword = useMemo(
    () => user && user.mustChangePassword,
    [user]
  );

  return (
    <>
      {shouldRenderChangePassword && <FirstTimeLoginChangePassword />}
      {user ? (
        <Suspense fallback={<div>Loading...</div>}>{children}</Suspense>
      ) : null}
    </>
  );
};

export default AuthProvider;
