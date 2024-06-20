import { lazy } from "react";
import { Routes, Route } from "react-router-dom";
import SuspenseLoading from "../components/SuspenseLoading";
import { AUTH, USER, HOME, TODO_ITEM } from "../constants/pages";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";
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
        <Route path={"*"} element={<div>Notfound</div>} />
      </Routes>
    </SuspenseLoading>
  );
};

export default AppRoutes;
