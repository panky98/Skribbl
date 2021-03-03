import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useParams} from "react-router-dom"

function Soba()
{    
    const [ connection, setConnection ] = useState(null);
    const [ chat, setChat ] = useState([]);
    const latestChat = useRef(null);
    const [ newPotez, setNewPotez ] = useState([]);
    const [ amHost, setAmHost ] = useState(false);
    const latestHost = useRef(false);
    const [remainingTime,setRemainingTime]=useState(30);

    
    const {sobaId}=useParams();

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
                        if(message==="HostMessage")
                        {
                            latestHost.current=true;
                            setAmHost(true);
                        }
                        setChat(updatedChat);
                    });
                    connection.on('GussedWord', message => {
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        alert("You have guessed the word! Congratulations");
                        setChat(updatedChat);
                    });
                    connection.on('FinishedGame', message => {
                        alert("Game has finished!");
                    });
                    connection.on('Timer', message => {
                        console.log(message);
                        setRemainingTime(message);
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


    useEffect(()=>{
        return async function cleanup() {
            if(connection){
                await connection.send("RemoveFromGroup",sobaId,latestHost.current).then(result=>{
                    console.log("RESULT "+result);
                }).catch(excp=>{
                    console.log("Exception: "+excp);
                })
            }
          };    
    },[connection]);

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
            <h1>Enter message:</h1>
            <input type="text" onChange={(event)=>setNewPotez(event.currentTarget.value)}/>
            <button onClick={async ()=>{await sendMessage("proba",newPotez);}}>Send</button>
            <button onClick={()=>{console.log(amHost);}}>Log</button><br/>
            {amHost && <div><button onClick={()=>startGame()}>Start</button></div>}
            <label style={{color:"red"}}>Remaining time {remainingTime}s</label>
        </div>
    );

    //TODO Hardokiran izbor reci i dautm, treba da se izmeni da nije hardkodirano
    function startGame()
    {
        latestHost.current=false;
        setAmHost(false);
        fetch("https://localhost:44310/Rec/getThreeWordsFromCategory/9",{
            method:"GET"
        }).then(p=>{
            if(p.ok)
            {
                p.json().then(data=>{
                  fetch("https://localhost:44310/TokIgre/createTokIgre",{
                      method:"POST",
                      headers:{"Content-Type":"application/json"},
                      body:JSON.stringify({"pocetakIgre":"2020-12-12T00:00:00","recZaPogadjanjeId":data[0].id,"sobaId":parseInt(sobaId.slice(4,sobaId.length))})
                  }).then(p=>{
                      if(p.ok)
                      {
                          p.json().then(async tokIgreId=>{
                              //sacuvati trenutni idToka igre u redisu
                              await connection.send("SaveNewTokIgreId",sobaId,tokIgreId,data[0].naziv).then(result=>{
                                console.log("RESULT "+result);
                            }).catch(excp=>{
                                console.log("Exception: "+excp);
                            });
                            await fetch("https://localhost:44310/TokIgre/startTokIgre/"+sobaId,{
                                method:"POST"
                            }); 
                          })
                      }
                  })
                })
            }
        })
    }
}

export default Soba;