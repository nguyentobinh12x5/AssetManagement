import { lazy } from "react";
import { Routes, Route } from "react-router-dom";
import SuspenseLoading from "../components/SuspenseLoading";
import {
  HOME,
  AUTH,
  ASSETS,
  USER,
  ACCESS_DENIED,
  ASSIGNMENTS,
} from "../constants/pages";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";
import Assets from "../features/asset";
import Assignments from "../features/assignment";

const Home = lazy(() => import("../features/home"));
const Auth = lazy(() => import("../features/auth"));
const Users = lazy(() => import("../features/manager-user"));
const AccessDenied = lazy(() => import("../features/access-denied"));

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
            <PrivateRoute roles={["Administrator"]}>
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
            <PrivateRoute roles={["Administrator"]}>
              <Assets />
            </PrivateRoute>
          }
        />
        <Route
          path={ASSIGNMENTS}
          element={
            <PrivateRoute roles={["Administrator"]}>
              <Assignments />
            </PrivateRoute>
          }
        />
        <Route
          path={ACCESS_DENIED}
          element={
            <PublicRoute showSidebar={false}>
              <AccessDenied />
            </PublicRoute>
          }
        />
        <Route path={"*"} element={<div>Notfound</div>} />
      </Routes>
    </SuspenseLoading>
  );
};

export default AppRoutes;
