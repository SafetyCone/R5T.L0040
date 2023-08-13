using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using R5T.L0031.Extensions;
using R5T.L0040.T000;
using R5T.T0132;
using R5T.T0161;
using R5T.T0187;


namespace R5T.L0040.Internal
{
    [FunctionalityMarker]
    public partial interface IProjectContextOperator : IFunctionalityMarker
    {
        public Task Setup_RazorClassLibrary(
            IProjectContext projectContext,
            IProjectDescription projectDescription)
        {
            var projectNamespaceName = Instances.ProjectNamespaceNamesOperator.Get_DefaultProjectNamespaceName(
                projectContext.ProjectName);

            return projectContext.Run(
                 Instances.ProjectContextOperations_FileGeneration.Create_ProjectPlanFile(
                    projectDescription),
                Instances.ProjectContextOperations_FileGeneration.Create_InstancesFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_DocumentationFile(
                    projectDescription,
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_ExampleComponent(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_PlaceholderHtmlFile(),
                Instances.ProjectContextOperations_FileGeneration.Create_TailwindContentPathsJsonFile()
            );
        }

        public Task Setup_BlazorClient(
            IProjectContext projectContext,
            IProjectDescription projectDescription)
        {
            var projectNamespaceName = Instances.ProjectNamespaceNamesOperator.Get_DefaultProjectNamespaceName(
                projectContext.ProjectName);

            var operations = Instances.EnumerableOperator.From(
                Instances.ProjectContextOperations_FileGeneration.Create_ProjectPlanFile(
                    projectDescription),
                Instances.ProjectContextOperations_FileGeneration.Create_InstancesFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_DocumentationFile(
                    projectDescription,
                    projectNamespaceName)
                )
                .Append(
                    Instances.ProjectContextOperationSets.Setup_BlazorClient(
                        projectDescription,
                        projectNamespaceName)
                )
                ;

            return projectContext.Run(operations);
        }

        public Task Setup_WebServerForBlazorClient(
            IProjectContext projectContext,
            IProjectDescription projectDescription)
        {
            var projectNamespaceName = Instances.ProjectNamespaceNamesOperator.Get_DefaultProjectNamespaceName(
                projectContext.ProjectName);

            return projectContext.Run(
                Instances.ProjectContextOperations_FileGeneration.Create_ProjectPlanFile(
                    projectDescription),
                Instances.ProjectContextOperations_FileGeneration.Create_InstancesFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_DocumentationFile(
                    projectDescription,
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_ProgramFile_WebServerForBlazorClient(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_LaunchSettingsJsonFile_WebServerForBlazorClient(),
                Instances.ProjectContextOperations_FileGeneration.Create_AppSettingsJsonFile(),
                Instances.ProjectContextOperations_FileGeneration.Create_Development_AppSettingsJsonFile()
            );
        }

        public Task Setup_ConsoleProject(
            IProjectContext projectContext,
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext.Run(
                Instances.ProjectContextOperations_FileGeneration.Create_ProjectPlanFile(
                    projectDescription),
                Instances.ProjectContextOperations_FileGeneration.Create_InstancesFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_DocumentationFile(
                    projectDescription,
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_ProgramFile_Console(
                    projectNamespaceName)
            );
        }

        public Task Setup_ConsoleProject(
            IProjectContext projectContext,
            IProjectDescription projectDescription)
        {
            var projectNamespaceName = Instances.ProjectNamespaceNamesOperator.Get_DefaultProjectNamespaceName(
                    projectContext.ProjectName);

            return this.Setup_ConsoleProject(
                projectContext,
                projectDescription,
                projectNamespaceName);
        }

        public Task Setup_LibraryProject(
            IProjectContext projectContext,
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext.Run(
                Instances.ProjectContextOperations_FileGeneration.Create_ProjectPlanFile(
                    projectDescription),
                Instances.ProjectContextOperations_FileGeneration.Create_InstancesFile(
                    projectNamespaceName),
                Instances.ProjectContextOperations_FileGeneration.Create_DocumentationFile(
                    projectDescription,
                    projectNamespaceName),
                Instances.ProjectContextOperations.Add_ProjectFileReferences(
                    Instances.ProjectFileReferences.For_NET_6_FoundationLibrary)
            );
        }

        public Task Setup_LibraryProject(
            IProjectContext projectContext,
            IProjectDescription projectDescription)
        {
            var projectNamespaceName = Instances.ProjectNamespaceNamesOperator.Get_DefaultProjectNamespaceName(
                    projectContext.ProjectName);

            return this.Setup_LibraryProject(
                projectContext,
                projectDescription,
                projectNamespaceName);
        }
    }
}
