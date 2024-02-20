namespace POS.Aplication.Dtos.Purcharse.Request
{
    public class PurcharseRequestDto
    {
        public string? Observacion {  get; set; }
        public decimal Subtotal { get; set; }
        public decimal Igv {  get; set; }
        public decimal TotalAmount { get; set; }
        public int WarehouseId { get; set; }
        public int ProviderId {  get; set; }
        public ICollection<PurcharseDetailRequestDto> purcharseDetails { get; set; } = null!;
    }
}
