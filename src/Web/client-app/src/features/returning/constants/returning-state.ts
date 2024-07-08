export enum ReturningState {
    'Completed',
    'Waiting for returning'
};

export type ReturningStateKey = keyof typeof ReturningState;