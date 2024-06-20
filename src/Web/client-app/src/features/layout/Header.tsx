import { Dropdown } from "react-bootstrap";
import { useAppState } from "../../redux/redux-hooks";
import { Link, useLocation } from "react-router-dom";
import ChangePasswordForm from "../auth/changepassword/ChangePasswordForm";
import { useState } from "react";

const Header = () => {
  const { user, isAuthenticated } = useAppState((state) => state.auth);
  const location = useLocation();
  const isAtLoginPage = location.pathname.includes("login");
  console.log(location.pathname);
  const HeaderTitle = isAuthenticated ? "Home" : "Online Asset Management";

  return (
    <div className="header align-items-center font-weight-bold">
      <div className="container-lg-min mh-100 container-fluid d-flex justify-content-between py-1">
        <div className="d-flex align-items-center gap-2">
          {isAtLoginPage && (
            <img
              alt="Online asset management icon"
              src="/images/Logo_lk.png"
              className="header-logo"
            />
          )}
          <p className="headText">{HeaderTitle}</p>
        </div>

        {user ? (
          <UserDropdown user={user} />
        ) : (
          !isAtLoginPage && (
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

const UserDropdown = (props: any) => {
  const { user } = props;

  const [showChangePasswordModal, setShowChangePasswordModal] = useState(false);

  const handleShowChangePasswordModal = () => setShowChangePasswordModal(true);
  const handleCloseChangePasswordModal = () =>
    setShowChangePasswordModal(false);

  return (
    <>
      <Dropdown data-bs-theme="dark" align={"end"}>
        <Dropdown.Toggle className="bg-transparent border-0">
          {user.email}
        </Dropdown.Toggle>

        <Dropdown.Menu>
          {/* <Dropdown.Item href="#/action-1" active>
            Action
          </Dropdown.Item>
          <Dropdown.Item href="#/action-2">Another action</Dropdown.Item>
          <Dropdown.Item href="#/action-3">Something else</Dropdown.Item>
          <Dropdown.Divider /> */}
          <Dropdown.Item href="#/action-4">Logout</Dropdown.Item>
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
