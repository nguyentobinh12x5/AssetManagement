export enum AssignmentState {
  'Accepted',
  'Waiting for acceptance',
  'Declined',
}

export type AssignmentStateKey = keyof typeof AssignmentState;

export const WATTING_FOR_ACCEPTANCE = 'Waiting for acceptance';
