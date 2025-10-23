namespace WebHostApplication.ViewModels.Invoices
{
    public class DetalleCreateDTO
    {
        public string CodigoFijo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public int IdVendedor2 { get; set; }
    }
}
