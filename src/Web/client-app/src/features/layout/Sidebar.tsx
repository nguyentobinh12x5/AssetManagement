import { NavLink } from "react-router-dom";
import { HOME_LINK, TODO_ITEM_LINK } from "../../constants/pages";

const Sidebar = () => {
  return (
    <div className="nav-left mb-5">
      <img src="/images/Logo_lk.png" alt="logo" />
      <p className="user intro-x">Online Asset Management</p>
      <NavLink className="navItem intro-x" to={HOME_LINK}>
        <button className="btnCustom">Home</button>
      </NavLink>
      <NavLink className="navItem intro-x" to={TODO_ITEM_LINK}>
        <button className="btnCustom">Todo Items</button>
      </NavLink>
    </div>
  );
};

export default Sidebar;
