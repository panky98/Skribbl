import React, { useState,useEffect,useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import {useParams} from "react-router-dom"
import {ChromePicker} from 'react-color';
import hexRgb from 'hex-rgb';
import useFetch from "../Services/useFetch";
import Spinner from "./Spinner";
import {Link} from 'react-router-dom'
import Pagination from "react-js-pagination";
function MyReplays()
{    
    const {userId}=useParams();
    //const {data:tokIgre, loading, error}=useFetch("TokIgrePoKorisniku/getTokIgrePoKorisnikuZaKorisnika/"+userId);
    const replaysPerPage = 5;
    const [ activePage, setCurrentPage ] = useState( 1 );
    const [data, setData] = useState([]);
    let indexOfLastReplay  = activePage * replaysPerPage;
    let indexOfFirstReplay = indexOfLastReplay - replaysPerPage;
    let currentReplays;
    let renderReplays;
    let test=[];
  
     const handlePageChange = ( pageNumber ) => {
        console.log( `active page is ${ pageNumber }` );
        setCurrentPage( pageNumber )
         indexOfLastReplay  = activePage * replaysPerPage;
         indexOfFirstReplay = indexOfLastReplay - replaysPerPage;
         currentReplays     = data.slice( indexOfFirstReplay, indexOfLastReplay );
         renderReplays = currentReplays.map(t=>{
             <li><Link to={`/Replay/${t.id}`} className="resultElement">{t.naziv}</Link></li>
        });
     };

   // if(error) throw error;
    //if(loading) return <Spinner/>

    useEffect( async () => {
        async function init(){
            try {
        const response1=await fetch("https://localhost:44310/TokIgrePoKorisniku/getTokIgrePoKorisnikuZaKorisnika/"+userId,{
            method:"GET",
            headers:{"Content-Type":"application/json",
            "Authorization":"Bearer "+localStorage.getItem("loginToken")
           },
         });
         if (response1.ok) {
            const json = await response1.json();
            for(let i=0;i<json.length;i++){
                const response2=await fetch("https://localhost:44310/TokIgre/getOneTokIgre/"+json[i].tokIgre,{
            method:"GET",
            headers:{"Content-Type":"application/json",
            "Authorization":"Bearer "+localStorage.getItem("loginToken")
           },
         });
         if(response2.ok)
         {
             const jsonResponse= await response2.json();
             test.push(jsonResponse);
             renderReplays.push( <li key={jsonResponse.id}><Link to={`/Replay/${jsonResponse.id}`}>{jsonResponse.naziv}</Link> </li>)
             setData(test);
         }else if(response2.status==401){
            localStorage.removeItem("loginToken");
            window.location.replace("/LogIn");
          } 

          currentReplays     = data.slice( indexOfFirstReplay, indexOfLastReplay );
     renderReplays = currentReplays.map(t=>{
         <li><Link to={`/Replay/${t.id}`} className="resultElement">{t.naziv}</Link></li>});
            };

            
         }else if(response1.status==401){
            localStorage.removeItem("loginToken");
            window.location.replace("/LogIn");
          } 
        }
        catch (e) {
           
          } finally {
            
          }
    }
    init();
     }, []);
     console.log(data);

     currentReplays     = data.slice( indexOfFirstReplay, indexOfLastReplay );
     
     renderReplays = currentReplays.map(t=>{
         <li><Link to={`/Replay/${t.id}`} className="resultElement">{t.naziv}</Link></li>});

   console.log(currentReplays);
    console.log(renderReplays);
    return( 
        
        <div>
            <ul className="list-group">
                {data.length!=0&&renderReplays}
            </ul>
            <div className="pagination">
            <Pagination
               activePage={ activePage }
               itemsCountPerPage={ 5 }
               totalItemsCount={ data.length }
               pageRangeDisplayed={ 3 }
               onChange={ handlePageChange }
            />
         </div>
        </div>
    );
       
}

export default MyReplays;