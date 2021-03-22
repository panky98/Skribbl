import React from 'react'

import { Link, NavLink } from "react-router-dom";
const activeStyle = {
  color: "purple",
};
function NavBar() {
    return (

      <header>
      <nav >
        <ul >
          <li>
            <Link activeStyle={activeStyle} to="/">
                Skribbl
            </Link>
          </li>    
          <li>
          <Link to="/Sobe">
                Rooms
            </Link>
           
          </li> 
          <li>
          <Link activeStyle={activeStyle} to="/Kategorije">
          Kategorije
            </Link>
           
          </li> 
          {window.localStorage.loginToken==null ? <li>
            
            <Link activeStyle={activeStyle} to="/LogIn">
                  LogIn
              </Link>
              </li>:

               <li >
            
              <Link activeStyle={activeStyle} to="/LogOut" >
                    Logout
                </Link>
                </li> 
              
              }
              <li>
         
          <Link activeStyle={activeStyle} to="/Leaderboard">
                Leaderboard
          </Link>
          </li> 
            {//window.localStorage.loginToken&&<li>
            //<Link activeStyle={activeStyle} to="/MyReplays">
            //      My Replays
            //  </Link>
            //  </li>
              
              }
        </ul>
      </nav>
      </header>
    )
}

export default NavBar
