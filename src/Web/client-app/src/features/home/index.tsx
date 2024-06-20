import { APP_CONFIG } from "../../constants/appConfig";
import { useAppState } from "../../redux/redux-hooks";
import { isAdminUser } from "../../utils/authUtils";

const Home = () => {
  const { user } = useAppState((state) => state.auth);
  return (
    <div className="container m-auto p-5">
      <h1>Welcome to Asset Management {APP_CONFIG.ENVIRONMENT}</h1>
      <p>Test Dev ENV</p>
      <p>
        Welcome {!user ? "Anonymous" : isAdminUser(user) ? "Admin" : "Staff"}
      </p>
    </div>
  );
};

export default Home;


