import React, { useState } from "react";
import { InputGroup, Button, FormControl } from "react-bootstrap";
import { Search } from "react-bootstrap-icons";

interface SearchBoxProps {
  defaultValue?: string;
  handleFilterBySearchTerm: (searchTerm: string) => void;
}

const SearchBox: React.FC<SearchBoxProps> = ({
  handleFilterBySearchTerm,
  defaultValue = "",
}) => {
  const [searchTerm, setSearchTerm] = useState(defaultValue);

  const onSearch = () => {
    handleFilterBySearchTerm(searchTerm);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setSearchTerm(value);
  };

  return (
    <>
      <InputGroup className="mb-3">
        <FormControl
          maxLength={256}
          value={searchTerm}
          onChange={handleInputChange}
          onKeyDown={(e) => {
            if (e.key === "Enter") onSearch();
          }}
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
    </>
  );
};

export default SearchBox;
