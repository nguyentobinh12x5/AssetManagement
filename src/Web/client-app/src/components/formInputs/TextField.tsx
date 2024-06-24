import React, { InputHTMLAttributes } from "react";
import { ErrorMessage, useField } from "formik";

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  placeholder?: string;
  name: string;
  required?: boolean;
  noValidation?: boolean;
};

const TextField: React.FC<InputFieldProps> = (props) => {
  const [field, { error, touched }] = useField(props);
  const { label, required, noValidation } = props;

  const validateClass = () => {
    if (touched && error) return "is-invalid";
    if (noValidation) return "";
    if (touched) return "is-valid";
    return "";
  };

  return (
    <div className="form-group row">
      <label htmlFor={props.name} className="col-form-label col-4 d-flex">
        {label}
        {required && <div className="invalid ml-1">*</div>}
      </label>
      <div className="col">
        <input
          className={`form-control ${validateClass()}`}
          {...field}
          {...props}
        />
        <ErrorMessage
          name={props.name}
          component={"div"}
          className="invalid mt-2"
        />
        {/* {error && touched && <div className="invalid">{error}</div>} */}
      </div>
    </div>
  );
};

export default TextField;
