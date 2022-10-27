using System.Data;

namespace CoderHouse_SistemaGestion.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripciones { get; set; }
        public float Costo { get; set; }
        public float PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto()
        {
            Id = 0;
            Descripciones = string.Empty;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }


    }
}
