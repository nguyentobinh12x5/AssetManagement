import React, { useState } from "react";
import {
  Dropdown,
  FormControl,
  InputGroup,
  DropdownButton,
} from "react-bootstrap";
import { FunnelFill } from "react-bootstrap-icons";

interface Props {
  label: string;
  options: string[];
  selectedOption: string;
  handleOptionChange: (option: string) => void;
}

const DropdownFilter: React.FC<Props> = ({
  options,
  selectedOption,
  handleOptionChange,
}) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
    setDropdownOpen(true); // Show the dropdown menu when the search bar changes() Not working)
  };

  const filteredOptions = options.filter((option) =>
    option.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <Dropdown
      show={dropdownOpen}
      onToggle={(isOpen) => setDropdownOpen(isOpen)}
    >
      <InputGroup className="d-inline-flex align-items-center mb-3">
        <FormControl
          placeholder="Search..."
          value={searchTerm}
          onChange={handleSearchChange}
          onClick={() => setDropdownOpen(true)}
        />
        <DropdownButton
          variant="outline-secondary"
          title={<FunnelFill />}
          id="input-group-dropdown-1"
          align="end"
        >
          {filteredOptions.length > 0 ? (
            filteredOptions.map((option, index) => (
              <Dropdown.Item
                key={index}
                onClick={() => {
                  handleOptionChange(option);
                  setDropdownOpen(false);
                }}
                active={option === selectedOption}
              >
                {option}
              </Dropdown.Item>
            ))
          ) : (
            <Dropdown.Item disabled>No options found</Dropdown.Item>
          )}
        </DropdownButton>
      </InputGroup>
    </Dropdown>
  );
};

export default DropdownFilter;
