using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyApp.Infrastructure;

public class DefaultModelCustomizer : RelationalModelCustomizer
{
    public DefaultModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
    {
    }

    public override void Customize(ModelBuilder builder, DbContext context)
    {
        base.Customize(builder, context);

        var entityTypes = builder.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
        foreach (var type in entityTypes)
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", 1, new Type[] { }).MakeGenericMethod(type);
            var entityTypeBuilder = (EntityTypeBuilder)entityMethod.Invoke(builder, null);
            var properties = entityTypeBuilder.Metadata.GetProperties().ToList();

            entityTypeBuilder.ToTable(tableBuilder =>
            {
                var tableName = tableBuilder.Metadata.GetTableName();
                var schema = tableBuilder.Metadata.GetSchema();
                tableBuilder.Metadata.SetTableName(tableName.PascalToSnakeCase());
                tableBuilder.Metadata.SetSchema(schema.PascalToSnakeCase());
            });

            foreach (var property in properties)
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    entityTypeBuilder.Property(property.Name)
                        .HasColumnType("timestamp without time zone")
                        .HasConversion(typeof(DateTimeUnspecifiedKindConversion));

                var columnName = property.Name.PascalToSnakeCase();
                entityTypeBuilder.Property(property.Name).HasColumnName(columnName);
            }
        }
    }
}

public class DateTimeUnspecifiedKindConversion : ValueConverter<DateTime, DateTime>
{
    public DateTimeUnspecifiedKindConversion() : base(x => DateTime.SpecifyKind(x, DateTimeKind.Unspecified), x => x)
    {
    }
}