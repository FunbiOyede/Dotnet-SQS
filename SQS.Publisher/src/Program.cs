using Amazon.SQS;
using SQS.Publisher.Data;
using SQS.Publisher.Implementation;
using SQS.Publisher.Messaging;
using SQS.Publisher.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrderContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<ISqsMessage, SqsMessager>();
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));
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
