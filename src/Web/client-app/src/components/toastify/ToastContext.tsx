// src/ToastContext.tsx
import React, {
  createContext,
  useState,
  useContext,
  useCallback,
  ReactNode,
  useEffect,
} from "react";
import { setToast } from "./toast-helper";

type ToastVariant =
  | "primary"
  | "secondary"
  | "success"
  | "danger"
  | "warning"
  | "info"
  | "light"
  | "dark";

type Toast = {
  id: number;
  content: string;
  variant: ToastVariant;
};

export interface ToastContextType {
  toasts: Toast[];
  toast: {
    (content: string, variant?: ToastVariant): void;
    success: (content: string) => void;
    error: (content: string) => void;
    info: (content: string) => void;
  };
  removeToast: (id: number) => void;
}

export const ToastContext = createContext<ToastContextType | undefined>(
  undefined
);

interface ToastProviderProps {
  children: ReactNode;
}

export const ToastProvider: React.FC<ToastProviderProps> = ({ children }) => {
  const [toasts, setToasts] = useState<Toast[]>([]);

  const addToast = useCallback(
    (content: string, variant: ToastVariant = "info") => {
      setToasts((prevToasts) => [
        ...prevToasts,
        { id: Date.now(), content, variant },
      ]);
    },
    []
  );

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const toast: ToastContextType["toast"] = useCallback(
    ((content: string, variant: ToastVariant = "info") => {
      addToast(content, variant);
    }) as ToastContextType["toast"],
    [addToast]
  );

  toast.success = useCallback(
    (content: string) => {
      addToast(content, "success");
    },
    [addToast]
  );

  toast.error = useCallback(
    (content: string) => {
      addToast(content, "danger");
    },
    [addToast]
  );

  toast.info = useCallback(
    (content: string) => {
      addToast(content, "info");
    },
    [addToast]
  );

  const removeToast = useCallback((id: number) => {
    setToasts((prevToasts) => prevToasts.filter((toast) => toast.id !== id));
  }, []);

  // Set the toast instance in the helper
  useEffect(() => {
    setToast(toast);
  }, [toast]);
  return (
    <ToastContext.Provider value={{ toasts, toast, removeToast }}>
      {children}
    </ToastContext.Provider>
  );
};

export const useToast = (): ToastContextType => {
  const context = useContext(ToastContext);
  if (!context) {
    throw new Error("useToast must be used within a ToastProvider");
  }
  return context;
};
