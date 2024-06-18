// src/toastHelper.ts

import { ToastContextType } from "./ToastContext";

let toast: ToastContextType['toast'];

export const setToast = (toastInstance: ToastContextType['toast']) => {
  toast = toastInstance;
};

export const showToast = (content: string, variant?: 'primary' | 'secondary' | 'success' | 'danger' | 'warning' | 'info' | 'light' | 'dark') => {
  if (toast) {
    toast(content, variant);
  }
};

export const showSuccessToast = (content: string) => {
  if (toast) {
    toast.success(content);
  }
};

export const showErrorToast = (content: string) => {
    console.log("Show Toast")
    console.log(toast)
  if (toast) {
    toast.error(content);
  }
};

export const showInfoToast = (content: string) => {
  if (toast) {
    toast.info(content);
  }
};
