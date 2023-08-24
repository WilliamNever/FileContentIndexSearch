using FileSearchByIndex.Core;
using FileSearchByIndex.Core.Interfaces;
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
            ServicesRegister.Services
                .AddTransient<ICreateIndexService, CreateIndexService>()
                .AddTransient<IFileAnalysis, FileAnalysis>()
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