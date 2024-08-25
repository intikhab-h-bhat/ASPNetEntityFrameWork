using ASPNetEntityFrameWork.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
//builder.Logging.AddDebug();
builder.Logging.AddConsole();


builder.Services.AddDbContext<CollegeDBContext>(options =>
    //options.UseSqlServer("Data Source=LAPTOP-DMBBHO6R;Initial Catalog=GPSDB;Integrated Security=True;Trust Server Certificate=True") ) ;
    options.UseSqlServer(builder.Configuration.GetConnectionString("collegDbCon")));

// Add services to the container.

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable =true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSceret"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken  = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };

});

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
