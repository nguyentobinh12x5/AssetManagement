import React, { useMemo } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppState } from "../../../redux/redux-hooks";
import useAssignmentList from "../list/useAssignmentList";
import {
  AssignmentState,
  AssignmentStateKey,
} from "../constants/assignment-state";

interface FilterByStateProps {}
const StateKeys = Object.keys(AssignmentState)
  .filter((_) => isNaN(Number(_)))
  .filter((_) => _ !== "Declined");

const FilterByState: React.FC<FilterByStateProps> = () => {
  const {
    states,
    assignmentQuery: { state: assignmentState },
  } = useAppState((state) => state.assignments);
  const { handleFilterByState } = useAssignmentList();
  const selectedOptions = useMemo(
    () =>
      assignmentState.map((state) => {
        if (isNaN(Number(state)) || !state) {
          return state;
        }

        return AssignmentState[
          state as keyof typeof AssignmentState
        ].toString();
      }),
    [assignmentState]
  );

  // Define the type for asset statuses
  type AssignmentState = (typeof states)[number];

  // Get the list of display values from the map
  const displayAssetStatuses: string[] = ["All", ...StateKeys];

  const handleStateChange = (displayStatuses: string[]) => {
    if (
      displayStatuses.includes("All") &&
      (displayStatuses.length === 1 ||
        displayStatuses[displayStatuses.length - 1] === "All")
    ) {
      handleFilterByState(["All"]);
    } else {
      const actualStatuses: AssignmentState[] = displayStatuses
        .map((displayStatus) => {
          if (isNaN(Number(displayStatus))) {
            return displayStatus;
          }
          return AssignmentState[
            displayStatus as keyof typeof AssignmentState
          ].toString();
        })
        .filter((status) => status !== "All");
      handleFilterByState(actualStatuses);
    }
  };

  return (
    <DropdownFilter
      placeholder="State"
      label="State"
      options={displayAssetStatuses}
      selectedOptions={selectedOptions}
      handleOptionChange={handleStateChange}
    />
  );
};

export default FilterByState;
