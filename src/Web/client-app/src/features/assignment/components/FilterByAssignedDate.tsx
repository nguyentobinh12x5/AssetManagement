import React, { useState } from "react";
import DatePicker from "react-datepicker";
import { CalendarDateFill } from "react-bootstrap-icons";
import { useAppState } from "../../../redux/redux-hooks";
import useAssignmentList from "../list/useAssignmentList";
import { utcToDateString } from "../../../utils/dateUtils";

interface FilterByAssignedDateProps {}

const FilterByAssignedDate: React.FC<FilterByAssignedDateProps> = () => {
  const {
    assignmentQuery: { assignedDate },
  } = useAppState((state) => state.assignments);
  const { handleFilterByAssignedDate } = useAssignmentList();

  const [selectedDate, setSelectedDate] = useState<Date | null>(
    assignedDate ? new Date(assignedDate) : null
  );

  const handleDateChange = (date: Date | null) => {
    setSelectedDate(date);
    const formattedDate = date ? utcToDateString(date.toString()) : "";
    handleFilterByAssignedDate(formattedDate);
  };

  return (
    <div className="assignment-datepicker-wrapper form-control">
      <DatePicker
        selected={selectedDate}
        onChange={handleDateChange}
        dateFormat="dd/MM/yyyy"
        placeholderText="Assigned Date"
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

export default FilterByAssignedDate;
