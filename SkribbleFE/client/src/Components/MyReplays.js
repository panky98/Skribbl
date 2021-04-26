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
    const [loading, setLoading] = useState(true);
    //let test=[];
  
     const handlePageChange = ( pageNumber ) => {
        console.log( `active page is ${ pageNumber }` );
        setCurrentPage( pageNumber )
         indexOfLastReplay  = activePage * replaysPerPage;
         indexOfFirstReplay = indexOfLastReplay - replaysPerPage;
         currentReplays     = data.slice( indexOfFirstReplay, indexOfLastReplay );
         renderReplays = currentReplays.map(t=>{
             <div><Link to={`/Replay/${t.id}`}>{t.naziv}</Link></div>
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
            let test=[];
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
             console.log(test);
             setData(test);
         }else if(response2.status==401){
            localStorage.removeItem("loginToken");
            window.location.replace("/LogIn");
          } 

        
            };

            
         }else if(response1.status==401){
            localStorage.removeItem("loginToken");
            window.location.replace("/LogIn");
          } 
        }
        catch (e) {
           
          } finally {

            setLoading(false);
            
          }
    }
    init();
     }, []);
     console.log(data);
  currentReplays     = data.slice( indexOfFirstReplay, indexOfLastReplay );
     renderReplays = currentReplays.map(t=>{
         <div><Link to={`/Replay/Replay/${t.id}`} >{t.naziv}</Link></div>});
   console.log(currentReplays);
    console.log(renderReplays);
    return( 
        
        <div>
                    <div>
         {loading && <Spinner/> }
</div>
            <div >
                {currentReplays.map(t=><div className="card"  ><Link to={`/Replay/${t.id}`}>{"Naziv:" +t.naziv}</Link></div>)}
            </div>
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