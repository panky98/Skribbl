import { useState, useEffect } from "react";

const baseUrl = "https://localhost:44310/";

export default function useFetch(url) {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function init() {
      try {
        const response = await fetch(baseUrl + url,{
          method:"GET",
          headers:{"Content-Type":"application/json","Authorization":"Bearer "+localStorage.getItem("loginToken")}
        });
        if (response.ok) {
          const json = await response.json();
          setData(json);
        }else if(response.status==401){
          window.location.replace("/LogIn");
        } 
        else {
          throw response;
        }
      } catch (e) {
        setError(e);
      } finally {
        setLoading(false);
      }
    }
    init(); 
  }, [url]);

  return { data, error, loading };
}
