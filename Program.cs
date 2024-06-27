using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var con = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LojaDBContext>(op => op.UseMySql(con, new MySqlServerVersion(new Version(8, 3, 0))));
builder.Services.AddScoped<ClienteService, ClienteService>();
builder.Services.AddScoped<ServicoServie, ServicoServie>();
builder.Services.AddScoped<ContratoService, ContratoService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        //Opcoes de validacao do token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("testetestetestetestetestetestetestetestetestetestetestetestetesteteste"))
        };
    });

var app = builder.Build();

#region Cliente

app.MapPost("/LoginCliente", async (ClienteService service, LoginModel loginUser) =>
{

    var isLogin = await service.Login(loginUser);

    if (isLogin)
    {

        var token = Token.GenerateToken();


        return Results.Ok("token: " + token);
    }

    return Results.Unauthorized();

});

//Usado Para Testes
app.MapPost("/CreateCliente", async (HttpContext context, ClienteService service, CLiente cliente) =>
{
    var isToken = Token.IsTokenPresenteEValido(context);

    if (!isToken)
    {

        return Results.Unauthorized();

    }

    await service.AddClienteAsync(cliente);
    return Results.Created($"createCliente/{cliente.id}", cliente);

});

#endregion

#region Servico

app.MapPost("/servico", async (HttpContext context, Servico servico, ServicoServie servicoService) =>
{
    var isToken = Token.IsTokenPresenteEValido(context);

    if (!isToken)
    {

        return Results.Unauthorized();

    }

    await servicoService.AddServicoAsync(servico);
    return Results.Created($"/servico/{servico.id}", servico);

});


app.MapGet("/servico/{id}", async (HttpContext context, int id, ServicoServie servicoS) =>
{
    var isToken = Token.IsTokenPresenteEValido(context);

    if (!isToken)
    {

        return Results.Unauthorized();

    }

    var servico = await servicoS.GetServicoById(id);
    if (servico == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(servico);

});


app.MapPut("/produtos/atualizar", async (HttpContext context, Servico servico, ServicoServie productService) =>
{
    var isToken = Token.IsTokenPresenteEValido(context);

    if (!isToken)
    {

        return Results.Unauthorized();

    }

    await productService.UpdateServicoAsync(servico);
    return Results.Ok();

});


#endregion

#region Contrato

app.MapPost("/CreateContrato", async (HttpContext context, ContratoService service, Contrato contrato) =>
{
    var isToken = Token.IsTokenPresenteEValido(context);

    if (!isToken)
    {

        return Results.Unauthorized();

    }

    var isSucess = await service.AddContratoAsync(contrato);

    if (!isSucess)
    {
        //Caso nao exista um CLiente ou Contrato Com os Ids informados
        return Results.NotFound("Servico ou Cliente inexistente");

    }

    return Results.Created($"createContrato/{contrato.id}", contrato);

});

app.MapGet("/clientes/{id}/servicos", async (HttpContext context, ContratoService service, int id) =>
{

    var isToken = Token.IsTokenPresenteEValido(context);

    if (!isToken)
    {

        return Results.Unauthorized();

    }

    var contratos = await service.FindContratosByIdCliente(id);

    if (contratos is null || contratos.Count == 0)
    {

        return Results.NotFound("Sem contratos para este cliente! ");

    }

    return Results.Ok(contratos);

});

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
