using HR_API.Data;
using HR_API.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ====================  ====================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SMSVersion3")));
// ====================================================
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));



// Add services to the container.

builder.Services.AddControllers();

// Thêm dòng này
builder.Services.AddSignalR();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(

    options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder => builder.AllowAnyOrigin() // Cho phép t?t c? các ngu?n
                              .AllowAnyHeader() // Cho phép t?t c? các tiêu ??
                              .AllowAnyMethod()); // Cho phép t?t c? các ph??ng th?c
    }


    );



var app = builder.Build();

app.MapHub<ChatHub>("/chatHub");

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
