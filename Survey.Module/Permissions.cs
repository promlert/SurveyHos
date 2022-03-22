using OrchardCore.Security.Permissions;
using OrchardCore.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Module
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission SurveyAPIAccess = new Permission("SurveyAPIAccess", "Access to API ");
        public static readonly Permission ManageOwnUserProfile = new Permission("ManageOwnUserProfile", "Manage own user profile", new Permission[] { CommonPermissions.ManageUsers });

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { SurveyAPIAccess, ManageOwnUserProfile }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype {
                    Name = "Authenticated",
                    Permissions = new[] { SurveyAPIAccess }
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] { ManageOwnUserProfile }
                },
                new PermissionStereotype {
                    Name = "Moderator",
                    Permissions = new[] { ManageOwnUserProfile }
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = new[] { ManageOwnUserProfile }
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = new[] { ManageOwnUserProfile }
                }
            };
        }
    }
}
