import React, { InputHTMLAttributes } from "react";
import { useField } from "formik";
import DatePicker from "react-datepicker";
import { CalendarDateFill } from "react-bootstrap-icons";

import "react-datepicker/dist/react-datepicker.css";

type inputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  placeholder?: string;
  name: string;
  required?: boolean;
  novalidation?: boolean;
};

const DateField: React.FC<inputFieldProps> = (props) => {
  const [{ value }, { error }, { setValue}] =
    useField(props);
  const { label, required } = props;

  const handleDateChange = (date: Date) => {
    if (!date) {
      setValue(undefined);
    } else {
      const formattedDate = date.toISOString();
      setValue(formattedDate);
    }
  };

  return (
    <>
      <div className="form-group row ">
        <label className="col-form-label col-4 d-flex">
          {label}
          {required && <div className="invalid ml-1">*</div>}
        </label>
        <div className="col">
                  <div className="form-control pt-1 pb-1 d-flex justify-content-between align-items-center">
            <DatePicker
              dateFormat="dd/MM/yyyy"
              selected={value}
              onChange={handleDateChange}
            />
            <div className="date-icon p-1 pointer">
              <CalendarDateFill />
            </div>
          </div>
          { error && <div className="invalid">{error}</div>}
        </div>
      </div>
    </>
  );
};

export default DateField;
