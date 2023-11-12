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

const RegisterPilotForm = ({ onAddRegisterPilot }) => {
  const [name, setName] = useState("");
  const [dateBirth, setDateBirth] = useState("");

  const handleAddRegisterPilot = () => {
    const newRegisterPilot = {
      name,
      dateBirth
    };

    onAddRegisterPilot(newRegisterPilot);

    setName("");
    setDateBirth("");
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
            style={{boxSizing: "border-box", width: "calc(100% - 16px)", flexDirection: 'none'}}
            className="text-field-name"
          />

          <TextField
            type="datetime-local"
            variant="outlined"
            value={dateBirth}
            onChange={(e) => setDateBirth(e.target.value)}
            fullWidth
            // margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <ButtonContainer>
            <StyledButton
              variant="contained"
              color="primary"
              onClick={handleAddRegisterPilot}
            >
              Add Pilot
            </StyledButton>
          </ButtonContainer>
        </div>
      </div>
    </div>
  );
};

export default RegisterPilotForm;
