using Remotion.Utilities;

namespace Ouvidoria.Models
{
    public enum PerfilType : byte
    {
        [EnumDescription("Aluno")]
        Student = 0,

        [EnumDescription("Pais de Aluno")]
        ParentsStudents = 1,

        [EnumDescription("Professor")]
        Teacher = 2,

        [EnumDescription("Funcionário")]
        employee = 3,

        [EnumDescription("Visitante")]
        Visitor = 4,

        Aluno = 5,
    }
}