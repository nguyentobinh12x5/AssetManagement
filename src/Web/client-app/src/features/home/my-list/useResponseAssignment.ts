import { useState } from 'react';

const useResponseAssignment = () => {
  const [currentSelectedAssignment, setCurrentSelectedAssignment] = useState<
    number | null
  >(null);
  const [isAcceptModal, setIsAcceptModal] = useState(true);

  const handleShowAcceptModal = (id: number) => {
    setCurrentSelectedAssignment(id);
    setIsAcceptModal(true);
  };
  const handleShowDeclineModal = (id: number) => {
    setCurrentSelectedAssignment(id);
    setIsAcceptModal(false);
  };

  const hideDisableModal = () => {
    setCurrentSelectedAssignment(null);
  };

  return {
    handleShowAcceptModal,
    handleShowDeclineModal,
    currentSelectedAssignment,
    isAcceptModal,
    hideDisableModal,
  };
};

export default useResponseAssignment;
