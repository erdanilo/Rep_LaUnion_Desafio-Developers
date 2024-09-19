let BASE_API = "http://localhost:56193/";


class Api{
    getBaseApi(){
        return BASE_API;
    }

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
}


export default new Api();