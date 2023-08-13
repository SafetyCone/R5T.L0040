using System;
using System.Threading.Tasks;

using R5T.L0040.T000;
using R5T.T0131;
using R5T.T0161;
using R5T.T0187;


namespace R5T.L0040
{
    [ValuesMarker]
    public partial interface IProjectContextOperationSets : IValuesMarker
    {
        public Func<IProjectContext, Task>[] Setup_BlazorClient(
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return new[]
            {
                Instances.ProjectContextOperations_FileGeneration.Create_ProgramFile_BlazorClient(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_LaunchSettingsJsonFile_WebServerForBlazorClient(),
                Instances.ProjectContextOperations_FileGeneration.Create_PackageJsonFile(
                    projectDescription,
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_TailwindConfigJsFile(),
                Instances.ProjectContextOperations_FileGeneration.Create_TailwindCssFile(),
                Instances.ProjectContextOperations_FileGeneration.Create_IndexHtmlFile_WebBlazorClient(),
                Instances.ProjectContextOperations_FileGeneration.Create_AppRazorFile_WebBlazorClient(),
                Instances.ProjectContextOperations_FileGeneration.Create_ImportsRazorFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_PagesImportsRazorFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_ComponentsImportsRazorFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_ComponentsLayoutsImportsRazorFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_ComponentsLayoutsLayoutRazorFile(),
                Instances.ProjectContextOperations_FileGeneration.Create_ComponentsLayoutsLayoutClassFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_IndexRazorFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_TailwindContentPathsJsonFile(),
                Instances.ProjectContextOperations_FileGeneration.Create_TailwindAllContentPathsJsonFile(),
                Instances.ProjectContextOperations.Run_NpmInstall(),
                Instances.ProjectContextOperations.Add_ProjectFileReferences(
                    Instances.ProjectFileReferenceSets.For_BlazorClient)
            };
        }
    }
}
