using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
// using FGC.API.Models; // Removido se n�o for usado diretamente aqui, mas necess�rio para ApplicationUser
using FGC.API.Data; // Namespace do seu DbContext e ApplicationUser
using Microsoft.AspNetCore.Identity;
using FGC.API.Models;
using FGC.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --- Configura��o do DbContext ---
// Registra o DbContext para o Identity.
// � crucial que este DbContext herde de IdentityDbContext<ApplicationUser>
builder.Services.AddDbContext<DbIdentityLoginContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbIdentityLoginContext") // Garanta que esta connection string est� correta no appsettings.json
    ?? throw new InvalidOperationException("Connection string 'DbIdentityLoginContext' n�o encontrada.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o do ASP.NET Core Identity 
// IMPORTANTE: Alterado de IdentityUser para ApplicationUser para usar a classe personalizada.
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;           // Exige pelo menos um d�gito (0-9).
    options.Password.RequireLowercase = true;       // Exige pelo menos uma letra min�scula (a-z).
    options.Password.RequireUppercase = true;       // Exige pelo menos uma letra mai�scula (A-Z).
    options.Password.RequireNonAlphanumeric = true; // Exige pelo menos um caractere n�o alfanum�rico (!, @, #, etc.).
    options.Password.RequiredLength = 8;            // Comprimento m�nimo de 8 caracteres.
    options.User.RequireUniqueEmail = true; // Exige que emails sejam �nicos (recomendado)

    // Outras op��es do Identity
    // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // Tempo de bloqueio ap�s tentativas falhas
    // options.Lockout.MaxFailedAccessAttempts = 5;                      // N� m�ximo de tentativas antes de bloquear
    // options.SignIn.RequireConfirmedAccount = false;                  // Define se a conta precisa ser confirmada (email/telefone) para login
    // options.SignIn.RequireConfirmedEmail = false;
    // options.SignIn.RequireConfirmedPhoneNumber = false;
})
// Configura o Entity Framework Core como o armazenamento para o Identity, usando o DbContext especificado.
.AddEntityFrameworkStores<DbIdentityLoginContext>()
// Adiciona os provedores de token padr�o (para reset de senha, confirma��o de email, etc.).
.AddDefaultTokenProviders();


// --- Autentica��o e Autoriza��o ---
// configura��o de autentica��o.
/*

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true, // Validar quem emitiu o token
        ValidateAudience = true, // Validar para quem o token foi emitido
        ValidateLifetime = true, // Validar se o token n�o expirou
        ValidateIssuerSigningKey = true, // Validar a assinatura do token
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Emissor configurado no appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"], // Audi�ncia configurada no appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Chave secreta
    };
});

builder.Services.AddAuthorization(); // Adiciona o servi�o de autoriza��o
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Em desenvolvimento, � �til ter p�ginas de erro detalhadas.
    app.UseDeveloperExceptionPage();
}
else
{
    // Em produ��o, use um handler de exce��o mais robusto e HSTS.
    app.UseExceptionHandler("/Error");
    app.UseHsts(); 
}

app.UseHttpsRedirection();

// IMPORTANTE: Adicionar UseAuthentication() ANTES de UseAuthorization() quando implementar o JWT.
// app.UseAuthentication(); // Habilita a autentica��o. //descomentado para n�o gerar erro de autentica��o no swagger

app.UseAuthorization(); // Habilita a autoriza��o.

app.UseMiddleware<ExceptionMiddleware>(); // Adiciona o middleware de tratamento de exce��es.

app.MapControllers(); 

app.Run();