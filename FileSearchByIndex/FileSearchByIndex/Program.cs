using FileSearchByIndex.Core;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.Infrastructure.CSAnalysis.Services;
using FileSearchByIndex.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileSearchByIndex
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region register services
            ServicesRegister.Init();
            ServicesRegister.Services
                .AddTransient<ICreateIndexService, CreateIndexService>()
                .AddTransient<IFileAnalysis, FileAnalysis>()
                .AddTransient<IAnalysisService, CSAnalysisService>()
                ;

            ServicesRegister.Services
                .AddTransient<Func<string, IAnalysisService?>>(_ =>
                    exsions =>
                    {
                        var analysises = _.GetServices<IAnalysisService>();
                        var analisis = analysises?.FirstOrDefault(x => x.FileExtension.Equals(exsions, StringComparison.OrdinalIgnoreCase));
                        return analisis;
                    }
                )
                ;

            ServicesRegister.Build();
            #endregion
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}