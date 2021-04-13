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
    const {data:tokIgre, loading, error}=useFetch("TokIgrePoKorisniku/getTokIgrePoKorisnikuZaKorisnika/"+userId);
    const replaysPerPage = 5;
    const [ activePage, setCurrentPage ] = useState( 1 );

    const indexOfLastReplay  = activePage * replaysPerPage;
    const indexOfFirstReplay = indexOfLastReplay - replaysPerPage;
    let currentReplays;
    let renderReplays;
    console.log(tokIgre);
  
     const handlePageChange = ( pageNumber ) => {
        console.log( `active page is ${ pageNumber }` );
        setCurrentPage( pageNumber )
     };

    if(error) throw error;
    if(loading) return <Spinner/>
    currentReplays     = tokIgre.slice( indexOfFirstReplay, indexOfLastReplay );
    renderReplays = currentReplays.map(t=>{
       return(
           <li className="list-group-item" key={t.id}><Link to={`/Replay/${t.tokIgre}`}>{t.tokIgre}</Link></li>
   )});
    console.log(tokIgre);   
    return(
        
        <div>
            <ul className="list-group">
                {renderReplays}
            </ul>
            <div className="pagination">
            <Pagination
               activePage={ activePage }
               itemsCountPerPage={ 5 }
               totalItemsCount={ tokIgre.length }
               pageRangeDisplayed={ 3 }
               onChange={ handlePageChange }
            />
         </div>
        </div>
    );
       
}

export default MyReplays;