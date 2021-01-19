namespace DataLayer.DTOs
{
    public class RecPoKategorijiDTO
    {
        public int Id { get; set; }
        public RecDTO Rec { get; set; }
        public KategorijaDTO Kategorija { get; set; }

        public RecPoKategorijiDTO()
        {

        }

        public RecPoKategorijiDTO(RecDTO r, KategorijaDTO k)
        {
            this.Rec = r;
            this.Kategorija = k;
        }
    }
}