import { ReturningState } from '../constants/returning-state';

export interface IBriefReturning {
  id: string;
  assetCode: string;
  assetName: string;
  requestedBy: string;
  assignedDate: string;
  acceptedBy: string;
  returnedDate: string;
  state: ReturningState;
}
