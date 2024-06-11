import { lazy } from "react";
import { Routes, Route } from "react-router-dom";
import SuspenseLoading from "../components/SuspenseLoading";
import { HOME, TODO_ITEM } from "../constants/pages";
import PublicRoute from "./PublicRoute";

const Home = lazy(() => import("../features/home"));
const TodoItems = lazy(() => import("../features/todo-item"));

const AppRoutes = () => {
    return (
        <SuspenseLoading>
            <Routes>
                <Route
                    path={HOME}
                    element={
                        <PublicRoute>
                            <Home/>
                        </PublicRoute>
                    }
                />
                <Route
                    path={TODO_ITEM}
                    element={
                        <PublicRoute>
                            <TodoItems/>
                        </PublicRoute>
                    }
                />
            </Routes>
        </SuspenseLoading>
    )
}

export default AppRoutes;



