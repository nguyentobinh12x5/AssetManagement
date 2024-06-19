import React from "react";
import { Route, Routes } from "react-router-dom";
import LoginForm from "./LoginForm";

const Login = () => {
  return (
    <div className="d-flex justify-content-center align-items-center mt-5">
      <LoginForm />
    </div>
  );
};

export default Login;
