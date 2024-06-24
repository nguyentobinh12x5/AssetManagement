import { lazy } from "react";
import { Routes, Route } from "react-router-dom";
import SuspenseLoading from "../components/SuspenseLoading";
import { HOME, AUTH, MANAGE_USER, TODO_ITEM, ASSETS, USER } from "../constants/pages";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";
import Assets from "../features/asset";

const Home = lazy(() => import("../features/home"));
const Auth = lazy(() => import("../features/auth"));
const Users = lazy(() => import("../features/manager-user"));

const AppRoutes = () => {
  return (
    <SuspenseLoading>
      <Routes>
        <Route
          path={HOME}
          element={
            <PublicRoute>
              <Home />
            </PublicRoute>
          }
        />
        <Route
          path={USER}
          element={
            <PrivateRoute>
              <Users />
            </PrivateRoute>
          }
        />
        <Route
          path={AUTH}
          element={
            <PublicRoute showSidebar={false}>
              <Auth />
            </PublicRoute>
          }
        />
        <Route
          path={ASSETS}
          element={
            <PrivateRoute showSidebar={true}>
              <Assets />
            </PrivateRoute>
          }
        />
      </Routes>
    </SuspenseLoading>
  );
};

export default AppRoutes;
