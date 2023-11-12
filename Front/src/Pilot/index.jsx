import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./styles.css";
import Header from "../components/Header";
import PilotInfo from "../components/PilotInfo/index.jsx";
import { toast } from "react-toastify";
import Button from "@material-ui/core/Button";

function Pilot() {
  const [data, setData] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const api = "https://localhost:7149/api/Pilot";

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
        dado.name && dado.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

  return (
    <div>
      <Header />
      <div className="container">
        <div className="header-container">
          <h1 className="header-title">Registered pilots</h1>
          <div className="filter-section">
            <div>
              <Link to="/register-pilot">
                <Button variant="contained" color="Primary" size="small">
                  Add New Pilot
                </Button>
              </Link>
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
                placeholder="Search name"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
            </div>
          </div>
        </div>
      </div>
      <div className="content-table-div">
        <div className="content-table-header">
          <div className="content-table-info">
            <p className="content-table-name-pilot">Name</p>
            <p className="content-table-time">Date Birth</p>
            <p className="content-table-airplane">Airplane</p>
            <p className="content-table-status-request">Status Request</p>
          </div>
        </div>
        <div className="content-table">
          {filteredData.length > 0 ? (
            filteredData.map((dado) => (
              <PilotInfo
                key={dado.id}
                pilotId={dado.id}
                name={dado.name}
                dateBirth={dado.dateBirth}
                airplane={dado.airplane}
                updateData={updateData}
              />
            ))
          ) : (
            <div className="content-table-error">No pilot found</div>
          )}
        </div>
      </div>
    </div>
  );
}

export default Pilot;
