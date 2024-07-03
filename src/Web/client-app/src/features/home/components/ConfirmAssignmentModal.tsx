import React from "react";
import { useDispatch } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import { updateStateAssignment } from "../reducers/my-assignment-slice";
import { AssignmentState } from "../../assignment/constants/assignment-state";

interface Props {
  assignmentId: number | null;
  hideModal: () => void;
  isAcceptModal: boolean;
}

const ConfirmAssignmentModal: React.FC<Props> = ({
  assignmentId,
  hideModal,
  isAcceptModal,
}) => {
  const dispatch = useDispatch();

  const handleClick = () => {
    if (!assignmentId) return;
    if (isAcceptModal) {
      dispatch(
        updateStateAssignment({
          id: assignmentId,
          state: AssignmentState.Accepted,
        })
      );
    } else {
      dispatch(
        updateStateAssignment({
          id: assignmentId,
          state: AssignmentState.Declined,
        })
      );
    }
    hideModal();
  };

  return (
    <div>
      <ConfirmModal title="Are you sure?" isShow={assignmentId !== null}>
        <div className="modal-body-content">
          <p>
            {isAcceptModal
              ? "Do you want to accept this assignment?"
              : "Do you want to decline this assignment?"}
          </p>
          <div className="modal-buttons">
            <button className="btn btn-danger" onClick={handleClick}>
              {isAcceptModal ? "Accept" : "Decline"}
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

export default ConfirmAssignmentModal;
