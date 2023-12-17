namespace Capa_Entitdad
{
    public class CitaCLS
    {
        //consultar
        public string nombremascota { get; set; } = "";

        public string nombretipomascota { get; set; } = "";

        public string fechaenfermedadcadena { get; set; } = "";

        public string nombreusuariomascota { get; set; } = "";

        public string nombreestadocita { get; set; } = "";

        public byte[] fotomascota { get; set; } = new byte[0];


        //insertar
        public int iidcita { get; set; } = 0;

        public int iidusuario { get; set; } = 0;

        public int iidtipomascota { get; set; } = 0;

        public int iidmascota { get; set; } = 0;

        public string descripcionenfermedad { get; set; } = "";

        public DateTime? fechaenfermedad { get; set; }

        public int iidsede { get; set; } = 0;

        public int iidestadocita { get; set; } = 0;
    }
}
