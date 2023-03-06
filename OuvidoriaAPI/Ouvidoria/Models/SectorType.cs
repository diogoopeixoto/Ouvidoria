using Remotion.Utilities;

namespace Ouvidoria.Models
{
    public enum SectorType : byte
    {
        [EnumDescription("Biblioteca")]
        Library = 0,

        [EnumDescription("Centro de Atendimento")]
        callcenter = 1,

        [EnumDescription("Financeiro")]
        Financial = 2,

        [EnumDescription("Outro")]
        Other = 3,
    }
}