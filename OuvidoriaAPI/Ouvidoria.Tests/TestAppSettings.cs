using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouvidoria.Tests
{

    public class TestAppSettings
    {
        public string ServidorEmail { get; set; }
        public int Porta { get; set; }
        public bool SSL { get; set; }
        public string UsuarioEmail { get; set; }
        public string SenhaEmail { get; set; }
        public string UsrDB { get; set; }
        public string SenhaDB { get; set; }
    }

}
