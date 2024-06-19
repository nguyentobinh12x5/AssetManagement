import React from "react";
import {
  ToastContainer as BootstrapToastContainer,
  Toast,
} from "react-bootstrap";
import { useToast } from "./ToastContext";

const ToastContainer: React.FC = () => {
  const { toasts, removeToast } = useToast();

  return (
    <BootstrapToastContainer
      position="top-end"
      className="p-3"
      style={{ zIndex: 9999 }}
    >
      {toasts.map((toast) => (
        <Toast
          key={toast.id}
          onClose={() => removeToast(toast.id)}
          show={true}
          delay={2000}
          autohide
          bg={toast.variant}
          animation
        >
          <Toast.Body className="text-white fw-semibold">
            {toast.content}
          </Toast.Body>
        </Toast>
      ))}
    </BootstrapToastContainer>
  );
};

export default ToastContainer;
