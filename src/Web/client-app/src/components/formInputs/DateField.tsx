import React, { InputHTMLAttributes } from "react";
import { useField, useFormikContext } from "formik";
import DatePicker from "react-datepicker";
import { CalendarDateFill } from "react-bootstrap-icons";

import "react-datepicker/dist/react-datepicker.css";

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  placeholder?: string;
  name: string;
  required?: boolean;
  novalidation?: boolean;
  apiError?: string;
  maxDate?: Date | null | undefined;
};

const DateField: React.FC<InputFieldProps> = (props) => {
  const [{ value }, { error, touched }, { setValue, setError }] =
    useField(props);

  const { label, required, apiError, maxDate } = props;

  const { setFieldTouched } = useFormikContext();

  const handleDateChange = (date: Date) => {
    setFieldTouched(props.name, true);
    if (!date) {
      setError("");
      setValue(undefined);
    } else {
      setValue(date);
    }
  };

  const validateClass = () => {
    if (touched && (error || apiError)) return "is-invalid";
    if (props.novalidation) return "";
    if (touched) return "is-valid";
    return "";
  };

  return (
    <>
      <div className="form-group row">
        <label htmlFor={props.name} className="col-4 d-flex col-form-label">
          {label}
          {required && <div className="invalid ml-1">*</div>}
        </label>
        <div className="col">
          <div
            className={`form-control pt-1 pb-1 d-flex justify-content-between align-items-center ${validateClass()}`}
          >
            <DatePicker
              id={props.id}
              name={props.name}
              dateFormat="dd/MM/yyyy"
              selected={value}
              onChange={handleDateChange}
              showYearDropdown
              showMonthDropdown
              dropdownMode="select"
              maxDate={maxDate}
            />
            <label htmlFor={props.name} className="date-icon p-1 pointer">
              <CalendarDateFill />
            </label>
          </div>
          {touched && (apiError || error) && (
            <div className="invalid position-relative mt-2">
              {apiError ? apiError : error}
            </div>
          )}
        </div>
      </div>
    </>
  );
};

export default DateField;
