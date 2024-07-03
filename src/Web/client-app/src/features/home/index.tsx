import { Route, Routes } from "react-router-dom";
import { APP_CONFIG } from "../../constants/appConfig";
import { useAppState } from "../../redux/redux-hooks";
import { MY_ASSIGNMENT_LIST } from "./constants/my-assignment-list";
import MyAssignments from "./my-list";

const Home = () => {
  const { user, isAuthenticated } = useAppState((state) => state.auth);

  if (!isAuthenticated || !user)
    return (
      <>
        <h1>Welcome to Asset Management {APP_CONFIG.ENVIRONMENT}</h1>
        <p>Test Dev ENV</p>
        <p>Please log in to get furthur information</p>
      </>
    );

  return (
    <Routes>
      <Route path={MY_ASSIGNMENT_LIST} element={<MyAssignments />} />
    </Routes>
  );
};

export default Home;
