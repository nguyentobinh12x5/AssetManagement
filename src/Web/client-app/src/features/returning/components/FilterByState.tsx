import React, { useMemo } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppState } from "../../../redux/redux-hooks";
import { ReturningState } from "../constants/returning-state";
import useReturningList from "../list/useReturningList";

interface FilterByStateProps {}
const StateKeys = Object.keys(ReturningState)
  .filter((_) => isNaN(Number(_)))
  .filter((_) => _ !== "Declined");

const FilterByState: React.FC<FilterByStateProps> = () => {
  const {
    states,
    returningQuery: { state: returningState },
  } = useAppState((state) => state.returnings);
  const { handleFilterByState } = useReturningList();
  const selectedOptions = useMemo(
    () =>
      returningState.map((state) => {
        if (isNaN(Number(state)) || !state) {
          return state;
        }

        return ReturningState[state as keyof typeof ReturningState].toString();
      }),
    [returningState]
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
          return ReturningState[
            displayStatus as keyof typeof ReturningState
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
