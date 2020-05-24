using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharmacy.Application.Common.Constants
{
    public static class RoleConstants
    {
        public const string User = "user";
        public const string Manager = "manager";
        public const string Admin = "admin";
        public const string MainAdmin = "mainadmin";

        public static string GetRoleByIndex(int index) => index switch
        {
            0 => User,
            1 => Manager,
            2 => Admin,
            3 => MainAdmin,
            _ => throw new IndexOutOfRangeException()
        };

        public static string GetMainRole(IEnumerable<string> roles)
        {
            if (roles.Contains(MainAdmin))
                return MainAdmin;
            else if (roles.Contains(Admin))
                return Admin;
            else if (roles.Contains(Manager))
                return Manager;
            else if (roles.Contains(User))
                return User;
            else
                throw new ObjectNotFoundException(ExceptionStrings.RoleNotFoundException);
        }

        public static int GetRolePriority(string role)
        {
            RoleExists(role);

            var rolePriority = 0;

            for (int i = 0; i < IntegerConstants.NUMBER_OF_ROLES; i++)
            {
                if (GetRoleByIndex(i).Equals(role))
                   rolePriority = i;
            }

            return rolePriority;
        }

        public static bool RoleExists(string role)
        {
            StringArgumentValidator.ValidateStringArgument(role, nameof(role));

            string correctedRoleName = role.ToLower();

            for (int i = 0; i < IntegerConstants.NUMBER_OF_ROLES; i++)
            {
                if (GetRoleByIndex(i).Equals(correctedRoleName))
                {
                    return true;
                }
            }

            throw new ObjectNotFoundException(ExceptionStrings.RoleNotFoundException, role);
        }
    }
}
