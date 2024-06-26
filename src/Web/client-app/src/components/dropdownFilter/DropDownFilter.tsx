import React, { useState } from "react";
import {
  Dropdown,
  FormControl,
  InputGroup,
  DropdownButton,
  Form,
} from "react-bootstrap";
import { FunnelFill } from "react-bootstrap-icons";
import "./DropDownFilter.scss";

interface Props {
  label: string;
  options: string[];
  selectedOptions: string[];
  handleOptionChange: (options: string[]) => void;
  placeholder?: string;
}

const DropdownFilter: React.FC<Props> = ({
  options,
  selectedOptions,
  handleOptionChange,
  placeholder,
}) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
    setDropdownOpen(true);
  };

  const handleCheckboxChange = (option: string) => {
    const currentIndex = selectedOptions.indexOf(option);
    const newSelectedOptions = [...selectedOptions];

    if (currentIndex === -1) {
      newSelectedOptions.push(option);
    } else {
      newSelectedOptions.splice(currentIndex, 1);
    }

    handleOptionChange(newSelectedOptions);
  };

  const filteredOptions = options.filter((option) =>
    option.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleDropdownToggle = () => {
    if (filteredOptions.length > 0) {
      setDropdownOpen(!dropdownOpen);
    }
  };

  const handleDropdownItemClick = (option: string) => {
    handleCheckboxChange(option);
    setDropdownOpen(true);
  };

  return (
    <InputGroup
      className="custom-input-group d-inline-flex align-items-center mb-3"
      onClick={() => {
        handleDropdownToggle();
      }}
    >
      <FormControl
        placeholder={placeholder ? placeholder : "Search..."}
        value={searchTerm}
        onChange={handleSearchChange}
        onClick={() => setDropdownOpen(true)}
      />
      <DropdownButton
        className="btn-group position-static"
        show={dropdownOpen}
        variant="outline-secondary"
        title={<FunnelFill />}
        id="input-group-dropdown-1"
        align="end"
      >
        {filteredOptions.length > 0 ? (
          filteredOptions.map((option, index) => (
            <Dropdown.Item
              key={index}
              onClick={(e) => {
                e.stopPropagation();
                handleDropdownItemClick(option);
              }}
            >
              <Form.Check
                type="checkbox"
                id={`checkbox-${index}`}
                label={option}
                checked={selectedOptions.includes(option)}
                onChange={() => handleCheckboxChange(option)}
              />
            </Dropdown.Item>
          ))
        ) : (
          <Dropdown.Item disabled>No options found</Dropdown.Item>
        )}
      </DropdownButton>
    </InputGroup>
  );
};

export default DropdownFilter;
