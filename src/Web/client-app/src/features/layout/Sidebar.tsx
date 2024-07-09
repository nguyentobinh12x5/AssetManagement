import { NavLink } from "react-router-dom";
import {
  ASSETS_LINK,
  HOME_LINK,
  USER_LINK,
  ASSIGNMENTS_LINK,
  RETURNINGS_LINK,
} from "../../constants/pages";
import { useAppState } from "../../redux/redux-hooks";
import { isAdminUser } from "../../utils/authUtils";

const Sidebar = () => {
  const { user } = useAppState((state) => state.auth);
  return (
    <div className="nav-left mb-5">
      <img src="/images/Logo_lk.png" alt="logo" />
      <p className="nav mb-4">Online Asset Management</p>
      <NavLink className="navItem intro-x" to={HOME_LINK}>
        <button className="btnCustom">Home</button>
      </NavLink>
      {isAdminUser(user) && (
        <>
          <NavLink className="navItem intro-x" to={USER_LINK}>
            <button className="btnCustom">Manage User</button>
          </NavLink>
          <NavLink className="navItem intro-x" to={ASSETS_LINK}>
            <button className="btnCustom">Manage Asset</button>
          </NavLink>
          <NavLink className="navItem intro-x" to={ASSIGNMENTS_LINK}>
            <button className="btnCustom">Manage Assignment</button>
          </NavLink>
          <NavLink className="navItem intro-x" to={RETURNINGS_LINK}>
            <button className="btnCustom">Request for Returning</button>
          </NavLink>
        </>
      )}
    </div>
  );
};

export default Sidebar;
