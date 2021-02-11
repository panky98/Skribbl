import React, {useState} from 'react'

import {Link} from 'react-router-dom';

function LogIn() {
    
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    function handleSubmit()
    {
        alert(username + " " + password)
    }
    return (
        <form onSubmit={()=>handleSubmit()}>
            <div className="effect">
                <div className="effect1">
                <div className="divLogin">
                Welcome
        
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
        
        
        <button id="buttonLogin">LOGIN</button>
        
    </div>

      </div>
      <div className="effect2">
      <div className="aaa"></div>
      </div>
      <div className="proba" >
        <br/>Skribbl<br/><br/>
          <Link to="/SignUp">
            <button id="buttonSingUp">CREATE ACCOUNT</button>
          </Link>
        
      </div>

    </div>
        </form>
    )
}

export default LogIn
