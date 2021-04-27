import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useParams} from "react-router-dom"
import {ChromePicker} from 'react-color';
import hexRgb from 'hex-rgb';
import useFetch from "../Services/useFetch";
import Spinner from "./Spinner";
import Dialog from "rc-dialog";
import "rc-dialog/assets/index.css";
import TextField from "@material-ui/core/TextField";
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
    const [canDraw,setCanDraw]=useState(false);
    const [dialogVisible,setdialogVisibility]=useState(false);
    const[dialogTitle,setDialogTitle]=useState("");
    const[hasButtons,setHasButtons]=useState(false);
    const[buttonValue,setbuttonValue]=useState(false);
    const [replayParameter,setReplayParameter]=useState("test");
    const {data:trenutnaSoba, loading, error}=useFetch("Soba/getOneSoba/"+sobaId.slice(4));
    console.log(trenutnaSoba);
    const latestMessage = useRef(null);
    const [messages,setMessages]=useState("");
    let messagesT="";
    //
    const canvasRef=useRef(null);
    const contextRef=useRef(null);
    const [isDrawing, setIsDrawing]=useState(false);
    const [color, setColor]=useState('#000000');
    const firstDrawing=useRef(true);


    
    
    console.log(sobaId);
    latestChat.current = chat;
    latestMessage.current=messages;
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
                        messagesT=[latestMessage.current];
                        messagesT=messagesT+"\n"+message;
                        setMessages(messagesT);
                        updatedChat.push("\n");
                        if(message==="HostMessage")
                        {
                               latestHost.current=true;
                            setAmHost(true);
                            setCanDraw(true);
                            var canvas = document.getElementById("canvas");
                            var ctx = canvas.getContext("2d");
                            ctx.clearRect(0, 0, canvas.width, canvas.height); 
                        ctx.beginPath();         
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
                        messagesT=[latestMessage.current];
                        messagesT=messagesT+"\n"+message;
                        setMessages(messagesT);
                        updatedChat.push(message);
                        updatedChat.push("\n");
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
                        messagesT=[latestMessage.current];
                        messagesT=messagesT+"\n"+message;
                        setMessages(messagesT);
                        updatedChat.push(message);
                        updatedChat.push("\n");
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
                        const updatedChat = [...latestChat.current];
                        messagesT=[latestMessage.current];
                        messagesT=messagesT+"\n"+message;
                        setMessages(messagesT);
                        updatedChat.push(message);
                        updatedChat.push("\n");
                        setDialogTitle("Congratulations, you have guessed the word!");
                        setdialogVisibility(true); 
                        setChat(updatedChat);
                    });
                    connection.on('FinishedGame', message => {
                        //kraj igre, prosle sve runde
                        setDialogTitle("Game has finished! Do you want to save the replay of the previous round?");
                                    setdialogVisibility(true); 
                    });
                    connection.on('Timer', message => {
                        //primanje tajmera u rundi
                        console.log(message);
                        setRemainingTime(message);
                    });
                    connection.on('SaveReplay', message => {   
                        setReplayParameter(message);                    
                        setHasButtons(true);
                        setDialogTitle("Do you want to save the replay of the previous round?");
                        setdialogVisibility(true);                   
                        if (buttonValue==true) {

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
                        setDialogTitle("Your turn, press start and choose a word!Do you want to save the replay of the previous round?");
                                    setdialogVisibility(true); 
                    });
                    connection.on('SwitchedTurn', message => {
                        //obavestenje svim ostalima ko je sada na potezu!
                        const updatedChat = [...latestChat.current];
                        messagesT=[latestMessage.current];
                        messagesT=messagesT+"\n"+message;
                        setMessages(messagesT);
                        updatedChat.push(message);
                        updatedChat.push("\n");
                        setChat(updatedChat);
                        setAmHost(false);
                        setCanDraw(false);
                        var canvas = document.getElementById("canvas");
                        var ctx = canvas.getContext("2d");
                        ctx.clearRect(0, 0, canvas.width, canvas.height); 
                        ctx.beginPath();         
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
         if(!canDraw)
            return;
        if(!isDrawing)
            return;

        //const {offsetX, offsetY} =nativeEvent;
        const offsetX=nativeEvent.layerX;
        const offsetY=nativeEvent.layerY;
        console.log(offsetX,offsetY);
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
                messagesT=[latestMessage.current];
                messagesT=messagesT+"\n"+message;
                setMessages(messagesT);
                updatedChat.push(message);
                updatedChat.push("\n");
                setChat(updatedChat);
                await connection.send('SendMessage',sobaId,chatMessage);
            }
            catch(e) {
                console.log(e);
            }
        }
        else {
            setDialogTitle("No connection to the server.");
            setdialogVisibility(true); 
        }
    }


    return(
        <div>
            <div>
                {showWordList && <div><select onChange={(ev)=>{chosenWordIdRef.current=ev.currentTarget.value.split(" ")[0];setChosenWordId(chosenWordIdRef.current);setShowWordList(false);setShowChosenWord(true);chosenWordRef.current=ev.currentTarget.value.split(" ")[1];setChosenWord(chosenWordRef.current);continueStartGame();}}>
                                    <option value="-1">Chose word</option>
                                    {wordList.map(el=>{
                                        return <option value={el.id+" "+el.naziv}>{el.naziv}</option>
                                    })}
                                    </select>
                                </div>}
                {showChosenWord &&<h1>{chosenWord}</h1>}
            </div>
           
                    <Dialog visible={dialogVisible} title={dialogTitle}>
                    { !hasButtons&&<button onClick={()=>{setbuttonValue(true);
                         setdialogVisibility(false);
                         setHasButtons(false);}}>OK</button>}
                       { hasButtons&&<button onClick={()=>{setbuttonValue(true);

                        console.log(replayParameter) ;  
                         fetch("https://localhost:44310/TokIgrePoKorisniku/createTokIgrePoKorisniku",{
                            method:"POST",
                            headers:{"Content-Type":"application/json",
                                     "Authorization":"Bearer "+localStorage.getItem("loginToken")
                                    },
                            body:JSON.stringify({"tokIgre":parseInt(replayParameter),"Korisnik":parseInt(-1)})
                        }).then(p=>{
                            if(p.ok){
                                setdialogVisibility(false); 
                                setDialogTitle("Replay successfully saved!");
                                setdialogVisibility(true); 
                            }
                            else if(p.status==401)
                            {
                                localStorage.removeItem("loginToken");
                                window.location.replace("/LogIn");
                            }
                            else
                            {
                                console.log("Replay isn't saved : "+p.status);

                            }
                        }).catch(exc=>{
                            console.log("Replay isn't saved : "+exc);
                        });
                        setdialogVisibility(false);
                        setHasButtons(false); }}>Yes</button>}

                       { hasButtons&&<button onClick={()=>{setbuttonValue(false);
                         setdialogVisibility(false);
                         setHasButtons(false);}}>No</button>}
                    </Dialog>
          <div className="mainDiv">
              <div className="childDiv">
             
                <ChromePicker
                color={color}
                disableAlpha={true}
                onChange={(updatedColor)=>handleChangeColor(updatedColor)}
                />
           
         <br/>
         <TextField
          id="outlined-multiline-static"
          multiline
          rows={10}
          defaultValue={messages}
          variant="outlined"
          disabled
        />
        <br/>
            <input type="text" onChange={(event)=>setNewPotez(event.currentTarget.value)}/>
            <button onClick={async ()=>{await sendMessage("proba",newPotez);}}>Send</button>
            <br/>
            {amHost && <div><button onClick={()=>startGame()}>Start</button></div>}
            <h5 style={{color:"red"}}>Remaining time {remainingTime}s</h5><br/>
            <h5>Users in room: {countUsersInRoom}/4</h5>
            <ul>
                {usersInRoom.map((el,indeks)=>{
                    return <li>{el} {usersInRoomPoints[indeks]}p</li>
                })}
            </ul>
            <br/> <br/> <br/> <br/> <br/> <br/> <br/>
            </div> 
            <div className="childDiv">
            <div className="kontejnerZaCrtanje">
                <canvas
                className="canvasZaCrtanje"
                onMouseDown={startDrawing}
                onMouseUp={finishDrawing}
                id="canvas"
                onMouseMove={draw}
                width={`${600}px`}
                height={`${600}px`}
                ref={canvasRef}
                />
            </div>
            </div> 
            </div>
        </div>
    );

    //TODO Hardokiran izbor reci i dautm, treba da se izmeni da nije hardkodirano
    function startGame()
    {
        var kategorijaId=trenutnaSoba.kategorija.id;
        latestHost.current=false;
        setAmHost(false);
        var canvas = document.getElementById("canvas");
                        var ctx = canvas.getContext("2d");
                        ctx.clearRect(0, 0, canvas.width, canvas.height); 
        ctx.beginPath();         
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
            body:JSON.stringify({"pocetakIgre":date,"naziv":""+date+" "+sobaId,"recZaPogadjanjeId":parseInt(chosenWordIdRef.current),"sobaId":parseInt(sobaId.slice(4,sobaId.length))})
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