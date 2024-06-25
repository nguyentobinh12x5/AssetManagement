import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import reportWebVitals from "./reportWebVitals";
import NProgress from "nprogress";
import { Provider } from "react-redux";
import { store } from "./redux/store";
import AppRoutes from "./routes";

import "bootstrap/dist/css/bootstrap.min.css";

import "./styles/index.scss";
import "./styles/App.scss";
import { ToastProvider } from "./components/toastify/ToastContext";
import ToastContainer from "./components/toastify/ToastContainer";
import AuthProvider from "./features/auth/AuthProvider";
import NavigateContext from "./components/navigate/NavigateContext";

NProgress.configure({ minimum: 1 });

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <NavigateContext>
          <ToastProvider>
            <AuthProvider>
              <AppRoutes />
            </AuthProvider>
            <ToastContainer />
          </ToastProvider>
        </NavigateContext>
      </BrowserRouter>
    </Provider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
