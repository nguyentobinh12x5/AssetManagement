import React, { useState } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";

interface FilterByRoleProps {
  handleFilterByType: (types: string[]) => void;
}

const FilterByRole: React.FC<FilterByRoleProps> = ({ handleFilterByType }) => {
  const [selectedTypes, setSelectedTypes] = useState<string[]>([]);

  // Define the type for user types
  type UserType = "Staff" | "Administrator" | "All";

  // Mapping of actual values to display values
  const userTypesMap: Record<UserType, string> = {
    All: "All",
    Administrator: "Admin",
    Staff: "Staff",
  };

  // Get the list of display values from the map
  const displayUserTypes = Object.values(userTypesMap);

  const handleTypeChange = (displayTypes: string[]) => {
    if (displayTypes.length === 0) {
      setSelectedTypes(["All"]);
      handleFilterByType(["All"]);
    } else {
      // Convert display types to actual types
      const actualTypes: UserType[] = displayTypes.map(
        (displayType) =>
          (Object.keys(userTypesMap) as UserType[]).find(
            (key) => userTypesMap[key] === displayType
          )!
      );

      setSelectedTypes(actualTypes);
      handleFilterByType(actualTypes);
    }
  };

  return (
    <DropdownFilter
      label="Type"
      options={displayUserTypes}
      selectedOptions={selectedTypes.map(
        (type) => userTypesMap[type as UserType]
      )}
      handleOptionChange={handleTypeChange}
    />
  );
};

export default FilterByRole;
