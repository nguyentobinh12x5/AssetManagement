import React from "react";
import { useNavigate } from "react-router-dom";
import { setNavigate } from "../../utils/navigateUtils";

interface IProps {
  children?: React.ReactNode;
}

const NavigateContext: React.FC<IProps> = ({ children }) => {
  const navigate = useNavigate();
  setNavigate(navigate);
  return <>{children}</>;
};

export default NavigateContext;
