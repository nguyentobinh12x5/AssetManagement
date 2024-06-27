import React from "react";
import { Col, Modal, Row } from "react-bootstrap";
import "./Detail.scss";
import useDetailUser from "./useDetailModal";
import Loading from "../../../components/Loading";
import { formatDate } from "../../../utils/dateUtils";
import ButtonIcon from "../../../components/ButtonIcon";
import { XSquare } from "react-bootstrap-icons";

interface PopupComponentProps {
  show: boolean;
  handleClose: () => void;
  userId: string;
}

const PopupComponent: React.FC<PopupComponentProps> = ({
  show,
  handleClose,
  userId,
}) => {
  const { user } = useDetailUser(userId);
  console.log(user);
  if (!user) {
    return (
      <Modal show={show} onHide={handleClose}>
        <Loading />
      </Modal>
    );
  }
  return (
    <Modal show={show} aria-labelledby="login-modal" centered>
      <Modal.Header className="user-info-header">
        <Modal.Title id="login-modal">
          <div className="d-flex align-items-center justify-content-around custom-modal fw-bold">
            Detailed User Information
            <ButtonIcon onClick={handleClose}>
              <XSquare color="rgba(207, 35, 56, 1)" />
            </ButtonIcon>
          </div>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body className="user-info-body">
        <div className="detail-user">
          <Row className="mb-3">
            <Col md={4}>Staff Code</Col>
            <Col md={8}>{user.staffCode}</Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Full Name</Col>
            <Col md={8}>
              {user.firstName} {user.lastName}
            </Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Username</Col>
            <Col md={8}>{user.username}</Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Date of Birth</Col>
            <Col md={8}>{formatDate(user.dateOfBirth)}</Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Gender</Col>
            <Col md={8}>{user.gender}</Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Joined Date</Col>
            <Col md={8}>{formatDate(user.joinDate)}</Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Type</Col>
            <Col md={8}>{user.type}</Col>
          </Row>
          <Row className="mb-3">
            <Col md={4}>Location</Col>
            <Col md={8}>{user.location}</Col>
          </Row>
        </div>
      </Modal.Body>
    </Modal>
  );
};

export default PopupComponent;
