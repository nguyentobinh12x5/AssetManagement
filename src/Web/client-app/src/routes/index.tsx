import { lazy } from "react";
import { Routes, Route } from "react-router-dom";
import SuspenseLoading from "../components/SuspenseLoading";
import { HOME, AUTH, MANAGE_USER, TODO_ITEM, ASSETS } from "../constants/pages";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";
import Assets from "../features/asset";

const Home = lazy(() => import("../features/home"));
const TodoItems = lazy(() => import("../features/todo-item"));
const Users = lazy(() => import("../features/manage-user"));
const Auth = lazy(() => import("../features/auth"));

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
          path={TODO_ITEM}
          element={
            <PrivateRoute>
              <TodoItems />
            </PrivateRoute>
          }
        />
        <Route
          path={MANAGE_USER}
          element={
            <PublicRoute>
              <Users />
            </PublicRoute>
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
