using System.Data.Entity;

namespace DesafioMetaWebApi.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext() : base("name=BaseContext")
        {
        }

        public System.Data.Entity.DbSet<DesafioMetaWebApi.Models.Contatos> Contatos { get; set; }
    }
}
