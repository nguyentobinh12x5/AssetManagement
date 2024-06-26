import { Dropdown } from "react-bootstrap";
import { useAppDispatch, useAppState } from "../../redux/redux-hooks";
import { Link, useLocation } from "react-router-dom";
import ChangePasswordForm from "../auth/changepassword/ChangePasswordForm";
import { useState } from "react";
import { logout } from "../auth/reducers/auth-slice";
import { IUserInfo } from "../auth/interfaces/IUserInfo";
import BreadCrumbs from "./BreadCrumbs";

const Header = () => {
  const { user, isAuthenticated } = useAppState((state) => state.auth);

  return (
    <div className="header align-items-center font-weight-bold">
      <div className="container-lg-min mh-100 container-fluid d-flex justify-content-between py-1">
        <BreadCrumbs />

        {user ? (
          <UserDropdown user={user} />
        ) : (
          !isAuthenticated && (
            <Link
              to={"/auth/login"}
              className="text-white text-decoration-none"
            >
              Login
            </Link>
          )
        )}
      </div>
    </div>
  );
};

export default Header;

const UserDropdown = (props: { user: IUserInfo }) => {
  const { user } = props;
  const dispatch = useAppDispatch();

  const [showChangePasswordModal, setShowChangePasswordModal] = useState(false);

  const handleShowChangePasswordModal = () => setShowChangePasswordModal(true);
  const handleCloseChangePasswordModal = () =>
    setShowChangePasswordModal(false);

  const handleLogout = () => {
    dispatch(logout());
  };
  return (
    <>
      <Dropdown data-bs-theme="dark" align={"end"}>
        <Dropdown.Toggle className="bg-transparent border-0">
          {user?.username}
        </Dropdown.Toggle>

        <Dropdown.Menu>
          {/* <Dropdown.Item href="#/action-1" active>
            Action
          </Dropdown.Item>
          <Dropdown.Item href="#/action-2">Another action</Dropdown.Item>
          <Dropdown.Item href="#/action-3">Something else</Dropdown.Item>
          <Dropdown.Divider /> */}
          <Dropdown.Item onClick={handleLogout}>Logout</Dropdown.Item>
          <Dropdown.Item onClick={handleShowChangePasswordModal}>
            Change Password
          </Dropdown.Item>
        </Dropdown.Menu>
      </Dropdown>

      <ChangePasswordForm
        show={showChangePasswordModal}
        onHide={handleCloseChangePasswordModal}
      />
    </>
  );
};
