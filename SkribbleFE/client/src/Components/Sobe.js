import React, { useState,useEffect,useRef } from 'react';
import useFetch from "../Services/useFetch"
import Spinner from "./Spinner"
import {Link} from 'react-router-dom'
import CreateSobaForm from './CreateSobaForm';
import Pagination from "react-js-pagination";
function Sobe()
{
    const[showForm,setShowForm]=useState(false);
    const[sobe,setSobe]=useState([]);
    const sobePerPage = 5;
   const [ activePage, setCurrentPage ] = useState( 1 );
   const[showSpinner,setShowSpinner]=useState(false);
   const[filterSobe,setFilterSobe]=useState(false);
   const {data:kategorije, loading, error}=useFetch("Kategorija/getAllKategorija");
   // Logic for displaying current todos
   let indexOfLastSoba  = activePage * sobePerPage;
   let indexOfFirstSoba = indexOfLastSoba - sobePerPage;
   let currentSobe;
   const [renderSobe,setRenderSobe]=useState([]);
   const handlePageChange = ( pageNumber ) => {
      console.log( `active page is ${ pageNumber }` );
      setCurrentPage( pageNumber )
      let indexOfLastSoba  = pageNumber * sobePerPage;
      let indexOfFirstSoba = indexOfLastSoba - sobePerPage;
      currentSobe=sobe.slice( indexOfFirstSoba, indexOfLastSoba );
      setRenderSobe(currentSobe.map( ( soba, index ) => {
         return <div key={ index }><Link to={`/Soba/Soba${soba.id}`} className="resultElement">{ soba.naziv }</Link></div>; }));
        
   };
   if(error) throw error;
   if(loading) return <Spinner/>
   console.log(kategorije);
   console.log(renderSobe);
   
    return(
        <div>
         {showSpinner && <Spinner/> }
       <div>
       <div className="filterPart">
             <label>Filter by category </label>
             <select className="form-control" onChange={(ev)=>Filter(ev.currentTarget.value)} required>
             <option value="" disabled selected hidden>Select your option</option>
                <option value="-1">All categories</option>
                {
                   kategorije.map((el)=>{
                      return <option value={el.id}>{el.naziv}</option>
                   })
                }
             </select>
       </div>
          {                    
         <div className="result">
            { console.log(renderSobe)}
            {renderSobe }
         </div>}
         {
         <div className="pagination">
            <Pagination
               activePage={ activePage }
               itemsCountPerPage={ 5 }
               totalItemsCount={sobe.length }
               pageRangeDisplayed={ 3 }
               onChange={ handlePageChange }
            />
         </div>
         }
      </div>
            <div>
                <button className="btn btn-secondary" btn-lg onClick={(event)=>{setShowForm(!showForm)}}>Create new room</button>
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

                     setSobe(data);
                     setCurrentPage(1);
                     indexOfLastSoba  = activePage * sobePerPage;
                     indexOfFirstSoba = indexOfLastSoba - sobePerPage;
                     currentSobe=data.slice( indexOfFirstSoba, indexOfLastSoba );
                     setRenderSobe(currentSobe.map( ( soba, index ) => {
                        return <div key={ index }><Link to={`/Soba/Soba${soba.id}`} className="resultElement">{ soba.naziv }</Link></div>; }));
                       console.log(renderSobe);
                     setShowSpinner(false);
                  })
               }
               else if(p.status==401)
               {
                   localStorage.removeItem("loginToken");
                   window.location.replace("/LogIn");
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
                     console.log("ovde sam");
                     setSobe(data);
                     setCurrentPage(1);
                     indexOfLastSoba  = activePage * sobePerPage;
                     indexOfFirstSoba = indexOfLastSoba - sobePerPage;
                     currentSobe= data.slice( indexOfFirstSoba, indexOfLastSoba );
                     setRenderSobe(currentSobe.map( ( soba, index ) => {
                        return <div key={ index }><Link to={`/Soba/Soba${soba.id}`} className="resultElement">{ soba.naziv } </Link></div>; }));
                         setFilterSobe(true);
                     setShowSpinner(false);
                     console.log(renderSobe);
                  })
               }
               else if(p.status==401)
               {
                   localStorage.removeItem("loginToken");
                   window.location.replace("/LogIn");
               }
            }).catch(exc=>{
               console.log(exc);
            })
         }
    }
}

export default Sobe;