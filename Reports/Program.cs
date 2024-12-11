using DinkToPdf;
using DinkToPdf.Contracts;
using Reports;
using Reports.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddSingleton<IConverter, SynchronizedConverter>(sp =>
            new SynchronizedConverter(new PdfTools()));

        services.AddScoped<PdfService>();

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();