import React, { useEffect } from "react";
import Layout from "../features/layout/Layout";
import { useAppState } from "../redux/redux-hooks";
import { Navigate, useNavigate } from "react-router-dom";
import InlineLoader from "../components/InlineLoader";

interface Props {
  children: React.ReactNode;
  showSidebar?: boolean;
}

const PrivateRoute: React.FC<Props> = ({ children, showSidebar = true }) => {
  const navigate = useNavigate();
  const { isAuthenticated, isCheckingSession } = useAppState(
    (state) => state.auth
  );

  useEffect(() => {
    if (!isCheckingSession && !isAuthenticated) navigate("/auth/login");
  }, [isAuthenticated, isCheckingSession, navigate]);

  if (isCheckingSession) return <InlineLoader />;

  if (!isAuthenticated) return <Navigate to={"/auth/login"} />;

  return <Layout showSidebar={showSidebar}>{children}</Layout>;
};

export default PrivateRoute;
