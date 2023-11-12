import React, { useState, useEffect } from "react";
import Button from "@material-ui/core/Button";
import "./styles.css";
import { ToastContainer, toast } from "react-toastify";

function PilotInfo({ pilotId, name, dateBirth, airplane, updateData }) {
  const [airplanesToday, setAirplanesToday] = useState([]);
  const [airplaneSelecionado, setAirplaneSelecionado] = useState("");
  const [isAddingAirplane, setIsAddingAirplane] = useState(false);

  useEffect(() => {
    const fetchAirplanesToday = async () => {
      try {
        const response = await fetch(
          "https://localhost:7149/api/Airplane/today"
        );
        const result = await response.json();
        setAirplanesToday(result);
      } catch (error) {
        toast.error("Error fetching airplanes today:", error);
      }
    };

    fetchAirplanesToday();
  }, []);

  const handleAddAirplane = () => {
    setIsAddingAirplane(true);
  };

  const handleSelectChange = (event) => {
    setAirplaneSelecionado(event.target.value);
  };

  const handleSubmit = async () => {
    try {
      const response = await fetch(
        `https://localhost:7149/api/Pilot/${pilotId}/add-airplane/${airplaneSelecionado}`,
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (response.ok) {
        toast.success("Airplane added successfully");
        updateData();
        setIsAddingAirplane(false);
        setAirplaneSelecionado("");
      } else {
        const errorText = await response.text();
        toast.error(errorText);
      }
    } catch (error) {
      toast.error(error);
    }
  };

  const handleRemovePilot = async () => {
    try {
      const response = await fetch(
        `https://localhost:7149/api/Pilot/${pilotId}`,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      if (response.ok) {
        toast.success(`Pilot ${name} removed successfully`);
        updateData();
      } else {
        const errorText = await response.text();
        toast.error(errorText);
      }
    } catch (err) {
      toast.error("Error removing pilot");
    }
  }

  const handleRemoveAirplane = async () => {
    try {
      const response = await fetch(
        `https://localhost:7149/api/Pilot/${pilotId}/remove-airplane`,
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (response.ok) {
        toast.success("Airplane removed successfully");
        updateData();
      } else {
        const errorText = await response.text();
        toast.error(errorText);
      }
    } catch (error) {
      toast.error("Error removing airplane");
    }
  };

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
      <div
        className="container-info-name-pilot-div"
        style={{
          display: "flex",
          flexDirection: 'row-reverse',
          alignItems: "center",
          gap: 12,
        }}
      >
        <p className="container-info-name-pilot" style={{width: 60}}>{name}</p>
        <div
          className="container-info-name-pilot-button"
          style={{
            background: "red",
            color: "black",
            textAlign: "center",
            width: 50,
            height: 50,
            display: 'flex',
            alignItems: 'center',
            gap: 8,
            padding: 10,
            borderRadius: 100,
            marginLeft: -20
          }}
          onClick={handleRemovePilot}
        >
          {/* <p>Delete</p> */}
          <svg
            xmlns="http://www.w3.org/2000/svg"
            x="0px"
            y="0px"
            width="50"
            height="auto"
            viewBox="0 0 24 24"
          >
            <path d="M 10 2 L 9 3 L 4 3 L 4 5 L 5 5 L 5 20 C 5 20.522222 5.1913289 21.05461 5.5683594 21.431641 C 5.9453899 21.808671 6.4777778 22 7 22 L 17 22 C 17.522222 22 18.05461 21.808671 18.431641 21.431641 C 18.808671 21.05461 19 20.522222 19 20 L 19 5 L 20 5 L 20 3 L 15 3 L 14 2 L 10 2 z M 7 5 L 17 5 L 17 20 L 7 20 L 7 5 z M 9 7 L 9 18 L 11 18 L 11 7 L 9 7 z M 13 7 L 13 18 L 15 18 L 15 7 L 13 7 z"></path>
          </svg>
        </div>
      </div>
      <div className="container-info-values">
        <div className="container-info-values-treatment">
          <p className="container-info-time">{formatDateTime(dateBirth)}</p>
          <p className="container-info-airplane">
            {airplane && airplane.name ? airplane.name : "No airplane"}
          </p>
        </div>
        <ToastContainer />
        <div className="container-info-buttons">
          {!isAddingAirplane && airplane == null && (
            <div className="button">
              <Button
                variant="contained"
                color="primary"
                size="small"
                style={{ fontSize: "12px", padding: "5px 10px" }}
                onClick={handleAddAirplane}
              >
                Add Airplane
              </Button>
            </div>
          )}
          {airplane != null && (
            <div className="button">
              <Button
                variant="contained"
                color="secondary"
                size="small"
                style={{
                  fontSize: "12px",
                  padding: "5px 10px",
                  marginLeft: "10px",
                }}
                onClick={handleRemoveAirplane}
              >
                Remove Airplane
              </Button>
            </div>
          )}
          {isAddingAirplane && (
            <div className="select-container">
              <select
                id="airplane-select"
                value={airplaneSelecionado}
                onChange={handleSelectChange}
              >
                <option value="" disabled>
                  Choose an airplane
                </option>
                {airplanesToday.map((airplane) => (
                  <option key={airplane.id} value={airplane.id}>
                    {airplane.name}
                  </option>
                ))}
              </select>
              <Button
                variant="contained"
                color="primary"
                size="small"
                style={{
                  fontSize: "12px",
                  marginTop: "8px",
                  marginRight: "5px",
                }}
                onClick={handleSubmit}
              >
                Submit
              </Button>
              <Button
                variant="contained"
                color="secondary"
                size="small"
                style={{ fontSize: "12px", marginTop: "8px" }}
                onClick={() => setIsAddingAirplane(false)}
              >
                Cancel
              </Button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default PilotInfo;
