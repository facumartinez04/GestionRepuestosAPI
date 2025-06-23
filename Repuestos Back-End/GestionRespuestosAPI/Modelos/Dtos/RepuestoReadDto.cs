namespace GestionRespuestosAPI.Modelos.Dtos
{
    public class RepuestoReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string CategoriaNombre { get; set; }
        public List<string> VehiculosCompatibles { get; set; }
    }
}
