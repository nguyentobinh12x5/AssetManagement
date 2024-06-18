import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface CounterState {
  value: number;
}

const initialState: CounterState = {
  value: 0,
};

const counterSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    incremented(state) {
      //You can only write "mutating" logic in Redux Toolkit's
      //createSlice and createReducer because they use Immer inside!
      //If you write mutating logic in reducers without Immer,
      //it will mutate the state and cause bugs!
      state.value++;
    },
    decremented(state) {
      state.value--;
    },
    amountAdded(state, action: PayloadAction<number>) {
      state.value += action.payload;
    },
  },
});

export const { incremented, decremented, amountAdded } = counterSlice.actions;
export default counterSlice.reducer;
