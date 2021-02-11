import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';


function Soba(props)
{    
    const [ connection, setConnection ] = useState(null);
    const [ chat, setChat ] = useState([]);
    const latestChat = useRef(null);
    const [ newPotez, setNewPotez ] = useState([]);

    latestChat.current = chat;


    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44310/potezhub')
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');
                    connection.on('ReceiveMessage', message => {
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        setChat(updatedChat);
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendMessage = async (user, message) => {
        const chatMessage = message;

        if (connection.connectionStarted) {
            try {
                await connection.send('SendMessage',"Soba1",chatMessage);
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
            <button onClick={async ()=>{await ConnectToTheGroup("Soba1");}}>Connect to the Group</button>
            <button onClick={()=>{console.log(chat);}}>Log</button>
        </div>
    );
    async function ConnectToTheGroup(groupName){
        await connection.invoke("AddToGroup","Soba1").then(result=>{
            console.log("RESULT "+result);
        }).catch(excp=>{
            console.log("Exception: "+excp);
        })
    }
}

export default Soba;