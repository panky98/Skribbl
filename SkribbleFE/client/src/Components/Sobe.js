import React, { useState,useEffect,useRef } from 'react';
import useFetch from "../Services/useFetch"
import Spinner from "./Spinner"
import {Link} from 'react-router-dom'
import CreateSobaForm from './CreateSobaForm';
import Pagination from "react-js-pagination";
function Sobe()
{
    const[showForm,setShowForm]=useState(false);
    const {data:sobe, loading, error}=useFetch("Soba/getAllSoba");
    const sobePerPage = 2;
   const [ activePage, setCurrentPage ] = useState( 1 );
   const niz=sobe;
    console.log("cekaju se sobe" +sobe);
    console.log(niz);
   // Logic for displaying current todos
 
   if(error) throw error;
   if(loading) return <Spinner/>
   const indexOfLastSoba  = activePage * sobePerPage;
   const indexOfFirstSoba = indexOfLastSoba - sobePerPage;
   let currentSobe;
   let renderSobe;
   currentSobe=sobe.slice( indexOfFirstSoba, indexOfLastSoba );
    

renderSobe = currentSobe.map( ( soba, index ) => {
   console.log(soba)
    return <div key={ index }><Link to={`/Soba/Soba${soba.id}`}  className="resultElement">{ soba.naziv }</Link></div>; });

   const handlePageChange = ( pageNumber ) => {
      console.log( `active page is ${ pageNumber }` );
      setCurrentPage( pageNumber )
   };
    console.log(sobe);
    return(
        <div>
       <div>
         <div className="result">
            { renderSobe }
         </div>
         <div className="pagination">
            <Pagination
               activePage={ activePage }
               itemsCountPerPage={ 2 }
               totalItemsCount={ sobe.length }
               pageRangeDisplayed={ 3 }
               onChange={ handlePageChange }
            />
         </div>
      </div>
            <div>
                <button className="btn btn-secondary" btn-lg onClick={(event)=>{setShowForm(!showForm)}}>Dodaj novu sobu</button>
                {showForm && <CreateSobaForm/>}
            </div>
        </div>
    );
}

export default Sobe;