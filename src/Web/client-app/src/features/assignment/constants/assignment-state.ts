export enum AssignmentState {
  'Accepted',
  'Waiting for acceptance',
  'Declined',
}

export type AssignmentStateKey = keyof typeof AssignmentState;
