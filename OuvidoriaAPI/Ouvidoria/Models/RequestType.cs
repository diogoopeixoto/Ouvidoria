using Remotion.Utilities;

namespace Ouvidoria.Models
{
    public enum RequestType : byte
    {
        [EnumDescription("Elogio")]
        Praise = 0,

        [EnumDescription("Reclamação")]
        Complaint = 1,

        [EnumDescription("Sugestão")]
        Suggestion = 2,

        [EnumDescription("Outro")]
        Other = 3,
    }
}