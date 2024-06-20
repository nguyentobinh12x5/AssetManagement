import React, { useState } from "react";
import { InputGroup, Button, FormControl } from "react-bootstrap";
import { Search } from "react-bootstrap-icons";
import useUserList from "../list/useUsersList";

interface SearchBoxProps {
    handleFilterBySearchTerm: (searchTerm: string) => void;
}

const SearchBox: React.FC<SearchBoxProps> = ({ handleFilterBySearchTerm }) => {
  const [searchTerm, setSearchTerm] = useState("");

  const onSearch = () => {
    handleFilterBySearchTerm(searchTerm);
  };

  return (
    <InputGroup className="mb-3">
      <FormControl
        placeholder="Recipient's username"
        aria-label="Recipient's username"
        aria-describedby="basic-addon2"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <Button variant="outline-secondary" id="button-addon2" onClick={onSearch}>
        <Search />
      </Button>
    </InputGroup>
  );
};

export default SearchBox;
