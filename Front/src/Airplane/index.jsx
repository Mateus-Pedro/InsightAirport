import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../Airplane/styles.css";
import Header from "../components/Header";
import AirplaneForm from "../components/AirplaneForm/index.jsx";

function Airplane() {
  const [data, setData] = useState([]);
  const api = "https://localhost:7149/api/Airplane";

  const navigate = useNavigate();

  const handleAddAirplane = async (newAirplane) => {
    try {
      const departureTime = new Date(newAirplane.departureTime);
      const arrivalTime = new Date(newAirplane.arrivalTime);
      const today = new Date();
      today.setHours(0, 0, 0, 0);
      
      if (departureTime > arrivalTime) {
        toast.error("The departure time must be less than the arrival time.");
        return;
      }
      
      if (departureTime < today) {
        toast.error("Departure time must be from today onwards.");
        return;
      }
      
      if (arrivalTime < today) {
        toast.error("Arrival time must be from today onwards.");
        return;
      }      

      const response = await fetch(api, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newAirplane),
      });

      if (!response.ok) {
        toast.error("Failed to add airplane. Please try again.");
        return;
      }else {
        const errorText = await response.text();
        toast.error(errorText);
      }

      const result = await response.json();
      setData([...data, result]);

      navigate("/home");

      toast.success("Airplane added successfully!");
    } catch (error) {
      toast.error("An unexpected error occurred. Please try again.");
    }
  };

  return (
    <div>
      <Header />
      <div className="container">
        <div className="header-container">
          <h1 className="header-title">Register airplane</h1>
        </div>
      </div>
      <div className="content-table-div">
        <div className="content-table-header">
          <div className="content-table-info">
            <p className="content-table-name">Name</p>
            <p className="content-table-departure">Departure Time</p>
            <p className="content-table-arrivel">Arrival Time</p>
            <p className="content-table">Request</p>
          </div>
        </div>
        <div className="content-table">
          <AirplaneForm onAddAirplane={handleAddAirplane} />
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}

export default Airplane;
