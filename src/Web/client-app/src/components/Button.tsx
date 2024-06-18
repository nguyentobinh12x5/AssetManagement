import React, { DetailedHTMLProps } from "react";
import { Button as BootstrapButton, ButtonProps } from "react-bootstrap";
type Props = ButtonProps & {
  children: React.ReactNode;
};

const Button: React.FC<Props> = (props) => {
  return (
    <BootstrapButton className={`primary-btn ${props.className}`} {...props}>
      {props.children}
    </BootstrapButton>
  );
};

export default Button;
