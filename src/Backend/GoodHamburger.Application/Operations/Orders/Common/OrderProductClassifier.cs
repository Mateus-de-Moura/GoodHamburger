using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Application.Operations.Orders.Common
{
    public static class OrderProductClassifier
    {
        public static bool TryGetType(Product product, out MenuItemType type)
        {
            var category = Normalize(product.Category?.Description);
            var name = Normalize(product.Name);

            if (ContainsAny(category, "lanche", "hamburg", "sandu"))
            {
                type = MenuItemType.Sandwich;
                return true;
            }

            if (ContainsAny(category, "acompanhamento", "acomp", "batata", "frita", "side"))
            {
                type = MenuItemType.Side;
                return true;
            }

            if (ContainsAny(category, "bebida", "refrigerante", "drink", "suco"))
            {
                type = MenuItemType.Drink;
                return true;
            }
           
            if (ContainsAny(name, "batata", "fries"))
            {
                type = MenuItemType.Side;
                return true;
            }

            if (ContainsAny(name, "refrigerante", "soda", "drink", "suco"))
            {
                type = MenuItemType.Drink;
                return true;
            }

            if (ContainsAny(name, "burger", "hamburg", "x ", "x-"))
            {
                type = MenuItemType.Sandwich;
                return true;
            }

            type = default;
            return false;
        }

        private static string Normalize(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var normalized = value.Trim().ToLowerInvariant();
            normalized = normalized
                .Replace("á", "a")
                .Replace("à", "a")
                .Replace("ã", "a")
                .Replace("â", "a")
                .Replace("é", "e")
                .Replace("ê", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ô", "o")
                .Replace("õ", "o")
                .Replace("ú", "u")
                .Replace("ç", "c");

            return normalized;
        }

        private static bool ContainsAny(string source, params string[] values)
            => values.Any(source.Contains);
    }
}
