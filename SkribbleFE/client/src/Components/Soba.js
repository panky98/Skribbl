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
    const [showWordList, setShowWordList]=useState(false);
    const [wordList, setWordList]=useState([]);
    const [chosenWordId,setChosenWordId]=useState();
    const [chosenWord,setChosenWord]=useState();
    const [showChosenWord,setShowChosenWord]=useState();
    const chosenWordRef=useRef();
    const chosenWordIdRef=useRef();
    const [usersInRoom,setUsersInRoom]=useState(["You"]);
    const usersInRoomRef=useRef();
    const [countUsersInRoom,setCountUsersInRoom]=useState(1);



    
    const {sobaId}=useParams();

    latestChat.current = chat;
    usersInRoomRef.current=usersInRoom;


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
                    connection.on('OnConnectUsers', message => {  
                        //pribavljanje na pocetku korisnika koji su trenutno u sobi
                        usersInRoomRef.current=message;
                        setUsersInRoom(usersInRoomRef.current);
                        setCountUsersInRoom(usersInRoomRef.current.length);
                    });
                    connection.on('UserIn', message => {
                        //korisnik se prikljucio sobi
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        setChat(updatedChat);
                        const updatedListOfUsers=[...usersInRoomRef.current];
                        updatedListOfUsers.push(message.split(" ")[0]);
                        setUsersInRoom(updatedListOfUsers);
                        setCountUsersInRoom(updatedListOfUsers.length);
                    });
                    connection.on('UserOut', message => {
                        //korisnik napustio sobu
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        setChat(updatedChat);

                        const updatedListOfUsers=[...usersInRoomRef.current];
                        updatedListOfUsers.splice(updatedListOfUsers.indexOf(message.split(" ")[0]),1);
                        setUsersInRoom(updatedListOfUsers);
                        setCountUsersInRoom(updatedListOfUsers.length);
                    });
                    connection.on('GussedWord', message => {
                        //pogodjena rec
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        alert("You have guessed the word! Congratulations");
                        setChat(updatedChat);
                    });
                    connection.on('FinishedGame', message => {
                        //kraj igre, prosle sve runde
                        alert("Game has finished!");
                    });
                    connection.on('Timer', message => {
                        //primanje tajmera u rundi
                        console.log(message);
                        setRemainingTime(message);
                    });
                    connection.on('ReceivePotez', message => {
                        //TODO obrada kad se primi iscrtani parametar od hosta
                        //da se iscrta od strane svih ostalih koji pogadjaju
                    });
                    connection.on('YourTurn', message => {
                        //obavestenje igracu koji je sada na redu da objasnjava rec
                        alert("Your turn, press start and chose WORD!")
                    });
                    connection.on('SwitchedTurn', message => {
                        //obavestenje svim ostalima ko je sada na potezu!
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        setChat(updatedChat);
                        setAmHost(false);
                        setShowChosenWord(false);
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
            <div>
                {showWordList && <div><select onChange={(ev)=>{chosenWordIdRef.current=ev.currentTarget.value.split(" ")[0];setChosenWordId(chosenWordIdRef.current);setShowWordList(false);setShowChosenWord(true);chosenWordRef.current=ev.currentTarget.value.split(" ")[1];setChosenWord(chosenWordRef.current);continueStartGame();}}>
                                    {wordList.map(el=>{
                                        return <option value={el.id+" "+el.naziv}>{el.naziv}</option>
                                    })}
                                    </select>
                                </div>}
                {showChosenWord &&<h1>{chosenWord}</h1>}
            </div>
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
            <label style={{color:"red"}}>Remaining time {remainingTime}s</label><br/>
            <label>Users in room: {countUsersInRoom}/4</label>
            <ul>
                {usersInRoom.map(el=>{
                    return <li>{el}</li>
                })}
            </ul>
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
                    setWordList(data);
                    setShowWordList(true);
                })
            }
        })
    }

    function continueStartGame()
    {
        fetch("https://localhost:44310/TokIgre/createTokIgre",{
            method:"POST",
            headers:{"Content-Type":"application/json"},
            body:JSON.stringify({"pocetakIgre":"2020-12-12T00:00:00","recZaPogadjanjeId":parseInt(chosenWordIdRef.current),"sobaId":parseInt(sobaId.slice(4,sobaId.length))})
        }).then(p=>{
            if(p.ok)
            {
                p.json().then(async tokIgreId=>{
                    //sacuvati trenutni idToka igre u redisu
                    await connection.send("SaveNewTokIgreId",sobaId,tokIgreId,chosenWordRef.current).then(result=>{
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
    }
}

export default Soba;