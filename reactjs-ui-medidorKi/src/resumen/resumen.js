import React, { useEffect } from 'react';
import API from '../utiles/api';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Detalle from '../detalle/detalle';


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

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 700,  // Width of the modal
  height: 700, // Height of the modal
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
  border: '1px solid #ccc',
  borderRadius: '8px',
  padding: '10px',  
  textAlign: 'center',
  // boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
  overflowY: 'auto'
};


const PersonajeCard = ({ luchador }) => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <>
      <div style={styles.card} onClick={handleOpen}>
        <img src={luchador.UrlFotoLuchador} alt={`${luchador.NombreLuchador} Avatar`} style={styles.avatar} />
        <h3>{luchador.NombreLuchador}</h3>
        <img src={luchador.UrlImagenPersonaje} alt={luchador.NombrePersonaje} style={styles.imagen}/> {/* style={{width:'150px', height:'200px'}} />*/}
        <p>Esferas: {luchador.Esferas}</p>
        <p>Ki: {luchador.Ki}</p>
      </div>

      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Detalle id_luchador_personaje={luchador.IdLuchadorPersonaje} />
        </Box>
      </Modal>
    </>
  );
};

const Luchadores = () => {
  const [resumen, setResumen] = React.useState([]);

  useEffect(
    ()=>{
      const resumen = async ()=>{ 
        let response = await API.Get("api/luchador/resumen");
        setResumen(response.data);
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
      <h1>Resumen Luchadores</h1>
      <Luchadores />
    </div>
  );
};


export default Resumen