namespace Ouvidoria.Models
{
    public class Manifestations
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public PerfilType PerfilType { get; set; }

        public string Campus { get; set; }

        public string Course { get; set; }

        public RequestType RequestType { get; set; }

        public SectorType SectorType { get; set; }

        public string Subject { get; set; }

        public string Manifestation { get; set; }
    }
}