import React, { useState,useEffect,useRef } from 'react';
import useFetch from "../Services/useFetch"
import Spinner from "./Spinner"
import {Link} from 'react-router-dom'

function Sobe()
{
    const {data:sobe, loading, error}=useFetch("Soba/getAllSoba");
    if(error) throw error;
    if(loading) return <Spinner/>
    console.log(sobe);
    return(
        <div>
            <ul>
            {sobe.map(el=>{
                return <div><Link to={`/Soba/Soba${el.id}`} className="btn">Soba{el.id}</Link></div>
            })}
            </ul>
        </div>
    );
}

export default Sobe;