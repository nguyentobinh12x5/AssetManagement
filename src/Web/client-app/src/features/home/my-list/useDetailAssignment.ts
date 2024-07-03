import { useState } from 'react';

const useDetailAssignment = () => {
  const [selectedAssignment, setSelectedAssignment] = useState<number | null>(
    null
  );

  const handleShowDetail = (id: number) => {
    setSelectedAssignment(id);
  };

  const handleClosePopup = () => {
    setSelectedAssignment(null);
  };

  return { handleShowDetail, handleClosePopup, selectedAssignment };
};

export default useDetailAssignment;
