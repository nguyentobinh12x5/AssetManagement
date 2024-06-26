import { NavigateFunction } from 'react-router-dom';

let navigate: NavigateFunction;

export const setNavigate = (navigateInstance: NavigateFunction) => {
  navigate = navigateInstance;
};

export const navigateTo = (path: string) => {
  if (navigate) {
    navigate(path);
  }
};
