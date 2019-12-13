using System;
using System.Configuration;

namespace DesafioMetaWebApi
{
    public static class Funcoes
    {
        public static bool ValidaUsuario(String AutorizacaoRemota, String TokenRemoto)
        {
            try
            {
                String TokenLocal = ConfigurationManager.AppSettings["TOKEN_AUTHORIZATION"].ToString().Trim();
                String AutorizacaoLocal = ConfigurationManager.AppSettings["AUTORIZACAO"].ToString().Trim();
                if (AutorizacaoRemota == AutorizacaoLocal && TokenRemoto == TokenLocal) return true; else return false;
            }
            catch { return false; }

        }
    }
}