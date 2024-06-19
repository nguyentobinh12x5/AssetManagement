import React, { useState } from "react";
import { useDispatch } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import ButtonIcon from "../../../components/ButtonIcon";
import { XCircle } from "react-bootstrap-icons";
import { deleteUser } from "../reducers/user-slice";

const ConfirmDisable = ({ userId }: { userId: string }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const dispatch = useDispatch();

  const handleShowModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  const handleDisableUser = () => {
    dispatch(deleteUser(userId));
    setIsModalOpen(false);
  };

  return (
    <div>
      <ButtonIcon onClick={handleShowModal}>
        <XCircle color="red" />
      </ButtonIcon>

      <ConfirmModal
        title="Are you sure?"
        isShow={isModalOpen}
        onHide={handleCloseModal}
      >
        <div className="modal-body-content">
          <p>Do you want to disable this user?</p>
          <div className="modal-buttons">
            <button className="btn btn-danger" onClick={handleDisableUser}>
              Disable
            </button>
            <button
              className="btn btn-light btn-outline-secondary"
              onClick={handleCloseModal}
            >
              Cancel
            </button>
          </div>
        </div>
      </ConfirmModal>
    </div>
  );
};

export default ConfirmDisable;
