using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.Contents;
using OrchardCore.Contents.Security;
using OrchardCore.Security.Permissions;

namespace Microsoft.AspNetCore.Authorization
{
    public static class AuthorizationServiceExtensions
    {
        /// <summary>
        /// Evaluate if we have a specific owner variation permission to at least one content type
        /// </summary>
        public static async Task<bool> AuthorizeContentTypeDefinitionsAsync(this IAuthorizationService service, ClaimsPrincipal user, Permission requiredPermission, IEnumerable<ContentTypeDefinition> contentTypeDefinitions, IContentManager contentManager)
        {
            var permission = GetOwnerVariation(requiredPermission);
            var contentTypePermission = ContentTypePermissionsHelper.ConvertToDynamicPermission(permission);

            foreach (var contentTypeDefinition in contentTypeDefinitions)
            {
                var dynamicPermission = ContentTypePermissionsHelper.CreateDynamicPermission(contentTypePermission, contentTypeDefinition);

                var contentItem = await contentManager.NewAsync(contentTypeDefinition.Name);
                contentItem.Owner = user.FindFirstValue(ClaimTypes.NameIdentifier);

                if (await service.AuthorizeAsync(user, dynamicPermission, contentItem))
                {
                    return true;
                }
            }

            return await service.AuthorizeAsync(user, permission);
        }

        private static Permission GetOwnerVariation(Permission permission)
        {
            if (permission.Name == CommonPermissions.PublishContent.Name)
            {
                return CommonPermissions.PublishOwnContent;
            }

            if (permission.Name == CommonPermissions.EditContent.Name)
            {
                return CommonPermissions.EditOwnContent;
            }

            if (permission.Name == CommonPermissions.DeleteContent.Name)
            {
                return CommonPermissions.DeleteOwnContent;
            }

            if (permission.Name == CommonPermissions.ViewContent.Name)
            {
                return CommonPermissions.ViewOwnContent;
            }

            if (permission.Name == CommonPermissions.PreviewContent.Name)
            {
                return CommonPermissions.PreviewOwnContent;
            }

            if (permission.Name == CommonPermissions.CloneContent.Name)
            {
                return CommonPermissions.CloneOwnContent;
            }

            return null;
        }
    }
}
