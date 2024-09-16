// import './App.css';
import Login from './login/login-container';
import React from "react";
import { Route, BrowserRouter, Routes } from 'react-router-dom';


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route path='/login' element={<Login />} />
      </Routes>
    </BrowserRouter>
  );
};


export default App;