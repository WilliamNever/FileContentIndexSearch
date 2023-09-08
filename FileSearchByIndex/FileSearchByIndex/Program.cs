using FileSearchByIndex.Core;
using FileSearchByIndex.Core.Consts;
using FileSearchByIndex.Core.Interfaces;
using FileSearchByIndex.CustomForm;
using FileSearchByIndex.Infrastructure.CSAnalysis.Services;
using FileSearchByIndex.Infrastructure.Services;
using FileSearchByIndex.Infrastructure.TextAnalysis.Services;
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
            if(!Directory.Exists(EnviConst.IndexesFolderPath))
                Directory.CreateDirectory(EnviConst.IndexesFolderPath);
            if(!Directory.Exists(EnviConst.TmpWorkingFolderPath))
                Directory.CreateDirectory(EnviConst.TmpWorkingFolderPath);

            #region register services
            ServicesRegister.Init();
            ServicesRegister.Services
                .AddTransient<ICreateIndexService, CreateIndexService>()
                .AddTransient<IFileAnalysis, FileAnalysis>()
                .AddTransient<IAnalysisService, CSAnalysisService>()
                .AddTransient<IAnalysisService, TxtAnalysisEntrance>()
                .AddTransient<IAnalysisService, ENTextAnalysisService>()
                .AddTransient<IAnalysisService, CHTextAnalysisService>()
                .AddTransient<ISearchingIndexFilesSerivce, SearchingIndexFilesSerivce>()
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
            //Application.Run(new SearchResultForm());
        }
    }
}