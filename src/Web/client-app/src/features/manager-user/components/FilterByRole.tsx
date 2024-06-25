import React, { useEffect, useState } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppDispatch, useAppState } from "../../../redux/redux-hooks";
import { getUserTypes, setUserTypes } from "../reducers/user-slice";
import { setUser } from "../../auth/reducers/auth-slice";

interface FilterByRoleProps {
  handleFilterByType: (types: string[]) => void;
}

const FilterByRole: React.FC<FilterByRoleProps> = ({ handleFilterByType }) => {
  const [selectedTypes, setSelectedTypes] = useState<string[]>([]);
  const { types } = useAppState((state) => state.users);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (types.length <= 1) dispatch(getUserTypes());
  });

  // Define the type for user types
  type UserType = (typeof types)[number];
  //type UserType = "Staff" | "Administrator" | "All";

  // Mapping of actual values to display values
  const userTypesMap: Record<UserType, string> = types.reduce(
    (acc, type) => {
      acc[type as UserType] = type.slice(0, 5);
      return acc;
    },
    {} as Record<UserType, string>
  );

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
