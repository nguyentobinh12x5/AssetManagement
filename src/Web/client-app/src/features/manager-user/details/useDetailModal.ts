import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { getUserById } from '../reducers/user-slice';
import { useEffect } from 'react';

const useDetailUser = (id: string) => {
  const dispatch = useAppDispatch();
  const { user } = useAppState((state) => state.users);

  useEffect(() => {
    if (id) {
      dispatch(getUserById(id));
    }
  }, [id]);

  return { user };
};

export default useDetailUser;
