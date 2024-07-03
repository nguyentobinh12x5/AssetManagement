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
  defaultValue?: Option;
};

const SelectField: React.FC<SelectFieldProps> = (props) => {
  const [field, { error, touched }, helpers] = useField(props);
  const { label, required, noValidation, options, apiError, defaultValue } =
    props;

  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState<Option | undefined>(
    defaultValue
  );
  const selectRef = useRef<HTMLDivElement>(null);

  const validateClass = () => {
    if (touched && (error || apiError)) return "is-invalid";
    if (noValidation) return "";
    if (touched) return "is-valid";
    return "";
  };

  const handleToggle = () => {
    if (props.disabled) return;
    setIsOpen(!isOpen);
  };

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

  useEffect(() => {
    const selected = options.find((option) => option.value === field.value);
    setSelectedOption(selected);
  }, [field.value, options]);

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
              value={selectedOption?.label ?? ""}
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

        {touched && (apiError || error) && (
          <div className="invalid position-relative mt-2">
            {apiError ? apiError : error}
          </div>
        )}
      </div>
    </div>
  );
};

export default SelectField;
