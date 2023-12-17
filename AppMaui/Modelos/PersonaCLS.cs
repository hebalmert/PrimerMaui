namespace AppMaui.Modelos
{
    public class PersonaCLS
    {
        public int iidpersona { get; set; } = 0;

        public string nombrecompleto { get; set; } = "";

        public string fechanacimientocadena { get; set; } = "";

        public string? correo { get; set; } = "";

        //Defino

        public string nombre { get; set; }

        public string appaterno { get; set; } = "";

        public string apmaterno { get; set; } = "";

        public DateTime fechanacimiento { get; set; }

        public int? iidSexo { get; set; } = 0;

        //Para el manejo de imagenes o archivos
        public string fotocadena { get; set; } = "";

        public string nombrearchivo { get; set; } = "";

        public byte[] archivo { get; set; } = new byte[0];


        public string telefono { get; set; } = "";

        public string nombresexo { get; set; } = "";

        public string nombreusuario { get; set; } = "";

        public string nombretipousuario { get; set; } = "";
    }
}