import React from 'react'

import { Link, NavLink } from "react-router-dom";

function NavBar() {
    return (

      <div>
      <nav class="navbar navbar-light bg-light">
        <ul>
          <li class="navbar-brand">
            <Link to="/">
                Fly Away
            </Link>
            <Link to="/Soba">
                Enter room
            </Link>
          </li>      
        </ul>
      </nav>
      </div>
    )
}

export default NavBar
