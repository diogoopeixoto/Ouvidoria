using Microsoft.EntityFrameworkCore;
using OuvidoriaAPI.Data;
using OuvidoriaAPI.Domain;

namespace OuvidoriaAPI
{
    public class FakeDB
    {
        public static void Create()
        {
            using (OuvidoriaContext oc = new OuvidoriaContext())
            {
                oc.Database.Migrate();

                if (oc.Setores.Count() == 0)
                    oc.AddRange(
                            new Setor("Biblioteca"),
                            new Setor("Refeitório"),
                            new Setor("Secretaria"),
                            new Setor("Ginásio"),
                            new Setor("Portaria"),
                            new Setor("Conselho Pedagógico"),
                            new Setor("Reitoria")
                        );

                if (oc.Ouvidores.Count() == 0)
                    oc.AddRange(
                        new Ouvidor("Rafael", "rafael@ugb.com", "1234"),
                        new Ouvidor("Fernanda", "fernanda@ugb.com", "4321")
                    );

                oc.SaveChanges();
            }
        }
    }
}
