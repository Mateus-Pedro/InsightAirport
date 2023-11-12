import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../RegisterPilot/styles.css";
import Header from "../components/Header";
import RegisterPilotForm from "../components/RegisterPilotForm/index.jsx";

function RegisterPilot() {
  const [data, setData] = useState([]);
  const api = "https://localhost:7149/api/Pilot";

  const navigate = useNavigate();

  const handleAddRegisterPilot = async (newRegisterPilot) => {
    try {
      const response = await fetch(api, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newRegisterPilot),
      });

      if (!response.ok) {
        toast.error("Failed to add Pilot. Please try again.");
        return;
      }

      const result = await response.json();
      setData([...data, result]);

      navigate("/Pilots");

      toast.success("Pilot added successfully!");
    } catch (error) {
      toast.error("An unexpected error occurred. Please try again.");
    }
  };

  return (
    <div>
      <Header />
      <div className="container">
        <div className="header-container">
          <h1 className="header-title">Register Pilot</h1>
        </div>
      </div>
      <div className="content-table-div">
        <div className="content-table-header">
          <div className="content-table-info">
            <p className="content-table-name">Name</p>
            <p className="content-table-birth">Date Birth</p>
            <p className="content-table">Request</p>
          </div>
        </div>
        <div className="content-table">
          <RegisterPilotForm onAddRegisterPilot={handleAddRegisterPilot} />
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}

export default RegisterPilot;
