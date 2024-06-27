import React, { useState } from "react";
import { InputGroup, Button, FormControl, FormText } from "react-bootstrap";
import { Search } from "react-bootstrap-icons";

interface SearchBoxProps {
  handleFilterBySearchTerm: (searchTerm: string) => void;
}

const SearchBox: React.FC<SearchBoxProps> = ({ handleFilterBySearchTerm }) => {
  const [searchTerm, setSearchTerm] = useState("");

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
