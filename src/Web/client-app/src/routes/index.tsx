import { lazy } from "react";
import { Routes, Route } from "react-router-dom";
import SuspenseLoading from "../components/SuspenseLoading";
import { AUTH, HOME, MANAGE_USER, TODO_ITEM } from "../constants/pages";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";

const Home = lazy(() => import("../features/home"));
const TodoItems = lazy(() => import("../features/todo-item"));
const Auth = lazy(() => import("../features/auth"));
const Users = lazy(() => import("../features/manage-user"));

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
          path={AUTH}
          element={
            <PublicRoute showSidebar={false}>
              <Auth />
            </PublicRoute>
          }
        />
        <Route
          path={MANAGE_USER}
          element={
            <PublicRoute>
              <Users/>
            </PublicRoute>
          }
        />
      </Routes>
    </SuspenseLoading>
  );
};

export default AppRoutes;
