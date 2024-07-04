import React, { useEffect, useMemo } from "react";
import Layout from "../features/layout/Layout";
import { useAppState } from "../redux/redux-hooks";
import { Navigate, useNavigate } from "react-router-dom";
import InlineLoader from "../components/InlineLoader";
import { ACCESS_DENIED_LINK, LOGIN_LINK } from "../constants/pages";

interface Props {
  children: React.ReactNode;
  showSidebar?: boolean;
  roles?: string[];
}

const PrivateRoute: React.FC<Props> = ({
  children,
  showSidebar = true,
  roles,
}) => {
  const navigate = useNavigate();
  const { isAuthenticated, isCheckingSession, user } = useAppState(
    (state) => state.auth
  );

  useEffect(() => {
    if (isCheckingSession) return;

    if (!isAuthenticated) {
      navigate(LOGIN_LINK);
      return;
    }

    if (user && roles && !roles.some((r) => user.roles.includes(r))) {
      navigate(ACCESS_DENIED_LINK);
    }
  }, [isAuthenticated, isCheckingSession, navigate, roles, user]);

  const isInRole = useMemo(
    () => user && roles && roles.some((r) => user.roles.includes(r)),
    [roles, user]
  );

  if (isCheckingSession) return <InlineLoader />;
  if (!isAuthenticated) return <Navigate to={LOGIN_LINK} />;
  if (!isInRole) return <Navigate to={ACCESS_DENIED_LINK} />;

  return <Layout showSidebar={showSidebar}>{children}</Layout>;
};

export default PrivateRoute;
