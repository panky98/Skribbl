import React from 'react'
import Spinner from '../Components/Spinner.js'
import useFetch from '../Services/useFetch.js'

function ReciDateKategorije({kategorijaId}) {

    const {data:reci, loading, error}=useFetch("RecPoKategoriji/getAllWordsByCategoryId/"+kategorijaId);


    if(error) throw error;
    if(loading) return <Spinner/>
    return (
        <div>
            Words of selected category:
            <ul className="list-group">
                {reci.map(r=>{
                    return(
                        <li className="list-group-item" key={r.id}>{r.naziv}</li>
                    )
                })}
            </ul>
        </div>
    )
}

export default ReciDateKategorije
 