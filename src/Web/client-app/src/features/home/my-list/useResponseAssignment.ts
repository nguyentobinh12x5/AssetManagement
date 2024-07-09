import { useState } from 'react';
import { AssignmentState } from '../../assignment/constants/assignment-state';

const useResponseAssignment = () => {
  const [currentSelectedAssignment, setCurrentSelectedAssignment] = useState<
    number | null
  >(null);
  const [typeModal, setTypeModal] = useState(AssignmentState.Accepted);

  const handleShowAcceptModal = (id: number) => {
    setCurrentSelectedAssignment(id);
    setTypeModal(AssignmentState.Accepted);
  };
  const handleShowDeclineModal = (id: number) => {
    setCurrentSelectedAssignment(id);
    setTypeModal(AssignmentState.Declined);
  };
  const handleShowReturningRequestModal = (id: number) => {
    setCurrentSelectedAssignment(id);
    setTypeModal(AssignmentState['Waiting for returning']);
  };

  const hideDisableModal = () => {
    setCurrentSelectedAssignment(null);
  };

  return {
    handleShowAcceptModal,
    handleShowDeclineModal,
    currentSelectedAssignment,
    typeModal,
    hideDisableModal,
    handleShowReturningRequestModal,
  };
};

export default useResponseAssignment;
