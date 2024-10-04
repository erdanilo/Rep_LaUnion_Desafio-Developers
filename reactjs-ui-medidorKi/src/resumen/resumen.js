import React, { useEffect } from 'react';
import API from '../utiles/api'


const styles = {
  container: {
    display: 'flex',
    flexWrap: 'wrap',
    gap: '20px',
    justifyContent: 'center',
  },
  card: {
    border: '1px solid #ccc',
    borderRadius: '8px',
    padding: '10px',
    width: '200px',
    textAlign: 'center',
    boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
  },
  avatar: {
    borderRadius: '50%',
    width: '50px',
    height: '50px',
  },
  imagen: {
    width: '150px',
    height: '200px',
    // width: '100%',
    // height: '100%',
    // objectFit: 'cover'
    marginTop: '10px'
  },
};

const PersonajeCard = ({ luchador }) => {
  return (
    <div style={styles.card}>
      <img src={luchador.UrlFotoLuchador} alt={`${luchador.NombreLuchador} Avatar`} style={styles.avatar} />
      <h3>{luchador.NombreLuchador}</h3>
      <img src={luchador.UrlImagenPersonaje} alt={luchador.NombrePersonaje} style={styles.imagen}/> {/* style={{width:'150px', height:'200px'}} />*/}
      <p>Esferas: {luchador.Esferas}</p>
      <p>Ki: {luchador.Ki}</p>
    </div>
  );
};

const Luchadores = () => {
  const [resumen, setResumen] = React.useState([]);

  useEffect(
    ()=>{
      const resumen = async ()=>{ 
        let response = await API.Get("api/luchador/resumen");
        setResumen(response);
      };

      resumen();
    },[]);
  
  return (
    <div style={styles.container}>
      {
        resumen.map((resumen) => (
          <PersonajeCard key={resumen.NombreLuchador} luchador={resumen} />
        ))
      }
    </div>
  );
};


const Resumen = () => {
  return (
    <div>
      <h1>Lista de Peleadores</h1>
      <Luchadores />
    </div>
  );
};


export default Resumen