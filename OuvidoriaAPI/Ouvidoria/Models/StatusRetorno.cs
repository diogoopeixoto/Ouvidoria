using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OuvidoriaAPI.DTO
{
    public enum StatusRetorno
    {
        /// <summary>
        /// Essa resposta provisória indica que tudo ocorreu bem até agora e que o cliente deve continuar com a requisição ou ignorar se já concluiu o que gostaria.
        /// </summary>
        Continue = 100,

        /// <summary>
        /// Este código indica que o servidor recebeu e está processando a requisição, mas nenhuma resposta está disponível ainda.
        /// </summary>
        Processing = 102,

        /// <summary>
        /// Estas requisição foi bem sucedida. O significado do sucesso varia de acordo com o método HTTP:
        /// </summary>
        OK = 200,

        /// <summary>
        /// A requisição foi bem sucedida e um novo recurso foi criado como resultado. Esta é uma tipica resposta enviada após uma requisição POST.
        /// </summary>
        Created = 201,

        /// <summary>
        /// Essa resposta significa que o servidor não entendeu a requisição pois está com uma sintaxe inválida.
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// O cliente não tem direitos de acesso ao conteúdo portanto o servidor está rejeitando dar a resposta. Diferente do código 401, aqui a identidade do cliente é conhecida.
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// O servidor não pode encontrar o recurso solicitado. Este código de resposta talvez seja o mais famoso devido à frequência com que acontece na web.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// O servidor encontrou uma situação com a qual não sabe lidar.
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// O servidor não está pronto para manipular a requisição. Causas comuns são um servidor em manutenção ou sobrecarregado. Note que junto a esta resposta, uma página amigável explicando o problema deveria ser enviada.
        /// </summary>
        ServiceUnavailable = 503
    }
}
