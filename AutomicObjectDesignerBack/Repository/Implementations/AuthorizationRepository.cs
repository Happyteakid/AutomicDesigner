using AutomicObjectDesigner.Models.Registration;
using AutomicObjectDesignerBack.Data;

namespace AutomicObjectDesignerBack.Repository.Implementations
{
    public class AuthorizationRepository : RepositoryBase<UserModel>, IAuthorizationRepository
    {
        public AuthorizationRepository(AppDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}