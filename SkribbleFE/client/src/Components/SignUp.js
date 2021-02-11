import React, {useState} from 'react'

import {Link} from 'react-router-dom';
function SignUp() {

   const [username, setUsername]=useState('');
   const [password, setPassword]=useState('');

   async function handleSubmit()
   {
        //alert(username + " " +  password)

        const obj={
            
            "username":username,
            "password":password
        }
        await fetch("https://localhost:44310/Korisnik/createKorisnik",{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify(obj)    
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno dodato!");
        }
    }).catch(exc=>{
        console.log(exc);
    });
   }

    return (
        <form onSubmit={()=>handleSubmit()}>
            <div className="effect">
            <div className="effect1">
            <div className="aaa"></div>
            </div>
      <div className="effect2">
        <div className="divSignup">
        <br/>
          <input className="inputSignup" placeholder="Username" type="text" value={username} onChange={(ev)=>setUsername(ev.target.value)}/>
          <br/><br/><br/> 
          <input className="inputSignup" placeholder="Password" type="password" value={password} onChange={(ev)=>setPassword(ev.target.value)}/>
          {/*nesto?
              <p className="error">
                All fields are required!</p>:
              <p className="error"></p>
          */}
          <button id="buttonSingUp" >CREATE ACCOUNT</button>
        </div>
      </div>
      <div className="proba2" >
        <br/>Skribbl<br/><br/>
          <Link to="/LogIn">
            <button id="buttonLogin">LOGIN</button>
          </Link>
      </div>
    </div>
        </form>
    )
}

export default SignUp
   