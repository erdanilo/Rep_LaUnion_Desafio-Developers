import axios from "axios";
import { isSignedIn } from "./authentication";


let BASE_API = "http://localhost:56193/";

const axios_instance = axios.create({
  baseURL: BASE_API
});


class Api{
 
    async Login(user, pass) {
        const fetchEndpoint = `${BASE_API}Api/Login`;
    
        var details = {
          Usuario: user,
          Contrasenia: pass,
          UsuarioToken: "usrMedidorKi",
          PasswordToken: "p$$s0rdT0j3n"
        };
    
        var formBody = [];
        for (var property in details) {
          var encodedKey = encodeURIComponent(property);
          var encodedValue = encodeURIComponent(details[property]);
          formBody.push(encodedKey + "=" + encodedValue);
        }

        formBody = formBody.join("&");
    
        const query = await fetch(fetchEndpoint, {
          method: "POST",
          headers: {
            "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
          },
          body: formBody,
        });
    
        const request = await query.json();
        console.log("API[Login]: DATOS DE SESSION (TOKEN REQUEST)", request);
        
        return request;
    }

    async Get(path){
      try {
        let authorization = await isSignedIn();
        axios_instance.defaults.headers.common['Authorization'] = `Bearer ${authorization.accessToken}`
        const response = await axios_instance.get(path);

        return response;
      } catch (error) {
        console.error(error);
      }
    }
}


export default new Api();