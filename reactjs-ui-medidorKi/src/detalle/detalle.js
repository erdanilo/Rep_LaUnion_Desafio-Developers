import React, { useEffect, useState } from 'react';
import './detalle.css';
import API from '../utiles/api';


const Avatar = ({luchador}) => (
  <div>
    <img 
      src={luchador.UrlFotoLuchador}
      alt="foto del luchador"
      className="avatar-img"
    />
    <h3>{luchador.NombreLuchador}</h3>
    <img 
      src={luchador.UrlImagenPersonaje}
      alt="imagen del personaje"
      className="avatar-img-character"
    />
  </div>
);

const DragonBallSpheres = ({Ki, Esferas}) => {
  
  return(
    <div className='divSpheras'>
        <h2>Ki:&nbsp;{Ki}</h2>
        {
          Array.from({length: Esferas}).map((_, index)=>(
            <img key={index}
            src="https://png.pngitem.com/pimgs/s/542-5426664_sphere-esfera-dragon-drago-ki-super-power-poder.png" 
            alt="Dragon Ball Sphere"
            className="avatar-img-sphera"/>
          ))
        }
        {/* <img 
        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzvujaLY4OFp4GEQmzLU9rsr6Uv6VS-PuXwg&s" 
        alt="Dragon Ball Sphere"
        className="avatar-img"
        /> */
        }
    </div>
  )
};

const Category = ({categoria, habilidad}) => (
  <div className="category">
    <h3>{categoria.NombreCategoria}</h3>
    <ul>
      {categoria.Retos.map((reto) => (
        <li key={reto.IdReto}>{reto.NombreReto}
          {
          habilidad.filter(habilidad=> habilidad.IdCategoria === categoria.IdCategoria).map((categoria)=>(
            categoria.Retos.filter(retoHabilidad=> retoHabilidad.IdReto === reto.IdReto).map(()=>(
              <img
                src="https://png.pngitem.com/pimgs/s/542-5426664_sphere-esfera-dragon-drago-ki-super-power-poder.png" 
                alt="Dragon Ball Sphere"
                className="avatar-img-sphera"
              />
            ))
            
          ))
          }
        </li>
      ))}
    </ul>
  </div>
);


function Detalle({id_luchador_personaje}) {
  const [luchador,setLuchador] = useState({});
  const [categorias, setCategorias] = useState([]);
  const [habilidadesObtenidas, setHabilidadesObtenidas] = useState([]);

  useEffect(()=>{
    const fetchData = async ()=> {
      try {
        const response = await API.Get(`api/luchador/detalle/${id_luchador_personaje}`);
        setLuchador(response.data[0]);
        setCategorias(response.data[0].Categorias);
        setHabilidadesObtenidas(response.data[0].HabilidadesObtenidas)
      } catch (error) {
        console.log(error)
      }
    }

    fetchData();
  },[id_luchador_personaje]);

  return (
  <div className="App">
    <header className="App-header">
      <Avatar luchador={luchador}/>
      <DragonBallSpheres Ki={luchador.Ki} Esferas={luchador.Esferas}/>
      <div className="categories">
        {categorias.map((categoria) => (
          <Category 
            key = {categoria.IdCategoria} 
            categoria = {categoria}
            habilidad = {habilidadesObtenidas}
          />
        ))}
      </div>
    </header>
  </div>
  );
}


export default Detalle