import styled from 'styled-components';
import logo  from '../img/logo.png'

export const Container = styled.div`
  height: 100px;
  display: flex;
  background-color: #1A202C; 
  box-shadow: 0 0 20px 3px;

  > svg {
    position: fixed;
    color: white;
    width: 30px;
    height: 30px;
    margin-top: 32px;
    margin-left: 32px;
    cursor: pointer;
  }
`;

export const Logo = styled.div`
  height: auto;
  width: 140px;
  margin-left: 80px;
  background-image: url(${logo});
  background-size: cover;
`;