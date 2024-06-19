import React from "react";
import Layout from "../features/layout/Layout";
import { useAppState } from "../redux/redux-hooks";
import { Navigate } from "react-router-dom";

interface Props {
  children: React.ReactNode;
  showSidebar?: boolean;
}

const PrivateRoute: React.FC<Props> = ({ children, showSidebar = true }) => {
  const { isAuthenticated } = useAppState((state) => state.auth);

  if (!isAuthenticated) return <Navigate to={"/auth/login"} />;

  return <Layout showSidebar={showSidebar}>{children}</Layout>;
};

export default PrivateRoute;
