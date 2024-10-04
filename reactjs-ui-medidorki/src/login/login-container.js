import React, { useState } from "react";
import {Button} from "@mui/material";
import {CssBaseline} from "@mui/material";
import {FormControl} from "@mui/material";
import {Input} from "@mui/material";
import {InputLabel} from "@mui/material";
import {Typography} from "@mui/material";
import styled from "@emotion/styled";
import API from "../utiles/api";
import { signIn, isSignedIn } from "../utiles/authentication";
import { useNavigate } from "react-router-dom";
import {AppBar} from "@mui/material";
import {Toolbar,Paper} from "@mui/material";
import {Oval} from "react-loader-spinner";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";


//#region creacion de componentes de react y etiquetas html estilizados con la funcion styled de la libreria @emotion/styled
const Main_styled = styled('main')({
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  minHeight: '100vh' // Ensures the Paper is centered vertically
});

const Paper_styled = styled(Paper)(() =>({
  width:'400px', 
  textAlign:'center',
  padding:'30px'
}));
//#endregion


function SignIn () {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [isLogin, setIsLogin] = useState(false);

  const navigate = useNavigate();
  const handleRedirect = () => { navigate('/home'); };

  async function handleLogin() {
    setIsLogin(true);

    try {
      await API.Login(userName, password).then(
        async (response) => {
          if (response.Estado) {
            if (await signIn(response)) {
              handleRedirect();
            } 
          } else {
            setIsLogin(false);
            toast.error("Su usuario o contraseña no son correctos.");
          }
        });
    } catch (e) {
        alert(
          "En este momento no se puede realizar su solicitud"
        );
    } finally {
      setIsLogin(false);
    }
  }

  function handleChangeUserName(e){
    setUserName(e.target.value);
  }

  function handleChangePassword(e){
    setPassword(e.target.value);
  }

  function handleKeyPress (e) {
    if (e.key === 'Enter') {
      handleLogin();
    }
  }

    return (
      <div>
        <CssBaseline />
        <AppBar position="static">
          <Toolbar style={{ backgroundColor: "#2B3C4D" }}>
            <img />
          </Toolbar>
        </AppBar>
        <Main_styled>
          <Paper_styled variant="elevation" elevation={3}>
            <Typography component="h1" variant="h5">
              Medidor KI
            </Typography>
            <div>
              <FormControl margin="normal" required fullWidth>
                <InputLabel htmlFor="usuario">Usuario</InputLabel>
                <Input
                  id="usuario"
                  name="usuario"
                  autoComplete="usuario"
                  autoFocus
                  onChange={handleChangeUserName}
                />
              </FormControl>
              <FormControl margin="normal" required fullWidth>
                <InputLabel htmlFor="password">Contraseña</InputLabel>
                <Input
                  name="password"
                  type="password"
                  id="password"
                  autoComplete="current-password"
                  onChange={handleChangePassword}
                  onKeyDown={handleKeyPress}
                />
              </FormControl>
              <Button
                fullWidth
                variant="contained"
                style={{
                  backgroundColor: "#e85635",
                  color: "white",
                  alignItems: "center",
                  justifyContent: "center",
                }}
                onClick={handleLogin}
              >
                {isLogin ? <Oval color="white" height="25" width="25" /> : <></> }
                &nbsp;Iniciar sesión
              </Button>
            </div>
          </Paper_styled>
        </Main_styled>
        <ToastContainer />
      </div>
    );
}


export default SignIn;