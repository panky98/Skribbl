import React from 'react'
import useFetch from '../Services/useFetch.js';
import Spinner from './Spinner.js';


function ReciPoKategoriji({id}) {

    const {data:reci, loading, error} = useFetch("RecPoKategoriji/getAllWordsByCategoryId/" + id);

    if(error) throw error;
    if(loading) return <Spinner/>
    return (
        <div>
        <h4>Reci</h4>
        <div>
            {reci.map(r=>{
                return(
                    <div key={r.id}>
                        {r.naziv}
                    </div>
                )
            })}
        </div>
        </div>
    )
}

export default ReciPoKategoriji
        