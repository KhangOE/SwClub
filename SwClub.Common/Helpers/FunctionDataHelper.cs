namespace SwClub.Common.Helpers
{
    using SwClub.Common.Constants;
    using System.Reflection;

    public static class FunctionDataHelper
    {
        public static string LookupResource(IReflect resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType != typeof(System.Resources.ResourceManager))
                {
                    continue;
                }

                var resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                return resourceManager.GetString(resourceKey);
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
        {
            char[] password = new char[passwordSize];
            string charSet = string.Empty;
            Random random = new Random();
            int counter;

            // Build up the character set to choose from
            if (useLowercase)
            {
                charSet += GlobalConstant.LowerCharacters;
            }

            if (useUppercase)
            {
                charSet += GlobalConstant.UpperCharacters;
            }

            if (useNumbers)
            {
                charSet += GlobalConstant.Digits;
            }

            if (useSpecial)
            {
                charSet += GlobalConstant.SpecialCharacters;
            }

            for (counter = 0; counter < passwordSize; counter++)
            {
                password[counter] = charSet[random.Next(charSet.Length - 1)];
            }

            return string.Join(null, password);
        }
    }
}