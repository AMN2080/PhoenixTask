using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PhoenixTask.Persistance.Extentions;

internal static class ModelBuilderExtentions
{
    private static readonly ValueConverter<DateTime, DateTime> UtcValueConverter =
            new ValueConverter<DateTime, DateTime>(outside => outside, inside => DateTime.SpecifyKind(inside, DateTimeKind.Utc));
    internal static void ApplyUtcDateTimeConverter(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<IMutableProperty> dateTimeUtcProperties = mutableEntityType.GetProperties()
                .Where(p => p.ClrType == typeof(DateTime) && p.Name.EndsWith("Utc", StringComparison.Ordinal));

            foreach (IMutableProperty mutableProperty in dateTimeUtcProperties)
            {
                mutableProperty.SetValueConverter(UtcValueConverter);
            }
        }
    }
}
