import React, { useState } from "react";
import { Dropdown } from "react-bootstrap";
import ChangePasswordForm from "../auth/changepassword/ChangePasswordForm";

const Header: React.FC = () => {
  const [showChangePasswordModal, setShowChangePasswordModal] = useState(false);

  const handleShowChangePasswordModal = () => setShowChangePasswordModal(true);
  const handleCloseChangePasswordModal = () =>
    setShowChangePasswordModal(false);

  return (
    <div className="header align-items-center font-weight-bold">
      <div className="container-lg-min container-fluid d-flex justify-content-between">
        <p className="headText">Home</p>
        <Dropdown>
          <Dropdown.Toggle variant="dark" id="dropdown-basic">
            Account
          </Dropdown.Toggle>

          <Dropdown.Menu>
            <Dropdown.Item onClick={handleShowChangePasswordModal}>
              Change Password
            </Dropdown.Item>
          </Dropdown.Menu>
        </Dropdown>
      </div>

      <ChangePasswordForm
        show={showChangePasswordModal}
        onHide={handleCloseChangePasswordModal}
      />
    </div>
  );
};

export default Header;
