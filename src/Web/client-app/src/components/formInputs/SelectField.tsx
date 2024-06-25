import React, { InputHTMLAttributes, useEffect, useRef, useState } from "react";
import { useField } from "formik";

interface Option {
  value: string;
  label: string;
}

type SelectFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  name: string;
  options: Option[];
  required?: boolean;
  noValidation?: boolean;
  apiError?: string;
};

const SelectField: React.FC<SelectFieldProps> = (props) => {
  const [field, { error, touched }, helpers] = useField(props);
  const { label, required, noValidation, options, apiError } = props;

  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState<Option | null>(
    options.length > 0 ? options[0] : null
  );
  const selectRef = useRef<HTMLDivElement>(null);

  const validateClass = () => {
    if (touched && (error || error === "" || apiError)) return "is-invalid";
    if (noValidation) return "";
    if (touched) return "is-valid";
    return "";
  };

  const handleToggle = () => setIsOpen(!isOpen);

  const handleOptionClick = (option: Option) => {
    setSelectedOption(option);
    helpers.setValue(option.value);
    setIsOpen(false);
  };

  const handleClickOutside = (event: any) => {
    if (selectRef.current && !selectRef.current.contains(event.target)) {
      setIsOpen(false);
    }
  };

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return (
    <div className="form-group row">
      <label htmlFor={props.name} className="col-form-label col-4 d-flex">
        {label}
        {required && <div className="invalid ml-1">*</div>}
      </label>
      <div className="col" ref={selectRef}>
        <div className="form-select-wrapper">
          <div className={`form-select-header`} onClick={handleToggle}>
            <input
              className={`form-control ${validateClass()}`}
              {...field}
              {...props}
              value={selectedOption?.value ?? ""}
              readOnly
            />
            <span className="form-select-arrow">
              {isOpen ? "\u25B2" : "\u25BC"}
            </span>
          </div>
          {isOpen && (
            <ul className="form-select-options">
              {options.map((option) => (
                <li
                  key={option.value}
                  className="form-select-option"
                  onClick={() => handleOptionClick(option)}
                >
                  {option.label}
                </li>
              ))}
            </ul>
          )}
        </div>

        {/* <select className={`d-none ${validateClass()}`} {...field} {...props}>
          {options.map((option) => (
            <option
              key={option.value}
              value={option.value}
              className="form-select-option"
            >
              {option.label}
            </option>
          ))}
        </select> */}
        {touched && (apiError || error) && (
          <div className="invalid position-relative mt-2">
            {apiError ?? error}
          </div>
        )}
      </div>
    </div>
  );
};

export default SelectField;
