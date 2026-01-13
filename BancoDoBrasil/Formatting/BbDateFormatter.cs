using System.Globalization;

namespace BancoDoBrasil.Formatting;

    public static class BbDateFormatter
    {
        private const string Format = "dd.MM.yyyy";

        public static string FormatDate(DateTime date)
            => date.ToString(Format, CultureInfo.InvariantCulture);
    }

