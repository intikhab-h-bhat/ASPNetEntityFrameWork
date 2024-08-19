using ASPNetEntityFrameWork.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CollegeDBContext>(options =>
    //options.UseSqlServer("Data Source=LAPTOP-DMBBHO6R;Initial Catalog=GPSDB;Integrated Security=True;Trust Server Certificate=True") ) ;
    options.UseSqlServer(builder.Configuration.GetConnectionString("collegDbCon")));

// Add services to the container.

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable =true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
