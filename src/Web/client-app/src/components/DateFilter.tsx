import React from "react";
import DatePicker from "react-datepicker";
import { CalendarDateFill } from "react-bootstrap-icons";

import "react-datepicker/dist/react-datepicker.css";

interface Props {
  label: string;
  date: Date;
  handleDateChange: Function;
}

const DateFilter: React.FC<Props> = ({ label, date, handleDateChange }) => {
  return (
    <div className="date-filter">
      <div className="col d-flex" style={{ width: "200px" }}>
        <div className="d-flex align-items-center w-100 position-relative">
          <DatePicker
            selected={date}
            onChange={(date) => handleDateChange(date)} //only when value has changed
            placeholderText={label}
          />
        </div>
        <div className="date-icon p-1 pointer">
          <CalendarDateFill />
        </div>
      </div>
    </div>
  );
};

export default DateFilter;
