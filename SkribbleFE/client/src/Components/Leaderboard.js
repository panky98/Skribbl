import React, {useState} from 'react'
import Spinner from "./Spinner"
import {Link} from 'react-router-dom';
import useFetch from '../Services/useFetch.js';
function Leaderboard(){
    const {data:leaderboard, loading, error} = useFetch("KorisnikPoSobi/getLeaderboard");

    if(error) throw error;
    if(loading) return <Spinner/>
    console.log(leaderboard);
    const getAnimalsContent = leaderboard => {
        let content = [];
        for (let i = 0; i < leaderboard.length; i++) {
          const item = leaderboard[i];
          content.push(<tr key={i}><td>{i+1}</td><td>{item.username}</td><td>{item.id}</td><td>{item.password}</td><td>{item.id/item.password}</td></tr>);
        }
        console.log(content);
        return content;
      };
      
    return (<table><thead><tr><td>Place</td><td>Username</td><td>Points</td><td>Games</td><td>Average</td></tr></thead><tbody>{getAnimalsContent(leaderboard)}</tbody></table>);
}
export default Leaderboard