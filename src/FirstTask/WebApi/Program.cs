using Application.Common;
using Domain;
using Infastructure.Common;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using WebApi.Common.Interfaces;
using WebApi.Common.Mapping;
using WebApi.Dtos;

var builder = WebApplication.CreateBuilder(args);



//Настройка логов.
   //Поле таблицы   Настройка таблицы
IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
    {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
};

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.PostgreSQL(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        tableName: "Logs",
        columnOptions: columnWriters,
        needAutoCreateTable: true //Каждый раз создается таблица при инициализации приложения
    )
    .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfastructure(builder.Configuration);
builder.Services.AddScoped<IMapper<SomeEntityDto,  SomeEntity>, SomeEntityMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
