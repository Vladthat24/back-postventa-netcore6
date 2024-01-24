using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Utilites.Static
{
    public class ReplyMessage
    {
        public const string MESSAGE_QUERY = "Consulta exitosa";
        public const string MESSAGE_QUERY_EMPTY = "No se encuentron registros";
        public const string MESSAGE_SAVE = "Se registró correctamente";
        public const string MESSAGE_UPDATE = "Se actualizó correctamente";
        public const string MESSAGE_DELETE = "Se eliminó correctamente";
        public const string MESSAGE_EXISTS = "El registro ya existe";
        public const string MESSAGE_ACTIVATE = "El registro ha sido activado";
        public const string MESSAGE_TOKEN = "Token generado correctamente";
        public const string MESSAGE_TOKEN_ERROR = "El usuerio y/o contraseña es incorrecta, compruébala.";
        public const string MESSAGE_VALIDATE = "Errores de validacion";
        public const string MESSAGE_FALIED = "Operacion fallida";
        public const string MESSAGE_EXCEPTION = "Hubo un error inesperado, comunicarse con el administrador";
        public const string MESSAGE_GOOGLE_ERROR = "Su cuenta no se encuentra registrada en el sistema";
        public const string MESSAGE_AUTH_TYPE_GOOGLE = "Por favor, ingrese con la opción de Google";
        public const string MESSAGE_AUTH_TYPE = "Su cuenta no se encuentra registrada en el sistema.";

    }
}
