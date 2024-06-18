import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { AUTH_LOGIN } from "./constants/auth-login";

const Login = lazy(() => import("./login"));

const Auth = () => {
  return (
    <Routes>
      <Route path={AUTH_LOGIN} element={<Login />} />
    </Routes>
  );
};

export default Auth;
