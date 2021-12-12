using System;
using System.Threading.Tasks;
using CommonUIHelper.Repository;
using InterfaceDAL.Repository;
using Microsoft.Extensions.Configuration;
using ObjectFactory;

namespace CommonUIHelper.Curator
{
    public class AuthCurator
    {
        private IConfiguration _configuration;
        private AuthRepository _repository;
        public AuthCurator(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new AuthRepository(_configuration);
        }

        async public Task<bool> login(string username,string password)
        {
            var t = await _repository.login(username,password);
            if (t!=null && t.Rows.Count != 0 && Convert.ToInt32(t.Rows[0][0]) == 1)
                return true;
            return false;
        }
    }
}
