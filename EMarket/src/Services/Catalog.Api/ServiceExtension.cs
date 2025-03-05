using BuildingBlocks.Behaviors;
using Catalog.Api.Products.CreateProduct;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System.Reflection;
using Weasel.Core;

namespace Catalog.Api;

public static class ServiceExtension
{
    public static Assembly Assembly = typeof(ServiceExtension).Assembly;

    public static IServiceCollection AddMarten(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMarten(options =>
        {
            var conStr = configuration.GetConnectionString("Database")!;
            options.Connection(conStr);
            options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
        }).UseLightweightSessions();

        return services;
    }

    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly);
            options.AddOpenBehavior(typeof(ValidationBehaviors<,>));
            options.AddOpenBehavior(typeof(LoggingBehaviors<,>));
        });

        return services;
    }

    public static IServiceCollection AddValidationSchema(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly);
        services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        return services;
    }

    public static WebApplicationBuilder AddLogsIntoDb(
        this WebApplicationBuilder builder,
        IConfiguration configuration
       )
    {
        var conStr = configuration.GetConnectionString("Database")!;
        var tableName = "logs";

        var columnWriters = new Dictionary<string, ColumnWriterBase>()
        {
           { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
           { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
           { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
           { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
           { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
           { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
           { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
           { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
        };

        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .PostgreSQL(conStr, tableName, columnWriters)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }

    public static IServiceCollection AddHealthCheckerWithUi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);

        return services;
    }

}
