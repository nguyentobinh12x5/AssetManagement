import React from "react";
import { useDispatch } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import { updateStateAssignment, returningAssignment } from "../reducers/my-assignment-slice";
import { AssignmentState } from "../../assignment/constants/assignment-state";

interface Props {
  assignmentId: number | null;
  hideModal: () => void;
  typeModal: number;
}

const ConfirmAssignmentModal: React.FC<Props> = ({
  assignmentId,
  hideModal,
  typeModal,
}) => {
  const dispatch = useDispatch();

  const handleClick = () => {
    if (!assignmentId) return;
    if (typeModal === AssignmentState.Accepted) {
      dispatch(
        updateStateAssignment({
          id: assignmentId,
          state: AssignmentState.Accepted,
        })
      );
    } else if (typeModal === AssignmentState.Declined) {
      dispatch(
        updateStateAssignment({
          id: assignmentId,
          state: AssignmentState.Declined,
        })
      );
    } else if (typeModal ===  AssignmentState["Waiting for returning"]) {
      dispatch(
        returningAssignment(assignmentId)
      );
    }
    hideModal();
  };

  return (
    <div>
      <ConfirmModal title="Are you sure?" isShow={assignmentId !== null}>
        <div className="modal-body-content">
        <p>
          {typeModal === AssignmentState.Accepted
            ? "Do you want to accept this assignment?"
            : typeModal === AssignmentState.Declined
            ? "Do you want to decline this assignment?"
            : "Do you want to create a returning request for this asset?"
          }
        </p>
          <div className="modal-buttons">
            <button className="btn btn-danger" onClick={handleClick}>
              {typeModal === AssignmentState.Accepted
                ? "Accept"
                : typeModal === AssignmentState.Declined
                ? "Decline"
                : "Yes"
              }
            </button>
            <button
              className="btn btn-light btn-outline-secondary"
              onClick={(e: { stopPropagation: () => void }) => {
                e.stopPropagation();
                hideModal();
              }}
            >
              { typeModal ===  AssignmentState["Waiting for returning"] ? "No" : "Cancel" }
            </button>
          </div>
        </div>
      </ConfirmModal>
    </div>
  );
};

export default ConfirmAssignmentModal;
