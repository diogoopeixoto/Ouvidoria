﻿
using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.DTO
{
    public class ManifestacaoDTO
    {
        public ManifestacaoDTO()
        {

        }
        public bool Excluido { get; set; }
        public DateTime? DataExclusao { get; set; }
        public Guid? OuvidorIdExclusao { get; set; }


        public Guid SetorId { get; set; }
        public virtual SetorDTO Setor { get; set; }

        public Guid? RespostaId { get; set; }
        public virtual RespostaDTO Resposta { get; set; }

        public Guid? VisOuvidorId { get; set; }
        public virtual OuvidorDTO VisOuvidor { get; set; }

        public DateTime? DataVisualizacao { get; set; }

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public PerfilManifestacao Perfil { get; set; }
        public TipoSolicitacao TipoSolicitacao { get; set; }


        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        public string Campus { get; set; }
        public string Curso { get; set; }

        public string Assunto { get; set; }
        public string Conteudo { get; set; }
    }
}
