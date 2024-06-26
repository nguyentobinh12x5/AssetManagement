import React, { TextareaHTMLAttributes } from "react";
import { useField } from "formik";

type InputFieldProps = TextareaHTMLAttributes<HTMLTextAreaElement> & {
  label: string;
  placeholder?: string;
  name: string;
  required?: boolean;
  noValidation?: boolean;
  apiError?: string;
};

const TextAreaField: React.FC<InputFieldProps> = (props) => {
  const [field, { error, touched }] = useField(props);
  const { label, required, noValidation, apiError } = props;

  const validateClass = () => {
    if (touched && (error || error === "" || apiError)) return "is-invalid";
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
        <textarea
          className={`form-control ${validateClass()}`}
          {...field}
          {...props}
        />

        {touched && (apiError || error) && (
          <div className="invalid position-relative mt-2">
            {apiError ? apiError : error}
          </div>
        )}
      </div>
    </div>
  );
};

export default TextAreaField;
