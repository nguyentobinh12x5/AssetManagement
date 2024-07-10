import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { getAssignments } from '../reducers/assignment-slice';

const useFetchAssignmentList = () => {
  const dispatch = useAppDispatch();
  const { assignments, assignmentQuery, isDataFetched } = useAppState(
    (state) => state.assignments
  );

  // Fetch Data
  useEffect(() => {
    if (!isDataFetched) {
      dispatch(getAssignments(assignmentQuery));
    }
  }, [dispatch, assignmentQuery, isDataFetched]);

  return { assignmentQuery, assignments };
};

export default useFetchAssignmentList;
