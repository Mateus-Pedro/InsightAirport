import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import styled from "styled-components";

const StyledButton = styled(Button)`
  && {
    height: 40px;
    min-width: 100px;
  }
`;
const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  width: 100%;
 
`;

const AirplaneForm = ({ onAddAirplane }) => {
  const [name, setName] = useState("");
  const [departureTime, setDepartureTime] = useState("");
  const [arrivalTime, setArrivalTime] = useState("");

  const handleAddAirplane = () => {
    const newAirplane = {
      name,
      flightStatus: 1,
      departureTime,
      arrivalTime,
    };

    onAddAirplane(newAirplane);
    
    setName("");
    setDepartureTime("");
    setArrivalTime("");
  };

  return (
    <div className="container-info">
      <div className="container-info-values">
        <div className="container-info-values-treatment" style={{alignItems: 'center'}}>
          <TextField
            variant="outlined"
            value={name}
            onChange={(e) => setName(e.target.value)}
            fullWidth
            // margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <TextField
            type="datetime-local"
            variant="outlined"
            value={departureTime}
            onChange={(e) => setDepartureTime(e.target.value)}
            fullWidth
            // margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <TextField
            type="datetime-local"
            variant="outlined"
            value={arrivalTime}
            onChange={(e) => setArrivalTime(e.target.value)}
            fullWidth
            // margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <ButtonContainer>
            <StyledButton
              variant="contained"
              color="primary"
              onClick={handleAddAirplane}
            >
              Add Airplane
            </StyledButton>
          </ButtonContainer>
        </div>
      </div>
    </div>
  );
};

export default AirplaneForm;
