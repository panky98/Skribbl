import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useParams} from "react-router-dom"
import {ChromePicker} from 'react-color';
import hexRgb from 'hex-rgb';
import useFetch from "../Services/useFetch";
import Spinner from "./Spinner";
import TextField from "@material-ui/core/TextField";
import {AiFillPlayCircle} from "react-icons/ai";
function Replay()
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
    const {tokIgreId}=useParams();
    const {data:potezi, loading, error}=useFetch("Potez/getAllPotezByTokIgre/"+tokIgreId);
    console.log(potezi);
    const canvasRef=useRef(null);
    const contextRef=useRef(null);
    const [isDrawing, setIsDrawing]=useState(false);
    const [color, setColor]=useState('#ffffff');
    const firstDrawing=useRef(true);
    let text=[];
    latestChat.current = chat;
    usersInRoomRef.current=usersInRoom;
    const [messages,setMessages]=useState("");
    let messagesT="";

    useEffect(()=>{
        
       /*  const canvas=canvasRef.current;

        
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
        context.lineWidth=5;
        contextRef.current=context; */
    }, [])
    if(error) throw error;
    if(loading) return <Spinner/>
    const startDrawing = ()=>{
        let canvas=canvasRef.current;
        let context=canvas.getContext("2d");
        contextRef.current=context; 
        
       // context.scale(2,2);
        context.lineCap="round";
        //context.strokeStyle="#FF0000";
        console.log(potezi);
        context.lineWidth=5;
        contextRef.current.beginPath();
        let start=potezi[1];
        let startTime=potezi[0].vremePoteza;
        let startPoint=potezi[1];
        let moving=0;
        for(let i=2;i<potezi.length;i++){
            let j=i-1;
            while(j>=0 &&(potezi[j].parametarLinije==null||potezi[j].parametarLinije=="stop"))
            {
                j--;
            }

            if(j<0)
            {
                j=i;
                while(j<potezi.length &&(potezi[j].parametarLinije==null||potezi[j].parametarLinije=="stop"))
                {
                    j++;
                }
                start=potezi[j];
            }
            else{
                start=potezi[j];
            }

            

            if(potezi[i].parametarLinije==null)
            {
                contextRef.current.beginPath();
                var node = document.createElement("LI");                 
                setTimeout(function(){
                    var node1 = document.createElement("LI");   
                    const sec = Math.floor((potezi[i].vremePoteza-startTime)/1000);              
                messagesT= messagesT+"\n"+sec+ ": "+potezi[i].tekstPoruke;
                setMessages(messagesT);            
                if(i==potezi.length-1)
                {
                    messagesT= messagesT+"\n"+"The end";
                    setMessages(messagesT);
                }
}, potezi[i].vremePoteza-startTime); 
                continue;                        
                
            }
            setTimeout(function(){
             if(potezi[i].parametarLinije=="stop")
                {
                    contextRef.current.beginPath();
                    console.log("pomeranje");
                    moving=1;

                }
                else{
                
               
                   
                        var parameters=potezi[i].parametarLinije.split(" ");
                        var parametersStart=start.parametarLinije.split(" ");

                        
                        const offsetX=parseInt(parameters[1]);
                        const offsetY=parseInt(parameters[2]);
                        console.log(parameters[0]);

                        context.strokeStyle=parameters[0];
                        console.log(context.strokeStyle);
                        const offsetXold=parametersStart[1];
                        const offsetYold=parametersStart[2];
                        if(moving==1)
                        {
                            contextRef.current.moveTo(offsetX,offsetY);
                            console.log("Pomeram se");
                            moving=0;
                        }
                        else{

                        contextRef.current.lineTo(offsetX, offsetY); 
                        contextRef.current.stroke();
                        }
                       
                    }
                    if(i==potezi.length-1)
                    {
                        messagesT= messagesT+"\n"+"The end";
                        setMessages(messagesT);
                    }
                     },  potezi[i].vremePoteza-startTime);
                        
            /* setTimeout(function(){
                contextRef.current.lineTo(offsetX, offsetY); 
            contextRef.current.stroke(); }, 3);   */    
               

        }

        //const {offsetX, offsetY} = nativeEvent;

    }

    const finishDrawing =async  ()=>{
    }

     const draw = async ({nativeEvent})=>{
    
    }

    const hexToRGB =(hex)=>{
        const rgbObj=hexRgb(color);
        return `rgb(${rgbObj.red},${rgbObj.green},${rgbObj.blue} )`
      }

    
    return(
        <div>
            <div>
            </div>
            <h1>Chat:</h1>  
            <TextField
          id="outlined-multiline-static"
          multiline
          rows={6}
          defaultValue={messages}
          variant="outlined"
          disabled
        />        

            <button onClick={startDrawing} style={{width:"100px"}}><AiFillPlayCircle/>Play</button>
            <div className="kontejnerZaCrtanje">
               
                <canvas
                className="canvasZaCrtanje"
                width={`${600}px`}
                height={`${600}px`}
                
                ref={canvasRef}
                />
            </div>
        </div>
    );
       
}

export default Replay;