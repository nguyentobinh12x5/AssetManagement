import React from "react";
import MyAssignmentsTable from "./MyAssignmentsTable";
import useMyAssignments from "./useMyAssignments";

const MyAssignments = () => {
  const { assignments, hasSortColumn, handleSort, handlePaging } =
    useMyAssignments();

  return (
    <div className="asset-list offset-1">
      <p className="table-title">My Assignment</p>

      {/* <MyAssignmentsTableHeader /> */}

      <MyAssignmentsTable
        assignments={assignments}
        sortState={{
          name: hasSortColumn.sortColumn,
          orderBy: hasSortColumn.sortOrder,
        }}
        handleSort={handleSort}
        handlePaging={handlePaging}
      />
    </div>
  );
};

export default MyAssignments;
