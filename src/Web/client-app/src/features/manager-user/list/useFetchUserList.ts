import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { getUsers } from '../reducers/user-slice';

const useFetchUserList = () => {
  const dispatch = useAppDispatch();
  const { users, userQuery, isDataFetched } = useAppState(
    (state) => state.users
  );

  // Fetch Data
  useEffect(() => {
    if (!isDataFetched) {
      dispatch(getUsers(userQuery));
    }
  }, [dispatch, userQuery, isDataFetched]);

  return { users, userQuery };
};

export default useFetchUserList;
