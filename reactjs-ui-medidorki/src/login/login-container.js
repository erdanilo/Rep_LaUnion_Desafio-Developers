import React, { Component } from "react";
import {Box, Button} from "@mui/material";
import {CssBaseline} from "@mui/material";
import {FormControl} from "@mui/material";
import {Input} from "@mui/material";
import {InputLabel} from "@mui/material";
import {Typography} from "@mui/material";
import styled from "@emotion/styled";
// import API from "../../utils/api";
// import { signIn, isSignedIn } from "../../utils/auth";
import { redirect } from "react-router-dom";
import {AppBar} from "@mui/material";
import {Toolbar,Paper} from "@mui/material";
// import logo from "../../image-repository/logo.png";
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


class SignIn extends Component {
  state = {
    userName: "",
    password: "",
    redirect: false,
    isLogin: false,
  };

  async componentWillMount() {
    // let vIsSignedIn = await isSignedIn();

    // if (vIsSignedIn.response) {
    //   this.setState({ redirect: true });
    // }
  }

  handleLogin = async () => {
      this.setState({ isLogin: true });

    try {
      // await API.Login(this.state.userName, this.state.password, 1).then(
      //   async (result) => {
      //     if (await signIn(result)) {
      //       this.setState({ redirect: true });
      //     } else {
      //       this.setState({ isLogin: false, redirect: false });
            toast.error(
              "Su usuario o contraseña no son correctos, intentalo de nuevo."
            );
      //     }
      //   }
      // );
    } catch (e) {
      alert(
        "En este momento no se puede realizar tu solicitud, intentalo más tarde o llama a tu administrador"
      );
    } finally {
      this.setState({ isLogin: false });
    }
  };

  handleChange = (name) => (event) => {
    this.setState({
      [name]: event.target.value,
    });
  };

  _handleKeyPress = (e) => {
    if (e.key === "Enter") {
      this.handleLogin();
    }
  };

  submitHandler(e) {
    e.preventDefault();
  }

  render() {
    if (this.state.redirect) {
      return redirect("/home");
    }
    return (
      <>
        <CssBaseline />
        <AppBar position="static">
          <Toolbar style={{ backgroundColor: "#2B3C4D" }}>
            {/* <img src={logo} className="ImgLogo" /> */}
            {/* <img  className="ImgLogo" /> */}
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
                  onChange={this.handleChange("userName")}
                />
              </FormControl>
              <FormControl margin="normal" required fullWidth>
                <InputLabel htmlFor="password">Contraseña</InputLabel>
                <Input
                  name="password"
                  type="password"
                  id="password"
                  autoComplete="current-password"
                  onChange={this.handleChange("password")}
                  onKeyPress={this._handleKeyPress}
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
                onClick={this.handleLogin}
              >
                {this.state.isLogin ? (                  
                  <Oval visible="true" color="white" height="25" width="25" />
                ) : (
                  <></>
                )}
                &nbsp;Iniciar sesión
              </Button>
            </div>
          </Paper_styled>
        </Main_styled>
        <ToastContainer />
      </>
    );
  }
}


export default SignIn;