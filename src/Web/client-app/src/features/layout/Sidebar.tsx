import { NavLink } from "react-router-dom";
import {
  HOME_LINK,
  TODO_ITEM_LINK,
  MANAGE_USER_LINK,
} from "../../constants/pages";
import { useAppState } from "../../redux/redux-hooks";
import { isAdminUser } from "../../utils/authUtils";

const Sidebar = () => {
  const { user } = useAppState((state) => state.auth);
  return (
    <div className="nav-left mb-5">
      <img src="/images/Logo_lk.png" alt="logo" />
      <p className="user intro-x">Online Asset Management</p>
      <NavLink className="navItem intro-x" to={HOME_LINK}>
        <button className="btnCustom">Home</button>
      </NavLink>
      {isAdminUser(user) && (
        <>
          <NavLink className="navItem intro-x" to={TODO_ITEM_LINK}>
            <button className="btnCustom">Todo Items</button>
          </NavLink>
          <NavLink className="navItem intro-x" to={TODO_ITEM_LINK}>
            <button className="btnCustom">Protected</button>
          </NavLink>
          <NavLink className="navItem intro-x" to={MANAGE_USER_LINK}>
            <button className="btnCustom">Manage User</button>
          </NavLink>
        </>
      )}
    </div>
  );
};

export default Sidebar;
