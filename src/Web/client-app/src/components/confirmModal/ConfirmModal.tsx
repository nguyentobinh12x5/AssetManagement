import React, { Fragment } from "react";
import Modal from "react-bootstrap/Modal";
import { XSquare } from "react-bootstrap-icons";
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
  onHide,
  children,
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
      <Modal.Header className="custom-header">
        <Modal.Title id="login-modal">
          <div className="d-flex align-items-center justify-content-between">
            {title}
            {isShowClose && <ButtonIcon onClick={onHide}>
              <XSquare color="red" />
            </ButtonIcon>}
          </div>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Fragment>{children}</Fragment>
      </Modal.Body>
    </Modal>
  );
};

export default ConfirmModal;
