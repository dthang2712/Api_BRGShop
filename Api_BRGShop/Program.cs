using BRG.libary.BusinessObject;
using BRG.libary.BusinessService.Common;
using BRG.libary.Helper;
using log4net.Config;
using System.Reflection;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory()
});

XmlConfigurator.Configure(new FileInfo("log4netcore.config")); //Configure log4net
log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(Program));

try
{
    #region Config Connection

    CommonCoreUtils.SetFormatSystem();

    var settingsSection = builder.Configuration.GetSection("AppSettings");
    var settings = settingsSection.Get<AppSettings>();

    AppSettings.GetInstance().CopyValue(settings);

    DefaultConnectionFactory.BRGShop.ApplicationName = "BRGShop";
    DefaultConnectionFactory.BRGShop.HostAddress = AppSettings.GetInstance().HostAddress;
    DefaultConnectionFactory.BRGShop.DatabaseName = AppSettings.GetInstance().DatabaseName;
    DefaultConnectionFactory.BRGShop.EncryptUser = AppSettings.GetInstance().EncryptUser;
    DefaultConnectionFactory.BRGShop.EncryptPass = AppSettings.GetInstance().EncryptPass;

    SystemConfigConnection.GetConnection = DefaultConnectionFactory.BRGShop.GetConnection;

    #endregion


    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    _logger.Error(ex.Message, ex);
    throw;
}

