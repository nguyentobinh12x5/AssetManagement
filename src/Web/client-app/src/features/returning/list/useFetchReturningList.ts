import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { getReturnings } from '../reducers/returning-slice';

const useFetchReturningList = () => {
  const dispatch = useAppDispatch();
  const { returnings, returningQuery } = useAppState(
    (state) => state.returnings
  );

  // Fetch Data
  useEffect(() => {
    dispatch(getReturnings(returningQuery));
  }, [dispatch, returningQuery]);

  return { returningQuery, returnings };
};

export default useFetchReturningList;
