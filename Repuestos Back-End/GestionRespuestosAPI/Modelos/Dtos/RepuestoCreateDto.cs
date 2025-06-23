namespace GestionRespuestosAPI.Modelos.Dtos
{
    public class RepuestoCreateDto
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public List<int> VehiculoIds { get; set; }
    }
}
