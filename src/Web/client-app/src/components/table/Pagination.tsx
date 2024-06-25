/* eslint-disable @typescript-eslint/no-unused-vars */
import React from "react";
import IPagination from "./interfaces/IPagination";
import Paging from "react-bootstrap/Pagination";
import usePagination from "./usePagination";
import "./Pagination.scss";

const Pagination: React.FC<IPagination> = ({
  currentPage = 1,
  totalPage = 1,
  handleChange,
}) => {
  const JUMP_ITEMS = 2;

  const [
    setDisabledFirstBtn,
    setDisabledPrevBtn,
    setDisabledNextBtn,
    setDisabledLastBtn,
  ] = usePagination(currentPage, totalPage);

  const renderFirstBtn = () => {
    return (
      <Paging.First
        disabled={setDisabledFirstBtn()}
        onClick={() => handleChange(1)}
      />
    );
  };

  const renderPrevBtn = () => {
    return (
      <Paging.Prev
        disabled={setDisabledPrevBtn()}
        onClick={() => handleChange(currentPage - 1)}
      >
        Previous
      </Paging.Prev>
    );
  };

  const renderFirstPage = () => {
    return (
      <Paging.Item active={currentPage === 1} onClick={() => handleChange(1)}>
        1
      </Paging.Item>
    );
  };

  const renderPrevEllipsis = (page: number) => {
    return page - JUMP_ITEMS > 2 ? <Paging.Ellipsis /> : <></>;
  };

  const renderMidPaging = (page: number, total: number) => {
    let prevItems = page - JUMP_ITEMS > 1 ? page - JUMP_ITEMS : 2;

    let nextItems = page + JUMP_ITEMS < total ? page + JUMP_ITEMS : total - 1;

    return total < 2 ? (
      <></>
    ) : (
      [...Array(nextItems - prevItems + 1).keys()].map((i) => (
        <Paging.Item
          key={i}
          active={i + prevItems === currentPage}
          onClick={() => handleChange(i + prevItems)}
        >
          {i + prevItems}
        </Paging.Item>
      ))
    );
  };

  const renderNextEllipsis = (page: number, total: number) => {
    return page + JUMP_ITEMS < total - 1 ? <Paging.Ellipsis /> : <></>;
  };

  const renderTotalPage = (page: number, total: number) => {
    return total <= 1 ? (
      <></>
    ) : (
      <Paging.Item
        active={total === currentPage}
        onClick={() => handleChange(total)}
      >
        {total}
      </Paging.Item>
    );
  };

  const renderNextPage = () => {
    return (
      <Paging.Next
        disabled={setDisabledNextBtn()}
        onClick={() => handleChange(currentPage + 1)}
      >
        Next
      </Paging.Next>
    );
  };

  const renderLastPage = () => {
    return (
      <Paging.Last
        disabled={setDisabledLastBtn()}
        onClick={() => handleChange(totalPage)}
      />
    );
  };

  return totalPage < 1 ? (
    <></>
  ) : (
    <Paging className="d-flex justify-content-end">
      {/* {renderFirstBtn()} */}
      {renderPrevBtn()}
      {renderFirstPage()}
      {renderPrevEllipsis(currentPage)}

      {renderMidPaging(currentPage, totalPage)}

      {renderNextEllipsis(currentPage, totalPage)}
      {renderTotalPage(currentPage, totalPage)}
      {renderNextPage()}
      {/* {renderLastPage()} */}
    </Paging>
  );
};
export default Pagination;
