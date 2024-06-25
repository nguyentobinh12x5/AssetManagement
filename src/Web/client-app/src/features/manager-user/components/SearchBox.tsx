import React, { useState } from "react";
import { InputGroup, Button, FormControl, FormText } from "react-bootstrap";
import { Search } from "react-bootstrap-icons";

interface SearchBoxProps {
  handleFilterBySearchTerm: (searchTerm: string) => void;
}

const SearchBox: React.FC<SearchBoxProps> = ({ handleFilterBySearchTerm }) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [warningMessage, setWarningMessage] = useState("");

  const onSearch = () => {
    handleFilterBySearchTerm(searchTerm);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value.length > 256) {
      setWarningMessage("Search term cannot exceed 256 characters.");
    } else {
      setWarningMessage("");
    }
    setSearchTerm(value);
  };

  return (
    <>
      <InputGroup className="mb-3">
        <FormControl
          value={searchTerm}
          onChange={handleInputChange}
          isInvalid={searchTerm.length > 256}
        />
        <Button
          variant="outline-secondary"
          id="button-addon2"
          onClick={onSearch}
          disabled={searchTerm.length > 256}
        >
          <Search />
        </Button>
      </InputGroup>
      {warningMessage && (
        <FormText className="text-danger">{warningMessage}</FormText>
      )}
    </>
  );
};

export default SearchBox;
