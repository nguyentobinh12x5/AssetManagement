import React from "react";
import { useDispatch } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import { deleteAssginments } from "../reducers/assignment-slice";

interface Props {
  Id: number | null;
  hideModal: () => void;
}

const ConfirmDelete: React.FC<Props> = ({ Id, hideModal }) => {
  const dispatch = useDispatch();

  const handleDelete = (e: any) => {
    e.stopPropagation();
    if (!Id) return;
    dispatch(deleteAssginments(Id));
    hideModal();
  };

  return (
    <div>
      <ConfirmModal title="Are you sure?" isShow={Id !== null}>
        <div className="modal-body-content">
          <p>Do you want to delete this assignment?</p>
          <div className="modal-buttons">
            <button className="btn btn-danger" onClick={handleDelete}>
              Delete
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

export default ConfirmDelete;
