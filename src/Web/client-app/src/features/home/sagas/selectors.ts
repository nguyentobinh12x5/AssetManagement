import { RootState } from '../../../redux/store';

export const getMyAssignmentsQuery = (state: RootState) =>
  state.myAssignments.query;
