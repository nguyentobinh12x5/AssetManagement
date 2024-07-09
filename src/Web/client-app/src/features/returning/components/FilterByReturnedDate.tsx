import React, { useState } from "react";
import DatePicker from "react-datepicker";
import { CalendarDateFill } from "react-bootstrap-icons";
import { useAppState } from "../../../redux/redux-hooks";
import { utcToDateString } from "../../../utils/dateUtils";
import useReturningList from "../list/useReturningList";

interface FilterByReturnedDateProps { }

const FilterByReturnedDate: React.FC<FilterByReturnedDateProps> = () => {
  const {
    returningQuery: { returnedDate },
  } = useAppState((state) => state.returnings);
  const { handleFilterByReturnedDate } = useReturningList();

  const [selectedDate, setSelectedDate] = useState<Date | null>(
    returnedDate ? new Date(returnedDate) : null
  );

  const handleDateChange = (date: Date | null) => {
    setSelectedDate(date);
    const formattedDate = date ? utcToDateString(date.toString()) : "";
    handleFilterByReturnedDate(formattedDate);
  };

  return (
    <div className="assignment-datepicker-wrapper form-control">
      <DatePicker
        selected={selectedDate}
        onChange={handleDateChange}
        dateFormat="dd/MM/yyyy"
        placeholderText="Returned Date"
        showYearDropdown
        showMonthDropdown
        dropdownMode="select"
        id="filter-date-assignment-list"
        name="filter-date-assignment-list"
      />
      <div className="assignment-datepicker-label">
        <label
          htmlFor="filter-date-assignment-list"
          className="date-icon p-1 pointer border-left"
        >
          <CalendarDateFill />
        </label>
      </div>
    </div>
  );
};

export default FilterByReturnedDate;
