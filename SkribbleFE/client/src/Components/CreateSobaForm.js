
import React, { useState,useEffect,useRef } from 'react';
import useFetch from "../Services/useFetch"
import CreateKategorijaForm from './CreateKategorijaForm';
import ReciDateKategorije from './ReciDateKategorije';
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
            <label>Naziv sobe: </label>
            
            <input className="form-control" type="text" onChange={(event)=>{setNewNaziv(event.currentTarget.value)}}/>
            <br/>
            <br/>
            <select className="form-control" onChange={(event)=>setSelectedKategorijaId(event.currentTarget.value)}>
                <option  value={-1}>Izaberi kategoriju sobe</option>
                {kategorije.map(el=>{
                    return <option value={el.id}>{el.naziv}</option>
                })}
            </select>
            {selectedKategorijaId!=-1 && <ReciDateKategorije kategorijaId={selectedKategorijaId}/>}
            <button className="btn btn-secondary btn-lg" disabled={selectedKategorijaId!=-1?false:true} onClick={()=>{CreateSoba();}}>Create</button>
            
            
        </div>
    )

    
    function CreateSoba()
    {
        console.log(newNaziv+" "+selectedKategorijaId);
        setShowSpinner(true);
        fetch("https://localhost:44310/Soba/createSoba",{
            method:"POST",
            headers:{"Content-Type":"application/json","Authorization":"Bearer "+localStorage.getItem("loginToken")},
            body: JSON.stringify({"naziv":newNaziv,"kategorija":{"id":parseInt(selectedKategorijaId)},"status":true})
        }).then(p=>{
            if(p.ok){
                setShowSpinner(false);
                window.location.reload();
            }
            else if(p.status==401)
            {
                localStorage.removeItem("loginToken");
                window.location.replace("/LogIn");
            }
        }).catch(exc=>{
            console.log("EXCP:" +exc);
            setShowSpinner(false);
        });
    }
}

export default CreateSobaForm;