import React, { useEffect } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppDispatch, useAppState } from "../../../redux/redux-hooks";
import { getUserTypes, setUserQuery } from "../reducers/user-slice";
import useUserList from "../list/useUsersList";

interface FilterByRoleProps {}

const FilterByRole: React.FC<FilterByRoleProps> = () => {
  const {
    types,
    userQuery: { types: userTypes },
  } = useAppState((state) => state.users);
  const { handleFilterByType } = useUserList();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (types.length <= 1) {
      dispatch(getUserTypes());
    }
  }, [dispatch, types.length]);

  type UserType = (typeof types)[number];

  // Mapping of actual values to display values
  const userTypesMap: Record<UserType, string> = types.reduce(
    (acc, type) => {
      acc[type as UserType] = type.slice(0, 5);
      return acc;
    },
    {} as Record<UserType, string>
  );

  const displayUserTypes = Object.values(userTypesMap);

  const handleTypeChange = (displayTypes: string[]) => {
    if (displayTypes.length === 0) {
      handleFilterByType(["All"]);
    } else {
      // Convert display types to actual types
      const actualTypes: UserType[] = displayTypes
        .map(
          (displayType) =>
            (Object.keys(userTypesMap) as UserType[]).find(
              (key) => userTypesMap[key] === displayType
            )!
        )
        .filter((type) => type !== "All");

      handleFilterByType(actualTypes);
    }
  };

  return (
    <DropdownFilter
      label="Type"
      options={displayUserTypes}
      selectedOptions={userTypes.map((type) => userTypesMap[type as UserType])}
      handleOptionChange={handleTypeChange}
      placeholder="Type"
    />
  );
};

export default FilterByRole;
