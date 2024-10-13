import React, { Component } from "react";
import styled, { css } from "styled-components";

function Index(props) {
    return (
        <Container>
            <Top4></Top4>
            <ScrollArea4></ScrollArea4>
            <Finished>Finished</Finished>
        </Container>
    );
}

const Container = styled.div`
    display: flex;
    background-color: rgba(184,188,192,1);
    margin-top: 6px;
    margin-bottom: 6px;
    margin-left: 6px;
    padding-right: 0px;
    margin-right: 5px;
    border-radius: 10px;
    flex-direction: column;
    width: 269px;
    height: 687px;
    position: relative;
`;

const Top4 = styled.div`
    flex: 0.09000000000000002 1 0%;
    background-color: rgba(97,98,101,1);
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    display: flex;
    flex-direction: column;
`;

const ScrollArea4 = styled.div`
    overflow-y: scroll;
    flex: 0.9099999999999999 1 0%;
    background-color: rgba(184,188,192,1);
    border-bottom-right-radius: 10px;
    border-bottom-left-radius: 10px;
    display: flex;
    flex-direction: column;
`;

const Finished = styled.span`
  font-family: Roboto;
  top: 11px;
  left: 72px;
  position: absolute;
  font-style: normal;
  font-weight: 400;
  color: rgba(218,220,223,1);
  font-size: 34px;
`;

export default Index;
