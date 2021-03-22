import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useParams} from "react-router-dom"
import {ChromePicker} from 'react-color';
import hexRgb from 'hex-rgb';
import useFetch from "../Services/useFetch";
import Spinner from "./Spinner";
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
    const [usersInRoomPoints,setUsersInRoomPoints]=useState([]);
    const usersInRoomPointsRef=useState();
    const {sobaId}=useParams();

    const {data:trenutnaSoba, loading, error}=useFetch("Soba/getOneSoba/"+sobaId.slice(4));
    console.log(trenutnaSoba);

    //
    const canvasRef=useRef(null);
    const contextRef=useRef(null);
    const [isDrawing, setIsDrawing]=useState(false);
    const [color, setColor]=useState('#ffffff');
    const firstDrawing=useRef(true);


    
    
    console.log(sobaId);
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

                        const niz=[];
                        message.forEach((el)=>{
                            niz.push(0);
                        });
                        
                        usersInRoomPointsRef.current=niz;
                        setUsersInRoomPoints(usersInRoomPointsRef.current);

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
                        
                        const usersInRoomPointsUpdate=[...usersInRoomPointsRef.current];
                        usersInRoomPointsUpdate.push(0);
                        setUsersInRoomPoints(usersInRoomPointsUpdate);
                        usersInRoomPointsRef.current=usersInRoomPointsUpdate;
                    });
                    connection.on('UserOut', message => {
                        //korisnik napustio sobu
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                        setChat(updatedChat);

                        const updatedListOfUsers=[...usersInRoomRef.current];
                        let indeks=updatedListOfUsers.indexOf(message.split(" ")[0]);
                        updatedListOfUsers.splice(indeks,1);
                        setUsersInRoom(updatedListOfUsers);
                        setCountUsersInRoom(updatedListOfUsers.length);

                        const usersInRoomPointsUpdate=[...usersInRoomPointsRef.current];
                        usersInRoomPointsUpdate.splice(indeks,1);
                        setUsersInRoomPoints(usersInRoomPointsUpdate);
                        usersInRoomPointsRef.current=usersInRoomPointsUpdate;
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
                    connection.on('SaveReplay', message => {
                        //poruka o cuvanju replaya, sam tekst poruke je id toka igre za koji se pita da li se cuva:
                        if (window.confirm('Are you want to save replay from previous round of game?')) {
                            fetch("https://localhost:44310/TokIgrePoKorisniku/createTokIgrePoKorisniku",{
                                method:"POST",
                                headers:{"Content-Type":"application/json",
                                         "Authorization":"Bearer "+localStorage.getItem("loginToken")
                                        },
                                body:JSON.stringify({"tokIgre":parseInt(message),"Korisnik":parseInt(-1)})
                            }).then(p=>{
                                if(p.ok){
                                    alert("Replay succesfly saved");
                                }
                                else
                                {
                                    console.log("Replay isn't saved : "+p.status);

                                }
                            }).catch(exc=>{
                                console.log("Replay isn't saved : "+exc);
                            })
                          } else {
                            // Do nothing!
                          }
                    });
                    connection.on('ReceivePotez', message => {
                       

                        if(message==='stop')
                        {
                            console.log("Stigo stop");
                            firstDrawing.current=true;
                            console.log("PRE")
                            contextRef.current.closePath();
                            console.log("POSLE");
                            console.log(firstDrawing.current + " je sad")
                        }
                        else
                        {
                            const info = message.split(" ");
                            const color=info[0];
                            const offsetX=info[1];
                            const offsetY=info[2];
                            console.log(color);
                            contextRef.current.strokeStyle=color;
                            //contextRef.current.strokeStyle="black";
                            if(firstDrawing.current===true)
                            {
                                console.log("Prvo crtanje " + offsetX + " " + offsetY);
                                contextRef.current.beginPath();
                                contextRef.current.moveTo(offsetX, offsetY);
                                firstDrawing.current=false;
                            }
                            else
                            {
                                console.log("Drugo crtanje " + offsetX + " " + offsetY);
                                contextRef.current.lineTo(offsetX, offsetY);
                                contextRef.current.stroke();
                            }
                        }

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
                    connection.on('UpdatePoints', message => {
                        const updateListOfPoint=[...usersInRoomPointsRef.current];
                        let indeks=usersInRoomRef.current.indexOf(message.split(" ")[0]);
                        updateListOfPoint[indeks]=message.split(" ")[1];

                        console.log("UpdateListOfPoint: "+updateListOfPoint);
                        setUsersInRoomPoints(updateListOfPoint);
                        usersInRoomPointsRef.current=updateListOfPoint;
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

    useEffect(()=>{
        
        const canvas=canvasRef.current;

        
        canvas.width=window.innerWidth * 2;
        canvas.height=window.innerHeight * 2;


        canvas.style.width=`${window.innerWidth}px`;
        canvas.style.height=`${window.innerHeight}px`;

        //canvas.style.width=`${600}px`;
        //canvas.style.height=`${600}px`;

        const context=canvas.getContext("2d");

        context.scale(2,2);
        context.lineCap="round";
        //context.strokeStyle="black";
        context.strokeStyle=hexToRGB(color);
        context.lineWidth=5;
        contextRef.current=context;
    }, [])

    const startDrawing = ({nativeEvent})=>{
        //const {offsetX, offsetY} = nativeEvent;
        const offsetX=nativeEvent.layerX;
        const offsetY=nativeEvent.layerY;
        contextRef.current.beginPath();
        contextRef.current.moveTo(offsetX, offsetY);
        setIsDrawing(true);
    }

    const finishDrawing =async  ()=>{
        contextRef.current.closePath();
        setIsDrawing(false);

        if(connection.connectionStarted){
            try{
                await connection.send('SendPotez', sobaId,'stop');
                
            }catch(e){
                console.log(e);
            }
        }
    }

     const draw = async ({nativeEvent})=>{
        if(!isDrawing)
            return;

        //const {offsetX, offsetY} =nativeEvent;
        const offsetX=nativeEvent.layerX;
        const offsetY=nativeEvent.layerY;
        if(offsetX >600 || offsetY >600)
            return;
        contextRef.current.lineTo(offsetX, offsetY);
        contextRef.current.stroke();

        if(connection.connectionStarted){
            try{
                await connection.send('SendPotez', sobaId,`${color} ${offsetX} ${offsetY}`);
                //console.log("Poslao sam" + `${color} ${offsetX} ${offsetY}`)
            }catch(e){
                console.log(e);
            }
        }
    }

    const hexToRGB =(hex)=>{
        const rgbObj=hexRgb(color);
        return `rgb(${rgbObj.red},${rgbObj.green},${rgbObj.blue} )`
      }

    const handleChangeColor = (updatedColor)=>{
        setColor(updatedColor.hex);
        contextRef.current.strokeStyle=hexToRGB(color);
    }

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
                {usersInRoom.map((el,indeks)=>{
                    return <li>{el} {usersInRoomPoints[indeks]}p</li>
                })}
            </ul>
            <div>
                <ChromePicker
                color={color}
                disableAlpha={true}
                onChange={(updatedColor)=>handleChangeColor(updatedColor)}
                />
            </div>
            <div className="kontejnerZaCrtanje">
                <canvas
                className="canvasZaCrtanje"
                onMouseDown={startDrawing}
                onMouseUp={finishDrawing}
                onMouseMove={draw}
                width={`${600}px`}
                height={`${600}px`}
                ref={canvasRef}
                />
            </div>
        </div>
    );

    //TODO Hardokiran izbor reci i dautm, treba da se izmeni da nije hardkodirano
    function startGame()
    {
        var kategorijaId=trenutnaSoba.kategorija.id;
        latestHost.current=false;
        setAmHost(false);
        fetch("https://localhost:44310/Rec/getThreeWordsFromCategory/"+kategorijaId+"/"+sobaId,{
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
        var date=new Date().toISOString();
        console.log(date);
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