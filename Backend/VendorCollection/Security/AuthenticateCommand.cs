using VendorCollection.Security;
using System;
using System.Linq;
using System.Data.Entity;
using VendorCollection.Data.Models;
using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using VendorCollection.Data;
using System.Threading.Tasks;

namespace VendorCollection.Security
{
    public class AuthenticateCommand
    {
        public class AuthenticateRequest : IRequest<AuthenticateResponse>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class AuthenticateResponse
        {
            public bool IsAuthenticated { get; set; }
        }

        public class AuthenticateHandler : IAsyncRequestHandler<AuthenticateRequest, AuthenticateResponse>
        {
            public AuthenticateHandler(VendorCollectionDataContext dataContext, IEncryptionService encryptionService)
            {
                _encryptionService = encryptionService;
                _dataContext = dataContext;
            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }

            public async Task<AuthenticateResponse> Handle(AuthenticateRequest message)
            {
                var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == message.Username.ToLower() && !x.IsDeleted);

                return new AuthenticateResponse()
                {
                    IsAuthenticated = ValidateUser(user, _encryptionService.HashPassword(message.Password))
                };
            }


            private VendorCollectionDataContext _dataContext { get; set; }
            private IEncryptionService _encryptionService { get; set; }
        }

    }

}
