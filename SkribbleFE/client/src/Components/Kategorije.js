import React, {useState} from 'react'
import useFetch from '../Services/useFetch.js';
import CreateKategorijaForm from './CreateKategorijaForm.js';
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
            <button onClick={()=>onClickNovaKategorija()}>{dugmeKategorija}</button>
            {showKategorijaForm && <CreateKategorijaForm/>}
            {kategorije.map(k=>{
                return(
                    <div key={k.id}>
                        <h3>{k.naziv}</h3>
                        <ReciPoKategoriji id={k.id}/>
                    </div>
                )
            })}
        </div>
    )
}

export default Kategorije
