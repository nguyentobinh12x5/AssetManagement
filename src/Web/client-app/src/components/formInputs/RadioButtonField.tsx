import React, { InputHTMLAttributes } from "react";
import { useField } from "formik";
import "./RadioButtonField.scss";
import ISelectOption from "./interfaces/ISelectOption";

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  name: string;
  required?: boolean;
  options: ISelectOption[];
  checked?: boolean;
};

const RadioButtonField: React.FC<InputFieldProps> = (props) => {
  const [field, { value }, { setValue }] = useField(props);
  const { label, name, required, options, checked } = props;

  const handleChange = (e: any) => {
    setValue(e.target.value);
  };

  const display = () => {
    if (checked) return "form-check-inline";
    return "";
  };

  return (
    <div className="form-group row">
      <label className="d-flex col-4 col-form-label">
        {label}
        {required && <div className="invalid ml-1">*</div>}
      </label>
      <div className="radio-btn-container col pt-1 pb-1">
        {options.map(({ id, label, value: optionValue }) => (
          <div className={`form-check ${display()}`} key={id}>
            <input
              {...field}
              className={`form-check-input input-radio`}
              id={id.toString()}
              type="radio"
              name={name}
              value={optionValue}
              checked={value === optionValue}
              onChange={handleChange}
            ></input>
            <label className={`form-check-label ml-1`} htmlFor={id.toString()}>
              {label}
            </label>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RadioButtonField;
