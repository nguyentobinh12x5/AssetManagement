export enum AssignmentState {
  'Accepted' = 0,
  'Waiting for acceptance' = 1,
}

export type AssignmentStateKey = keyof typeof AssignmentState;
