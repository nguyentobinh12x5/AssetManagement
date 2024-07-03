import React, { Fragment } from "react";
import Modal from "react-bootstrap/Modal";
import { XSquare, XSquareFill } from "react-bootstrap-icons";
import ButtonIcon from "../ButtonIcon";

interface Props {
  title: string;
  isShow: boolean;
  onHide?: () => void;
  children: React.ReactNode;
  dialogClassName?: string;
  isShowClose?: boolean;
}

const ConfirmModal: React.FC<Props> = ({
  title,
  isShow,
  children,
  onHide,
  dialogClassName = "",
  isShowClose,
}) => {
  return (
    <Modal
      show={isShow}
      dialogClassName={`${dialogClassName}`}
      aria-labelledby="login-modal"
      centered
    >
      <Modal.Header className="modal-title">
        <Modal.Title id="login-modal">
          <div className="d-flex align-items-center justify-content-between fw-bold">
            {title}
            {isShowClose && (
              <ButtonIcon onClick={onHide}>
                <XSquare color="rgba(207, 35, 56, 1)" />
              </ButtonIcon>
            )}
          </div>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body className="modal-body">
        <Fragment>{children}</Fragment>
      </Modal.Body>
    </Modal>
  );
};

export default ConfirmModal;
