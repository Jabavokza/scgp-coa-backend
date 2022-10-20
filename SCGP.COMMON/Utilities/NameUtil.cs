using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGP.COA.COMMON.Utilities
{
    public class NameUtil
    {
        public static string GetFullNameFormat(string firstName, string lastName)
        {
            firstName = firstName ?? "";
            lastName = lastName ?? "";

            firstName = firstName.FirstLetterToUpper();
            lastName = lastName.FirstLetterToUpper();

            var formatted = firstName;
            if (!string.IsNullOrEmpty(lastName))
            {
                formatted += " ";
                formatted += (lastName.Length > 1 ? lastName.Substring(0, 1) : lastName) + ".";
            }
            return formatted;
        }

        public static string GetFullNameFormatFromFullName(string fullName)
        {
            var nameSplitted = fullName?.SplitToList(" ");
            var firstName = nameSplitted != null && nameSplitted.Any() ? nameSplitted[0] : "";
            var lastName = nameSplitted != null && nameSplitted.Any() ? nameSplitted[nameSplitted.Count - 1] : "";
            if (firstName == lastName) lastName = "";

            var formatted = GetFullNameFormat(firstName, lastName);
            return formatted;
        }

        public static string GetFullNameFormatFromUserName(string username)
        {
            var domainSplitted = username?.SplitToList("@");
            var usernameNoDomain = domainSplitted != null && domainSplitted.Any() ? domainSplitted[0] : "";

            var usernameSplitted = usernameNoDomain?.SplitToList(".");
            var firstName = usernameSplitted != null && usernameSplitted.Any() ? usernameSplitted[0] : "";
            var lastName = usernameSplitted != null && usernameSplitted.Any() ? usernameSplitted[usernameSplitted.Count - 1] : "";
            if (firstName == lastName) lastName = "";

            var formatted = GetFullNameFormat(firstName, lastName);
            return formatted;
        }
    }
}
