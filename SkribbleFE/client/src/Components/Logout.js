import React, { useEffect, useState} from "react";
import NavBar from "./NavBar";
import Spinner from "./Spinner"
import {Link} from 'react-router-dom';
import { Redirect } from "react-router-dom";

import {useHistory} from "react-router-dom";


function Logout() {


  const history=useHistory();
  
  const refreshPage = ()=>{
    window.location.reload();
 }
  
 const yes = ()=>{
   localStorage.removeItem("loginToken");
   localStorage.removeItem("username");
    
    history.push("/Login");
    window.location.reload();
 }
 const no = ()=>{ 
    history.push("/");
 }
    

//ref();
    
    //setUser(data);
    
  
  useEffect(() =>
    {
      
      //refreshPage();
    });


  

  
    return (
    
        <div className="divLogout">
          
            Are you sure you want to log out?
            <br/>
            <button className="buttonInLogout" onClick={yes}>Yes</button>
            <button className="buttonInLogout" onClick={no}>No</button>
        </div>
      );

}
export default Logout