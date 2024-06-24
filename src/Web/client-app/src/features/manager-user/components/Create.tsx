import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Loading from "../../../components/Loading";
import IPagination from "../../../components/table/interfaces/IPagination";

const CreateUser = () => {
  const navigate = useNavigate();

  const formatDate = (date: Date) => {
    const parsedDate = typeof date === "string" ? new Date(date) : date;
    const day = String(parsedDate.getDate()).padStart(2, "0");
    const month = String(parsedDate.getMonth() + 1).padStart(2, "0");
    const year = parsedDate.getFullYear();
    return `${day}/${month}/${year}`;
  };
};

export default CreateUser;
