/* eslint-disable @typescript-eslint/no-unused-vars */
import Paging from 'react-bootstrap/Pagination';
import IPagination from './interfaces/IPagination';

const usePagination = (currentPage: number, totalPage: number) => {
  const JUMP_ITEMS = 2;

  const setDisabledFirstBtn = () => {
    return currentPage <= 1;
  };

  const setDisabledPrevBtn = () => {
    return currentPage - 1 < 1;
  };

  const setDisabledNextBtn = () => {
    return currentPage + 1 > totalPage;
  };

  const setDisabledLastBtn = () => {
    return currentPage >= totalPage;
  };

  return [
    setDisabledFirstBtn,
    setDisabledPrevBtn,
    setDisabledNextBtn,
    setDisabledLastBtn,
  ];
};

export default usePagination;
