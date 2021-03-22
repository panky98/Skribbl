import React, {useState} from 'react'
import Spinner from './Spinner';

function CreateKategorijaForm() {

    const [kategorija, setKategorija] = useState('');
    const [novaRec, setNovaRec] = useState('');
    const [sveReci, setSveReci] = useState([]);
    const [showSpinner, setShowSpinner] = useState(false);
    const [alertKategorija, setAlertKategorija] = useState(false);
    const [alertReci, setAlertReci] = useState(false);

    const dodajNovuRec = (ev) =>{
        ev.preventDefault();
        setSveReci([...sveReci, novaRec]);
        console.log(sveReci);
        setNovaRec('');
        if(alertReci===true)
            setAlertReci(false);
    }
    const napraviKategoriju =async (ev)=>{
        setShowSpinner(true);

        ev.preventDefault();
        let obj={"naziv":kategorija};
        if(kategorija==='')       
        {
            setAlertKategorija(true);
            ev.preventDefault();
        }
        else
        {
            setAlertKategorija(false);
            if(sveReci.length===0)
            {
                setAlertReci(true);
                ev.preventDefault();
            }
            else
            {
                setAlertReci(false);
                
                await fetch("https://localhost:44310/Kategorija/createKategorija",{
                    method:"POST",
                    headers:{"Content-Type":"application/json"},
                    body:JSON.stringify(obj)
                }).then( (p)=> {
                    
                    if(p.ok){
                            
                             fetch("https://localhost:44310/Kategorija/getKategorijaByName/"+kategorija, {
                                method:"GET",
                                headers:{"Content-Type":"application/json"}
                            }).then( p2=>{
                                if(p2.ok)
                                {
                                    
                                    p2.json().then((json)=>
                                    {
                                        
                                        sveReci.map(async r=>{
                                            
                                            obj={"naziv":r, "kategorijaId":[json.id]};
                                            const res3=await fetch("https://localhost:44310/Rec/createRec",{
                                                method:"POST",
                                                headers:{"Content-Type":"application/json"},
                                                body:JSON.stringify(obj)
                                               
                                        })})
                                        window.location.reload();
                                    });
                    
                                    
                                }});
                        }
                }).catch(ex=>{
                    console.log("EX:"+ex);
                });

            }
            
        }
        setShowSpinner(false);
    }

    return (
        <form>
            {showSpinner && <Spinner/>}
            <br/>
            <input className="form-control" type="text" placeholder="Naziv nove kategorije"
             value={kategorija} onChange={(ev)=>setKategorija(ev.target.value)}/>

             <br/>
            {alertKategorija && <p style={{color:"red", display:"inline"}}>*Unesite kategoriju!</p>}
            
            <div><label>Unesite novu rec</label> </div>
            <input className="form-control" type="text" value={novaRec} onChange={(ev)=>setNovaRec(ev.target.value)}/>
            <div><button className="btn btn-secondary" onClick={(ev)=>dodajNovuRec(ev)}>Dodaj novu rec</button></div>
            

            <textarea className="form-control" rows="5"  value={sveReci} readOnly/>
            {alertReci && <p style={{color:"red", display:"inline"}}>*Unesite rec za kategoriju {kategorija}!</p>}
            <div><button className="btn btn-secondary" onClick={(ev)=>napraviKategoriju(ev)}>Napravi</button></div>
        </form>
    )
}


export default CreateKategorijaForm
