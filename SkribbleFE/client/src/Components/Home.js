import React from 'react'

function Home() {

    if(localStorage.getItem("loginToken")!=null && localStorage.getItem("loginToken")!=undefined)
    {
        fetch("https://localhost:44310/Authenticate/checkLoginToken",{
            method:"GET",
            headers:{"Authorization":"Bearer "+localStorage.getItem("loginToken")}
        }).then(p=>{
            if(p.status==401)
            {
                localStorage.removeItem("loginToken");
                window.location.replace("/LogIn");
            }
        })
    }


    return (
        <div>
            
        </div>
    )
}

export default Home
