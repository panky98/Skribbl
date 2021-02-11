import React from 'react'

import { Link, NavLink } from "react-router-dom";

function NavBar() {
    return (

      <div>
      <nav class="navbar navbar-light bg-light">
        <ul>
          <li class="navbar-brand">
            <Link to="/">
                Skribbl
            </Link>
          </li>    
          <li>
          <Link to="/Sobe">
                Rooms
            </Link>
           
          </li> 
          <li>
          <Link to="/LogIn">
                LogIn
            </Link>
            </li> 
        </ul>
      </nav>
      </div>
    )
}

export default NavBar
