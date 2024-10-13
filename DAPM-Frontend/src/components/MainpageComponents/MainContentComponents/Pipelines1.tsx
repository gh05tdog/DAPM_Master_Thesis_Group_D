import React, { Component } from "react";
import styled, { css } from "styled-components";

function Index(props) {
    return (
        <Container>
            <Top1></Top1>
            <Build>Build</Build>
            <Bottom1>
                <ScrollArea></ScrollArea>
                <Group>
                    <Button2>
                        <ButtonOverlay></ButtonOverlay>
                    </Button2>
                    <Newpipelinebutton>
                        <ButtonOverlay>
                            <NewPipeline>New pipeline</NewPipeline>
                        </ButtonOverlay>
                    </Newpipelinebutton>
                </Group>
            </Bottom1>
        </Container>
    );
}

const Container = styled.div`
    display: flex;
    background-color: rgba(184,188,192,1);
    margin-top: 6px;
    margin-bottom: 6px;
    margin-left: 6px;
    border-radius: 10px;
    flex-direction: column;
    width: 269px;
    height: 687px;
    position: relative;
`;

const ButtonOverlay = styled.button`
    display: block;
    background: none;
    height: 100%;
    width: 100%;
    border:none
`;
const Top1 = styled.div`
    flex: 0.09000000000000002 1 0%;
    background-color: rgba(97,98,101,1);
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    display: flex;
    flex-direction: column;
`;

const Build = styled.span`
    font-family: Roboto;
    top: 11px;
    left: 96px;
    position: absolute;
    font-style: normal;
    font-weight: 400;
    color: rgba(218,220,223,1);
    font-size: 34px;
`;

const Bottom1 = styled.div`
    flex: 0.9099999999999999 1 0%;
    background-color: rgba(184,188,192,1);
    border-bottom-right-radius: 10px;
    border-bottom-left-radius: 10px;
    flex-direction: column;
    display: flex;
`;

const ScrollArea = styled.div`
    overflow-y: scroll;
    flex: 0.89 1 0%;
    background-color: rgba(184,188,192,1);
    display: flex;
    flex-direction: column;
`;

const Group = styled.div`
    flex: 0.10999999999999997 1 0%;
    flex-direction: column;
    position: relative;
    display: flex;
`;

const Button2 = styled.div`
    flex: 1 1 0%;
    background-color: rgba(184,188,192,1);
    border-bottom-right-radius: 10px;
    border-bottom-left-radius: 10px;
    border: none;
    display: flex;
    flex-direction: column;
`;

const Newpipelinebutton = styled.div`
    top: 13px;
    left: 18px;
    width: 232px;
    height: 49px;
    position: absolute;
    background-color: rgba(234,234,237,1);
    border-radius: 10px;
    flex-direction: column;
    display: flex;
    border: none;
`;

const NewPipeline = styled.span`
    font-family: Roboto;
    font-style: normal;
    font-weight: 400;
    color: #121212;
    font-size: 28px;
`;

export default Index;
