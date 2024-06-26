import React from "react";
import { CaretDownFill, CaretUpFill } from "react-bootstrap-icons";
import ISortState from "./interfaces/ISortState";
import SORT_TYPE from "./constants/SortType";

interface Props {
  name: string;
  sortState: ISortState;
}

const ColumnICon: React.FC<Props> = ({ name, sortState }) => {
  return name === sortState.name &&
    sortState.orderBy === SORT_TYPE.DESCENDING ? (
    <CaretUpFill />
  ) : (
    <CaretDownFill />
  );
};

export default ColumnICon;
