import React, { Fragment } from "react";
import Modal from "react-bootstrap/Modal";

interface Props {
  title: string;
  isShow: boolean;
  onHide?: () => void;
  children: React.ReactNode;
  dialogClassName?: string;
}

const ConfirmModal: React.FC<Props> = ({
  title,
  isShow,
  children,
  dialogClassName = "",
}) => {
  return (
    <Modal
      show={isShow}
      dialogClassName={`modal-90w ${dialogClassName}`}
      aria-labelledby="custom-confirm-modal"
      centered
    >
      <Modal.Header className="custom-header">
        <Modal.Title id="custom-confirm-modal">{title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Fragment>{children}</Fragment>
      </Modal.Body>
    </Modal>
  );
};

export default ConfirmModal;
