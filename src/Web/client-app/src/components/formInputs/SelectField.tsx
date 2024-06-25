import React, { SelectHTMLAttributes } from "react";
import { useField } from "formik";

interface Option {
  value: string;
  label: string;
}

type SelectFieldProps = SelectHTMLAttributes<HTMLSelectElement> & {
  label: string;
  name: string;
  options: Option[];
  required?: boolean;
  noValidation?: boolean;
};

const SelectField: React.FC<SelectFieldProps> = (props) => {
  const [field, { error, touched }] = useField(props);
  const { label, required, noValidation, options } = props;

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
        <select
          className={`form-control ${validateClass()}`}
          {...field}
          {...props}
        >
          {options.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        {error && touched && <div className="invalid">{error}</div>}
      </div>
    </div>
  );
};

export default SelectField;
