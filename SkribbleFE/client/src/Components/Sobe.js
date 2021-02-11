import React, { useState,useEffect,useRef } from 'react';
import useFetch from "../Services/useFetch"
import Spinner from "./Spinner"
import {Link} from 'react-router-dom'
import CreateSobaForm from './CreateSobaForm';

function Sobe()
{
    const[showForm,setShowForm]=useState(false);
    const {data:sobe, loading, error}=useFetch("Soba/getAllSoba");
    if(error) throw error;
    if(loading) return <Spinner/>
    console.log(sobe);
    return(
        <div>
            <ul>
            {sobe.map(el=>{
                return <div><Link to={`/Soba/Soba${el.id}`} className="btn">{el.naziv}</Link></div>
            })}
            </ul>
            <div>
                <button onClick={(event)=>{setShowForm(!showForm)}}>Dodaj novu sobu</button>
                {showForm && <CreateSobaForm/>}
            </div>
        </div>
    );
}

export default Sobe;