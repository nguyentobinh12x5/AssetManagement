import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'
import { RootDispatch, RootState } from './store'

export const useAppDispatch = () => useDispatch<RootDispatch>();
export const useAppState: TypedUseSelectorHook<RootState> = useSelector;