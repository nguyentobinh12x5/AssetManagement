import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import {
  deleteAssets,
  checkHistoricalAssignment,
  resetHistoricalAssignment,
} from "../reducers/asset-slice";
import { RootState } from "../../../redux/store";

interface Props {
  Id: number;
  hideModal: () => void;
  typePopup: "edit" | "delete";
}

const ConfirmDelete: React.FC<Props> = ({ Id, hideModal, typePopup }) => {
  const dispatch = useDispatch();

  const { isBelongToHistoricalAssignment, isLoading } = useSelector(
    (state: RootState) => state.assets
  );

  const handleDelete = (e: any) => {
    e.stopPropagation();
    dispatch(deleteAssets(Id));
    dispatch(resetHistoricalAssignment());
    hideModal();
  };

  const handleCancel = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation();
    hideModal();
  };

  useEffect(() => {
    dispatch(
      checkHistoricalAssignment({
        id: Id,
        typePopup,
      })
    );
  }, [Id, dispatch, typePopup]);

  return (
    <div>
      <ConfirmModal
        title="Are you sure?"
        isShow={
          Id !== null &&
          !isLoading &&
          !(!isBelongToHistoricalAssignment && typePopup === "edit")
        }
      >
        <div className="modal-body-content">
          <p>
            {isBelongToHistoricalAssignment ? (
              <>
                Cannot {typePopup} the asset because it belongs to one or more
                historical assignments.
              </>
            ) : (
              "Do you want to delete this asset?"
            )}
          </p>
          <div className="modal-buttons">
            {!isBelongToHistoricalAssignment && (
              <button className="btn btn-danger" onClick={handleDelete}>
                Delete
              </button>
            )}
            <button
              className="btn btn-light btn-outline-secondary"
              onClick={handleCancel}
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
