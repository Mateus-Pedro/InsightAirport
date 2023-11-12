import React from "react";
import "./styles.css";

function CommunicationlogInfo({ message, timestamp }) {
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

  return (
    <div className="container-info">
      <div className="container-info-values">
        <div className="container-info-values-treatment">
          <p className="container-info-time">{formatDateTime(timestamp)}</p>
          <p className="container-info-message">{message}</p>
        </div>
      </div>
    </div>
  );
}

export default CommunicationlogInfo;
