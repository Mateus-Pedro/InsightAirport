import React, { useState, useEffect } from "react";
import "./styles.css";
import Header from "../components/Header";
import ControlTowerInfo from "../components/ControlTowerInfo/index.jsx";
import { toast } from "react-toastify";

function ControlTower() {
  const [data, setData] = useState([]);
  const [selectedFlightStatus, setSelectedFlightStatus] = useState("");
  const [searchTerm, setSearchTerm] = useState("");
  const api = "https://localhost:7149/api/Airplane/today";

  const statusOptions = {
    1: "Requesting Exit",
    2: "On Runway",
    3: "In Flight",
    4: "Requesting Landing",
    5: "Landing",
    6: "End of Route",
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(api);
        const result = await response.json();
        setData(result);
      } catch (error) {
        toast.error(error);
      }
    };
    fetchData();
  }, [api]);

  const updateData = async () => {
    try {
      const response = await fetch(api);
      const result = await response.json();
      setData(result);
    } catch (error) {
      toast.error(error);
    }
  };

  const airplaneData = data || [];

  const filteredData =
    airplaneData &&
    airplaneData.filter(
      (dado) =>
        dado.name &&
        dado.name.toLowerCase().includes(searchTerm.toLowerCase()) &&
        (!selectedFlightStatus ||
          dado.flightStatus === parseInt(selectedFlightStatus))
    );

  return (
    <div>
      <Header />
      <div className="container">
        <div className="header-container">
          <h1 className="header-title">Control Tower</h1>
          <div className="filter-section">
            <div className="flight-status-filter">
              <label htmlFor="flightStatus" className="flight-status">
                Status:
              </label>
              <select
                id="flightStatus"
                name="flightStatus"
                value={selectedFlightStatus}
                onChange={(e) => setSelectedFlightStatus(e.target.value)}
                className="flight-status-select"
              >
                <option value="">All</option>
                <option value="1">Requesting Exit</option>
                <option value="2">On Runway</option>
                <option value="3">In Flight</option>
                <option value="4">Requesting Landing</option>
                <option value="5">Landing</option>
                <option value="6">End of Route</option>
              </select>
            </div>
            <div className="search-container">
              <div className="search-icon">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="20"
                  height="20"
                  viewBox="0 0 20 20"
                  fill="none"
                >
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M9.54183 1.52075C5.11205 1.52075 1.521 5.1118 1.521 9.54158C1.521 13.9714 5.11205 17.5624 9.54183 17.5624C13.9716 17.5624 17.5627 13.9714 17.5627 9.54158C17.5627 5.1118 13.9716 1.52075 9.54183 1.52075ZM0.145996 9.54158C0.145996 4.35241 4.35265 0.145752 9.54183 0.145752C14.731 0.145752 18.9377 4.35241 18.9377 9.54158C18.9377 14.7308 14.731 18.9374 9.54183 18.9374C4.35265 18.9374 0.145996 14.7308 0.145996 9.54158Z"
                    fill="#1A202C"
                  />
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M16.8474 16.8471C17.1158 16.5786 17.5511 16.5786 17.8196 16.8471L19.653 18.6804C19.9215 18.9489 19.9215 19.3842 19.653 19.6527C19.3845 19.9212 18.9492 19.9212 18.6807 19.6527L16.8474 17.8194C16.5789 17.5509 16.5789 17.1156 16.8474 16.8471Z"
                    fill="#1A202C"
                  />
                </svg>
              </div>
              <input
                type="text"
                className="search-input"
                placeholder="Search airplane name"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
            </div>
          </div>
        </div>
      </div>
      <div className="content-table-div">
        <div className="content-table-header">
          <p className="content-table-airplane">Airplane</p>
          <div className="content-table-info">
            <p className="content-table-status">Status</p>
            <p className="content-table-time">Departure</p>
            <p className="content-table-time">Arrival</p>
            <p className="content-table-pilot">Pilots</p>
            <p className="content-table-status-request">Status Request</p>
          </div>
        </div>
        <div className="content-table">
          {filteredData.length > 0 ? (
            filteredData.map((dado) => (
              <ControlTowerInfo
                key={dado.id}
                name={dado.name}
                airplaneId={dado.id}
                status={statusOptions[dado.flightStatus.toString()]}
                departureTime={dado.departureTime}
                arrivalTime={dado.arrivalTime}
                statusId={dado.flightStatus}
                pilots={dado.pilots}
                updateData={updateData}
              />
            ))
          ) : (
            <div className="content-table-error">
              No Airplanes flying now with this requisits
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ControlTower;
