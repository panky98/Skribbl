
import React, { useState,useEffect,useRef } from 'react';
import useFetch from "../Services/useFetch"
import Spinner from "./Spinner"



function CreateSobaForm()
{
    const [showSpinner, setShowSpinner]=useState(false);
    const [newNaziv,setNewNaziv]=useState("");
    const [selectedKategorijaId,setSelectedKategorijaId]=useState(-1);
    const {data:kategorije, loading, error}=useFetch("Kategorija/getAllKategorija");

    if(error) throw error;
    if(loading) return <Spinner/>
    return(
        <div>
            {showSpinner && <Spinner/>}
            <label>Naziv sobe: </label> <input type="text" onChange={(event)=>{setNewNaziv(event.currentTarget.value)}}/>
            <select onChange={(event)=>setSelectedKategorijaId(event.currentTarget.value)}>
                <option value={-1}>Izaberi kategoriju sobe</option>
                {kategorije.map(el=>{
                    return <option value={el.id}>{el.naziv}</option>
                })}
            </select>
            <button disabled={selectedKategorijaId!=-1?false:true} onClick={()=>{CreateKategorija();}}>Create</button>
        </div>
    )

    function CreateKategorija()
    {
        console.log(newNaziv+" "+selectedKategorijaId);
        setShowSpinner(true);
        fetch("https://localhost:44310/Soba/createSoba",{
            method:"POST",
            headers:{"Content-Type":"application/json","Authorization":"Bearer "+localStorage.getItem("loginToken")},
            body: JSON.stringify({"naziv":newNaziv,"kategorija":{"id":parseInt(selectedKategorijaId)}})
        }).then(p=>{
            if(p.ok){
                setShowSpinner(false);
                window.location.reload();
            }
        }).catch(exc=>{
            console.log("EXCP:" +exc);
            setShowSpinner(false);
        });
    }
}

export default CreateSobaForm;