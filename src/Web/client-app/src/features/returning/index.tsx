import { Route, Routes } from "react-router-dom";
import { RETURNING_LIST } from "./constants/returning-list";

import ReturningList from "./list";

const Returnings = () => {
  return (
    <Routes>
      <Route path={RETURNING_LIST} element={<ReturningList />} />
    </Routes>
  );
};

export default Returnings;
