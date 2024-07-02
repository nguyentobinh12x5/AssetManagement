import React, { useEffect } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppDispatch, useAppState } from "../../../redux/redux-hooks";
import { getAssetStatuses } from "../reducers/asset-slice";
import useAssetList from "../list/useAssetList";

interface FilterByStatusProps {}

const FilterByStatus: React.FC<FilterByStatusProps> = () => {
  const {
    statuses,
    assetQuery: { assetStatus },
  } = useAppState((state) => state.assets);
  const { handleFilterByStatus } = useAssetList();
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
    if (displayStatuses.includes("All")) {
      // If "All" is selected and it's the only selection, set actualStatuses to ["All"]
      if (displayStatuses.length === 1) {
        handleFilterByStatus(["All"]);
      } else {
        // Check the position of "All" in the array
        const allIndex = displayStatuses.indexOf("All");

        if (allIndex === 0) {
          // If "All" is at the beginning, remove "All"
          displayStatuses = displayStatuses.slice(1);
        } else if (allIndex === displayStatuses.length - 1) {
          // If "All" is at the end, keep only "All"
          displayStatuses = ["All"];
          handleFilterByStatus(displayStatuses);
        }
      }
    }

    const actualStatuses: AssetStatus[] = displayStatuses.map(
      (displayStatus) =>
        (Object.keys(assetStatusesMap) as AssetStatus[]).find(
          (key) => assetStatusesMap[key] === displayStatus
        )!
    );

    handleFilterByStatus(actualStatuses);
  };

  return (
    <DropdownFilter
      placeholder="State"
      label="State"
      options={displayAssetStatuses}
      selectedOptions={assetStatus.map(
        (status) => assetStatusesMap[status as AssetStatus]
      )}
      handleOptionChange={handleStatusChange}
    />
  );
};

export default FilterByStatus;
