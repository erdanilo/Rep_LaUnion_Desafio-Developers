import Login from './login/login-container';
import Home from './home/home-container';
import React from "react";
import { Route, BrowserRouter, Routes } from 'react-router-dom';


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route path='/login' element={<Login />} />
        <Route path='/home' element={<Home />}/>
      </Routes>
    </BrowserRouter>
  );
};


export default App;