import React, { useState } from 'react';
import DropdownFilter from '../../../components/dropdownFilter/DropDownFilter';

interface FilterByRoleProps {
  handleFilterByType: (type: string) => void;
}

const FilterByRole: React.FC<FilterByRoleProps> = ({ handleFilterByType }) => {
  const [selectedType, setSelectedType] = useState('');
  
  // Define the type for user types
  type UserType = 'Staff' | 'Administrator' | 'Default';

  // Mapping of actual values to display values
  const userTypesMap: Record<UserType, string> = {
    Staff: 'Staff',
    Administrator: 'Admin',
    Default: 'All',
  };

  // Get the list of display values from the map
  const displayUserTypes = Object.values(userTypesMap);

  const handleTypeChange = (displayType: string) => {
    // Find the actual value from the display value
    const actualType = (Object.keys(userTypesMap) as UserType[]).find(
      (key) => userTypesMap[key as UserType] === displayType
    );

    if (actualType) {
      setSelectedType(displayType);
      handleFilterByType(actualType);
    }
  };

  return (
    <DropdownFilter
      label="Type"
      options={displayUserTypes}
      selectedOption={selectedType}
      handleOptionChange={handleTypeChange}
    />
  );
};

export default FilterByRole;
