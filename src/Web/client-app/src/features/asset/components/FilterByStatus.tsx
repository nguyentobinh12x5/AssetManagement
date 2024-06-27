import React, { useEffect, useState } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppDispatch, useAppState } from "../../../redux/redux-hooks";
import { getAssetStatuses } from "../reducers/asset-slice";

interface FilterByStatusProps {
  handleFilterByStatus: (statuses: string[]) => void;
}

const FilterByStatus: React.FC<FilterByStatusProps> = ({
  handleFilterByStatus,
}) => {
  const [selectedStatuses, setSelectedStatuses] = useState<string[]>([
    "Assigned",
    "Available",
    "Not available",
  ]);
  const { statuses } = useAppState((state) => state.assets);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (statuses.length <= 1) dispatch(getAssetStatuses());
  }, [statuses, dispatch]);

  // Define the type for asset statuses
  type AssetStatus = (typeof statuses)[number];

  // Mapping of actual values to display values
  const assetStatusesMap: Record<AssetStatus, string> = statuses.reduce(
    (acc, status) => {
      acc[status as AssetStatus] = status;
      return acc;
    },
    {} as Record<AssetStatus, string>
  );

  // Get the list of display values from the map
  const displayAssetStatuses = Object.values(assetStatusesMap);

  const handleStatusChange = (displayStatuses: string[]) => {
    if (displayStatuses.length === 0) {
      setSelectedStatuses(["All"]);
      handleFilterByStatus([]);
    } else {
      // Convert display statuses to actual statuses
      const actualStatuses: AssetStatus[] = displayStatuses
        .map(
          (displayStatus) =>
            (Object.keys(assetStatusesMap) as AssetStatus[]).find(
              (key) => assetStatusesMap[key] === displayStatus
            )!
        )
        .filter((status) => status !== "All");

      setSelectedStatuses(actualStatuses);
      handleFilterByStatus(actualStatuses);
    }
  };

  return (
    <DropdownFilter
      placeholder="State"
      label="State"
      options={displayAssetStatuses}
      selectedOptions={selectedStatuses.map(
        (status) => assetStatusesMap[status as AssetStatus]
      )}
      handleOptionChange={handleStatusChange}
    />
  );
};

export default FilterByStatus;
