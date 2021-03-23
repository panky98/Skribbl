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
    const {data:kategorije, loadingKateogorije, errorKategorije}=useFetch("Kategorija/getAllKategorija");
    const sobePerPage = 2;
   const [ activePage, setCurrentPage ] = useState( 1 );
   const[showSpinner,setShowSpinner]=useState(false);
   const niz=sobe;
    console.log("cekaju se sobe" +sobe);
    console.log(niz);
   // Logic for displaying current todos
 
   if(error) throw error;
   if(loading) return <Spinner/>
   let indexOfLastSoba  = activePage * sobePerPage;
   let indexOfFirstSoba = indexOfLastSoba - sobePerPage;
   let currentSobe;
   let renderSobe;
   currentSobe=sobe.slice( indexOfFirstSoba, indexOfLastSoba );

   if(errorKategorije) throw error;
   if(loadingKateogorije) return <Spinner/>
    

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
         {showSpinner && <Spinner/> }
       <div>
       <div className="filterPart">
             <label>Filter by category </label>
             <select onChange={(ev)=>Filter(ev.currentTarget.value)}>
                <option value="-1">All categories</option>
                {
                   kategorije.map((el)=>{
                      return <option value={el.id}>{el.naziv}</option>
                   })
                }
             </select>
          </div>
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

    function Filter(val)
    {
       setShowSpinner(true);
         if(val==-1)
         {
            fetch("https://localhost:44310/Soba/getAllSoba",{
               method:"GET",
               headers:{"Content-Type":"application/json",
               "Authorization":"Bearer "+localStorage.getItem("loginToken")
              }
            }).then(p=>{
               if(p.ok)
               {
                  p.json().then(data=>{
                     setCurrentPage(1);
                     indexOfLastSoba  = 1 * sobePerPage;
                     indexOfFirstSoba = indexOfLastSoba - sobePerPage;
                     currentSobe=data.slice( indexOfFirstSoba, indexOfLastSoba );
                     setShowSpinner(false);
                  })
               }
            }).catch(exc=>{
               console.log(exc);
            })
         }
         else
         {
            fetch("https://localhost:44310/Soba/getAllByCategory/"+val,{
               method:"GET",
               headers:{"Content-Type":"application/json",
               "Authorization":"Bearer "+localStorage.getItem("loginToken")
              },
            }).then(p=>{
               if(p.ok)
               {
                  p.json().then(data=>{
                     setCurrentPage(1);
                     indexOfLastSoba  = 1 * sobePerPage;
                     indexOfFirstSoba = indexOfLastSoba - sobePerPage;
                     currentSobe=data.slice( indexOfFirstSoba, indexOfLastSoba );
                     setShowSpinner(false);
                  })
               }
            }).catch(exc=>{
               console.log(exc);
            })
         }
    }
}

export default Sobe;