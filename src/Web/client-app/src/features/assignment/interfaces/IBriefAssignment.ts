import { AssignmentState } from '../constants/assignment-state';
export interface IBriefAssignment {
  id: string;
  assetCode: string;
  assetName: string;
  assignedTo: string;
  assignedBy: string;
  assignedDate: string;
  state: AssignmentState;
}
