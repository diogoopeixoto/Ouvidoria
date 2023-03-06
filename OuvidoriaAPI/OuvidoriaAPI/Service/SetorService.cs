using OuvidoriaAPI.Data;
using OuvidoriaAPI.Domain;
using OuvidoriaAPI.DTO;

namespace OuvidoriaAPI.Service
{
    public class SetorService
    {
        public SetorDTO ObterPorId(Guid id)
        {
            using(OuvidoriaContext oc = new OuvidoriaContext())
            {
                Setor s = oc.Setores.Find(id);
                if (s == null) return null;
                return new SetorDTO(s);
            }
        }

        public SetorDTO ObterPorNome(string nome)
        {
            using (OuvidoriaContext oc = new OuvidoriaContext())
            {
                Setor s = oc.Setores.FirstOrDefault(x => x.Nome.Equals(nome));
                if (s == null) return null;
                return new SetorDTO(s);
            }
        }

        public ResultadoAcao Listar()
        {
            using (OuvidoriaContext oc = new OuvidoriaContext())
            {
                List<Setor> s = oc.Setores
                    .Where(x => x.Inativo == false)
                    .ToList();
                List<SetorDTO> dto = new List<SetorDTO>();
                s.ForEach(x => dto.Add(new SetorDTO(x)));
                return new ResultadoAcao(data: dto);
            }
        }
    }
}
