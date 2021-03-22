import React, {useState} from 'react'
import useFetch from '../Services/useFetch.js';
import CreateKategorijaForm from './CreateKategorijaForm.js';
import ReciDateKategorije from './ReciDateKategorije.js';
import ReciPoKategoriji from './ReciPoKategoriji.js';
import Spinner from './Spinner.js';



function Kategorije() {
    const [dugmeKategorija, setDugmeKategorija]=useState('Napravi novu kategoriju');
    const [showKategorijaForm, setShowKategorijaForm]=useState(false);

    const onClickNovaKategorija =(ev)=>
    {
        if(dugmeKategorija==='Zatvori')
        {
            setDugmeKategorija('Napravi novu kategoriju');
            setShowKategorijaForm(false);
        }
        else
        {
            setDugmeKategorija('Zatvori');
            setShowKategorijaForm(true);
        }
    }

    const {data:kategorije, loading, error} = useFetch("Kategorija/getAllKategorija");

    if(error) throw error;
    if(loading) return <Spinner/>

    return (
        <div>
            <button className="btn btn-secondary" onClick={()=>onClickNovaKategorija()}>{dugmeKategorija}</button>
            {showKategorijaForm && <CreateKategorijaForm/>}
            <div className="card-columns">
            
            <div className="col-sm-6">
            {/*<div className="card">*/}
                {kategorije.map(k=>{
                    return(
                        <div className="card"  key={k.id}>
                            <h3 className="card-title">{k.naziv}</h3>
                            {/*<ReciPoKategoriji id={k.id}/> -->*/}
                            <ReciDateKategorije kategorijaId={k.id}/>
                        </div>
                    )
                })}
            </div>
            </div>
            
        </div>
    )
}

export default Kategorije
