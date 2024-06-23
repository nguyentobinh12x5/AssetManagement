import React, { InputHTMLAttributes, useState } from "react";
import { useField, useFormikContext } from "formik";
import { EyeFill, EyeSlashFill } from "react-bootstrap-icons";

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  placeholder?: string;
  name: string;
  isrequired?: boolean;
  notvalidate?: boolean;
  apiError?: string;
};

const TextFieldPassword: React.FC<InputFieldProps> = (props) => {
  const [field, { error, touched }] = useField(props);
  const { values, isSubmitting } = useFormikContext();
  const { label, isrequired, notvalidate, apiError } = props;

  const validateClass = () => {
    if ((touched && error) || apiError) return "is-invalid";
    if (notvalidate) return "";
    if (touched) return "is-valid";
    return "";
  };

  const [showPass, setShowPass] = useState(true);
  const clickHandler = () => {
    setShowPass(!showPass);
  };

  return (
    <div className="form-group mb-3 row">
      <label className="col-4 col-form-label d-flex text-nowrap">
        {label}
        {isrequired && <div className="invalid ml-1">*</div>}
      </label>
      <div className="col">
        <div className="col d-flex position-relative p-0">
          <input
            type={showPass ? "password" : "text"}
            className={`form-control ps-2 ${validateClass()}`}
            {...field}
            {...props}
          />
          <div
            className="position-absolute top-50 end-0 translate-middle-y me-2 icon-eye"
            style={{ backgroundColor: "white" }}
            onClick={clickHandler}
          >
            {showPass ? (
              <EyeFill className="text-black" style={{ width: "20px" }} />
            ) : (
              <EyeSlashFill className="text-black" style={{ width: "20px" }} />
            )}
          </div>
        </div>
        {(error && isSubmitting) || apiError ? (
          <div className="invalid position-relative mt-2">
            {error || apiError}
          </div>
        ) : null}
      </div>
    </div>
  );
};

export default TextFieldPassword;
