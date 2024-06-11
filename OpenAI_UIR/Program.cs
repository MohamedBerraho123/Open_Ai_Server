using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Mapper;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;
using OpenAI_UIR.Repository.Implementation;
using OpenAI_UIR.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()), ServiceLifetime.Transient);
// Regiter Repos
builder.Services.AddScoped<IAdminRepository,AdminRepository>();
builder.Services.AddScoped<IConversationRepository,ConversationRepository>();
builder.Services.AddScoped<IQuestionRepository,QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository,AnswerRepository>();
builder.Services.AddScoped<OpenAIService>();
//
builder.Services.AddAutoMapper(typeof(MapperConfig));
// add authorization
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppDbContext>();
//
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder => corsBuilder.WithOrigins("http://localhost:5173")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials());
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
//
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("user/me",async(ClaimsPrincipal claims,AppDbContext db) =>
{
   
    return await db.Users.Include(u => u.Conversation).ThenInclude(c => c.Questions).ThenInclude(q => q.Answer).FirstOrDefaultAsync(u => u.Id == userId);
}).RequireAuthorization();
app.MapIdentityApi<User>();
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
