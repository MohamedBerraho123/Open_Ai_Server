using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Repository.Abstract;
using OpenAI_UIR.Repository.Implementation;
using OpenAI_UIR.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()), ServiceLifetime.Transient);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder => corsBuilder.WithOrigins("http://localhost:5173")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials());
});
// Regiter Repos
builder.Services.AddScoped<IAdminRepository,AdminRepository>();
builder.Services.AddScoped<IConversationRepository,ConversationRepository>();
builder.Services.AddScoped<IQuestionRepository,QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository,AnswerRepository>();
builder.Services.AddScoped<OpenAIService>();
//
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
