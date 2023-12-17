namespace Capa_Entitdad
{
    public class MascotaCLS
    {
        public int iidmascota { get; set; }

        public string? nombremascota { get; set; }

        public string? nombretipomascota { get; set; }

        public string? nombresexo { get; set; }

        public string? nombrefoto { get; set; }

        public byte[]? foto { get; set; }

        //para Insertar, Editar o REcuperar

        public int iidtipomascota { get; set; }

        public DateTime? fechanacimiento { get; set; }

        public string? ancho { get; set; }

        public string? alto { get; set; }

        public int iidsexo { get; set; }

        public int iidusuariopropietario { get; set; }

        public string? vobservacion { get; set; }
    }
}
