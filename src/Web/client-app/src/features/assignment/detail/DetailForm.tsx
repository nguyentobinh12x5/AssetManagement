import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import { RootState } from "../../../redux/store";
import { Col, Row } from "react-bootstrap";
import { formatDate } from "../../../utils/dateUtils";
import {
  getAssignmentById,
  resetState,
} from "../reducers/assignment-detail-slice";
import "./AssignmentDetailForm.scss";
import { AssignmentState } from "../constants/assignment-state";
import convertnewlinesUtils from "../../../utils/convertnewlinesUtils";

type AssignmentID = {
  id: number;
  onClose: () => void;
};
const stateLabels: Record<number, string> = {
  0: "Waiting For Acceptance",
  1: "In Progress",
  2: "Completed",
  3: "Rejected",
};

const DetailForm: React.FC<AssignmentID> = ({ id, onClose }) => {
  const [isModalOpen, setIsModalOpen] = useState(true);
  const dispatch = useDispatch();
  const { AssignmentDetail } = useSelector(
    (state: RootState) => state.assignmentDetail
  );

  useEffect(() => {
    if (isModalOpen) {
      dispatch(getAssignmentById(id));
    }

    return () => {
      dispatch(resetState());
    };
  }, [isModalOpen, dispatch, id]);

  const handleCloseModal = () => {
    setIsModalOpen(false);
    onClose();
  };

  useEffect(() => {
    return () => {
      dispatch(resetState());
    };
  }, [dispatch]);

  return (
    <div className="container m-auto p-5">
      <ConfirmModal
        title="Detailed Assignment Information"
        isShow={isModalOpen}
        onHide={handleCloseModal}
        dialogClassName="modal-100w modal-detail-assignment"
        isShowClose={true}
      >
        <div className="form-detail">
          <div>
            <Row className="mb-3">
              <Col md={3}>Asset Code</Col>
              <Col md={9}>{AssignmentDetail?.assetCode}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Asset Name</Col>
              <Col md={9}>{AssignmentDetail?.assetName}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Specification</Col>
              <Col md={9} className="multi-line-text">
                <div
                  dangerouslySetInnerHTML={{
                    __html: convertnewlinesUtils(
                      AssignmentDetail?.specification || ""
                    ),
                  }}
                />
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Assigned to</Col>
              <Col md={9}>{AssignmentDetail?.assignedTo}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Assigned by</Col>
              <Col md={9}>{AssignmentDetail?.assignedBy}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Assigned Date</Col>
              <Col md={9}>
                {formatDate(
                  AssignmentDetail
                    ? AssignmentDetail.assignedDate
                    : new Date().toDateString()
                )}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>State</Col>
              <Col md={9}>
                {AssignmentDetail
                  ? AssignmentState[AssignmentDetail.state]
                  : ""}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Note</Col>
              <Col md={9} className="multi-line-text">
                <div
                  dangerouslySetInnerHTML={{
                    __html: convertnewlinesUtils(AssignmentDetail?.note || ""),
                  }}
                />
              </Col>
            </Row>
          </div>
        </div>
      </ConfirmModal>
    </div>
  );
};

export default DetailForm;
