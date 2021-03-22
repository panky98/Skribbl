import React, {useState} from 'react'
import Spinner from "./Spinner"
import {Link} from 'react-router-dom';
import { Redirect } from "react-router-dom";


function LogIn() {
    
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [showSpinner,setShowSpinner] = useState(false);


    return (
        <div>
          {showSpinner && <Spinner/>}
            <div className="effect">
                <div className="effect1">
                <div className="divLogin">
                Welcome,
        
                <p>glad to see you!</p>
        <br/><br/><br/><br/>
        
        <input className="inputLoginn" placeholder="Email" type="text" value={username} onChange={(ev)=>setUsername(ev.target.value)}/>
        <br/><br/>
        <input className="inputLoginn" placeholder="Password" type="password" value={password} onChange={(ev)=>setPassword(ev.target.value)}/>
        {/*nesto?
        <p className="error">
        Incorrect username or password!</p>:
        <p className="error"></p>
      
        */}
        <p className="error"></p>
        
        
        <button id="buttonLogin" onClick={()=>LogIn()}>LOGIN</button>
        
    </div>

      </div>
      <div className="effect2">
      <div className="aaa"></div>
      </div>
      <div className="proba" >
        <br/>Skribbl<br/><br/>
          <Link to="/SignUp">
            <button id="buttonSingUp" >CREATE ACCOUNT</button>
          </Link>
        
      </div>

    </div>
        </div>
    )
    function LogIn()
    {
      setShowSpinner(true);
      fetch("https://localhost:44310/Authenticate/login",{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify({"username":username,"password":password})
    }).then(p=>{
        if(p.ok){
            setShowSpinner(false);
            p.json().then(data=>{
              localStorage.setItem("loginToken",data.token);
              window.location.replace("/Sobe");
            });
        }
        else if(p.status==401)
        {
          console.log("Wrong username or password!");
          setShowSpinner(false);
        }
    }).catch(exc=>{
      console.log("Wrong username or password!");
      setShowSpinner(false);
    });
    
  }
}

export default LogIn
