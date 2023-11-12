import React from "react";
import { Container, Content } from "./styles";
import { FaTimes, FaHome } from "react-icons/fa";
import { GiSatelliteCommunication, GiCommercialAirplane } from "react-icons/gi";
import { BsPersonCircle } from "react-icons/bs";
import SidebarItem from "../SidebarItem";
import { Link } from "react-router-dom";

const Sidebar = ({ active }) => {
  const closeSidebar = () => {
    active(false);
  };

  return (
    <Container sidebar={active}>
      <FaTimes onClick={closeSidebar} />
      <Content>
        <Link to="/home">
          <SidebarItem Icon={FaHome} Text="Home" />
        </Link>
        <Link to="/Pilots">
          <SidebarItem Icon={BsPersonCircle} Text="Pilots" />
        </Link>
        <Link to="/Communicationlog">
          <SidebarItem
            Icon={GiSatelliteCommunication}
            Text="Communication Log"
          />
        </Link>
        <Link to="/Airplane">
          <SidebarItem Icon={GiCommercialAirplane} Text="Register Airplane" />
        </Link>
      </Content>
    </Container>
  );
};

export default Sidebar;
