using VendorCollection.Data.Model;
using System.Threading.Tasks;
using System.Security.Principal;
using VendorCollection.Data;
using System.Data.Entity;

namespace VendorCollection.Security
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(IPrincipal user);
    }

    public class UserManager : IUserManager
    {
        public UserManager(IVendorCollectionContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(IPrincipal user) {
            return await _context.Users.SingleAsync(x => x.Username == user.Identity.Name);
        }

        protected readonly IVendorCollectionContext _context;
    }
}
