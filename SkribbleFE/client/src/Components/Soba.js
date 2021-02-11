import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useParams} from "react-router-dom"

function Soba()
{    
    const [ connection, setConnection ] = useState(null);
    const [ chat, setChat ] = useState([]);
    const latestChat = useRef(null);
    const [ newPotez, setNewPotez ] = useState([]);
    
    const {sobaId}=useParams();
    console.log(sobaId);

    latestChat.current = chat;


    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44310/potezhub',{ accessTokenFactory: () => localStorage.getItem("loginToken")})
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(async (result) => {
                    console.log('Connected!');
                    connection.on('ReceiveMessage', message => {
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        setChat(updatedChat);
                    });
                    await connection.send("AddToGroup",sobaId).then(result=>{
                        console.log("RESULT "+result);
                    }).catch(excp=>{
                        console.log("Exception: "+excp);
                    })
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendMessage = async (user, message) => {
        const chatMessage = message;

        if (connection.connectionStarted) {
            try {
                const updatedChat = [...latestChat.current];
                updatedChat.push(message);
                setChat(updatedChat);
                await connection.send('SendMessage',sobaId,chatMessage);
            }
            catch(e) {
                console.log(e);
            }
        }
        else {
            alert('No connection to server yet.');
        }
    }


    return(
        <div>
            <h1>Potezi:</h1>
            <ul>
                {chat.map((el=>{
                    return <li>{el}</li>
                }))}
            </ul>
            <h1>Enter potez:</h1>
            <input type="text" onChange={(event)=>setNewPotez(event.currentTarget.value)}/>
            <button onClick={async ()=>{await sendMessage("proba",newPotez);}}>Send</button>
            <button onClick={()=>{console.log(chat);}}>Log</button>
        </div>
    );
}

export default Soba;