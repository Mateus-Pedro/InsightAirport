import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import ControlTower from "../ControlTower";
import Airplane from "../Airplane";
import Communicationlog from "../Communicationlog";
import Pilot from "../Pilot";
import RegisterPilot from "../RegisterPilot";

function RouterApp() {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<ControlTower />} />
        <Route path="/home" element={<ControlTower />} />
        <Route path="/Communicationlog" element={<Communicationlog />} />
        <Route path="/Airplane" element={<Airplane />} />
        <Route path="/Pilots" element={<Pilot />} />
        <Route path="/Register-Pilot" element={<RegisterPilot />} />
      </Routes>
    </BrowserRouter>
  );
}

export default RouterApp;
