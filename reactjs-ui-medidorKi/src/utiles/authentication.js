export async function signIn(info) {
    if (!info.error) {
        try {
          localStorage.setItem("userData", JSON.stringify(info));
        } catch (error) {
          return null;
        }

        return true;
    } else {
        await signOut(); //Eliminamos todas las sesiones para evitar sesiones activas.

        return false;
    }
};

export async function signOut() {
    try {
      localStorage.removeItem('userData')
    } catch (error) {
      alert("Error se cerrara sesion" + error);
    }

    return true;
};

export async function isSignedIn() {
  let signedIn = { response: false, accessToken: "" };

  try {
    const credentials = localStorage.getItem("userData");

    if (credentials) {
      let objCredentials = JSON.parse(credentials);

      signedIn["response"] = true;
      signedIn["accessToken"] = objCredentials.access_token;
    }
  } catch (error) {
    signedIn["message"] = "Error al obtener los datos de sesión!" + error;
  }
  
  return signedIn;
};