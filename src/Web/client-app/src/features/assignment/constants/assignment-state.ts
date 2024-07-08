export enum AssignmentState {
  'Accepted',
  'Waiting for acceptance',
  'Declined',
  'Waiting for returning',
}

export type AssignmentStateKey = keyof typeof AssignmentState;

export const WATTING_FOR_ACCEPTANCE = 'Waiting for acceptance';
export const WATTING_FOR_RETURNING = 'Waiting for returning';
