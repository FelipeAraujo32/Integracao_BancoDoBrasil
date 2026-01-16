using System.Globalization;

namespace BancoDoBrasil.Formatting;

    public static class BbDateFormatter
    {
        private const string DateFormat = "dd.MM.yyyy";

        public static string Format(DateTime date)
        {
            return date.ToString(DateFormat, CultureInfo.InvariantCulture);
        }

        public static string? Format(DateTime? date)
        {
            return date?.ToString(DateFormat, CultureInfo.InvariantCulture);
        }
    }

