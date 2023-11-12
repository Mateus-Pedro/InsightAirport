import React, { useState } from "react";
import "./styles.css";
import Button from "@material-ui/core/Button";
import { ToastContainer, toast } from "react-toastify";

function ControlTowerInfo({
  name,
  status,
  departureTime,
  arrivalTime,
  pilots,
  statusId,
  airplaneId,
  updateData,
}) {
  const [showButtons, setShowButtons] = useState(false);
  const [showMainButton, setShowMainButton] = useState(true);

  const formatDateTime = (dateTimeString) => {
    const options = {
      year: "numeric",
      month: "numeric",
      day: "numeric",
      hour: "numeric",
      minute: "numeric",
      hour12: false,
    };

    return new Date(dateTimeString).toLocaleString(undefined, options);
  };

  const handleButtonClick = () => {
    if (!showButtons) {
      setShowButtons(true);
      setShowMainButton(false);
    }
  };

  const handleAccept = async () => {
    if (statusId === 1) {
      try {
        const response = await fetch(
          `https://localhost:7149/api/Airplane/takeoff/${airplaneId}`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
          }
        );

        if (response.ok) {
          toast.success("Takeoff request successful");
          updateData();
        } else {
          const errorText = await response.text();
          toast.error(errorText);
        }
      } catch (error) {
        toast.error(error);
      }
    }
    if (statusId === 3) {
      try {
        const response = await fetch(
          `https://localhost:7149/api/Airplane/landing/${airplaneId}`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
          }
        );

        if (response.ok) {
          toast.success("Landing request successful");
          updateData();
        } else {
          const errorText = await response.text();
          toast.error(errorText);
        }
      } catch (error) {
        toast.error("Error making takeoff request:", error);
      }
    }
    setShowButtons(false);
    setShowMainButton(true);
  };

  const handleDecline = () => {
    setShowButtons(false);
    setShowMainButton(true);
  };

  return (
    <div className="container-info">
      <p className="container-info-airplane">{name}</p>
      <div className="container-info-values">
        <div className="container-info-values-treatment">
          <p className="container-info-status">{status}</p>
          <p className="container-info-time">{formatDateTime(departureTime)}</p>
          <p className="container-info-time">{formatDateTime(arrivalTime)}</p>
          <p className="container-info-pilot">{pilots}</p>
        </div>
        <ToastContainer />
        {statusId !== 1 && statusId !== 3 && (
          <div className="container-info-button-nothing">T</div>
        )}
        {showButtons && (
          <div className="container-info-buttons">
            <div onClick={handleDecline} className="button">
              <Button variant="contained" color="secondary" size="small">
                Decline
              </Button>
            </div>
            <div onClick={handleAccept} className="button">
              <Button variant="contained" color="primary" size="small">
                Accept
              </Button>
            </div>
          </div>
        )}
        {showMainButton && (
          <div onClick={handleButtonClick} className="container-info-optins">
            {statusId === 1 && (
              <div className="container-info-button">
                <Button variant="contained" color="primary" size="small">
                  Take Off
                </Button>
              </div>
            )}
            {statusId === 3 && (
              <div className="container-info-button">
                <Button variant="contained" color="default" size="small">
                  Landing
                </Button>
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  );
}

export default ControlTowerInfo;
