import React, { useState } from "react";
import { useDispatch } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import ButtonIcon from "../../../components/ButtonIcon";
import { XCircle } from "react-bootstrap-icons";
import { deleteUser } from "../reducers/user-slice";

interface Props {
  userId: string | null;
  hideModal: () => void;
}

const ConfirmDisable: React.FC<Props> = ({ userId, hideModal }) => {
  const dispatch = useDispatch();

  const handleDisableUser = (e: any) => {
    e.stopPropagation();
    if (!userId) return;
    dispatch(deleteUser(userId));
    hideModal();
  };

  return (
    <div>
      <ConfirmModal title="Are you sure?" isShow={userId !== null}>
        <div className="modal-body-content">
          <p>Do you want to disable this user?</p>
          <div className="modal-buttons">
            <button className="btn btn-danger" onClick={handleDisableUser}>
              Disable
            </button>
            <button
              className="btn btn-light btn-outline-secondary"
              onClick={(e: { stopPropagation: () => void }) => {
                e.stopPropagation();
                hideModal();
              }}
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
