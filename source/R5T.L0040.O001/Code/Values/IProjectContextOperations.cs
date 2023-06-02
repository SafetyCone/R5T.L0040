using System;
using System.Threading.Tasks;

using R5T.T0131;
using R5T.T0187;
using R5T.T0161;
using R5T.T0172.Extensions;
using R5T.T0181.Extensions;

using R5T.L0040.T000;


namespace R5T.L0040.O001
{
    [ValuesMarker]
    public partial interface IProjectContextOperations : IValuesMarker
    {
        public Func<IProjectContext, Task> Create_AppSettingsJsonFile()
        {
            return context =>
            {
                var appSettingsFilePath = Instances.ProjectPathsOperator.GetAppSettingsJsonFilePath(
                    context.ProjectFilePath.Value)
                    .ToJsonFilePath();

                Instances.CodeFileGenerationOperations.Create_AppSettingsJsonFile(
                    appSettingsFilePath);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_Development_AppSettingsJsonFile()
        {
            return context =>
            {
                var appSettingsFilePath = Instances.ProjectPathsOperator.GetAppSettingsDevelopmentJsonFilePath(
                    context.ProjectFilePath.Value)
                    .ToJsonFilePath();

                Instances.CodeFileGenerationOperations.Create_Development_AppSettingsJsonFile(
                    appSettingsFilePath);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_DocumentationFile(
            IProjectDescription projectDescription,
            INamespaceName namespaceName)
        {
            return async context =>
            {
                var documentationFilePath = Instances.ProjectPathsOperator.GetDocumentationFilePath(
                    context.ProjectFilePath.Value)
                    .ToCSharpFilePath();

                await Instances.CodeFileGenerationOperations.Create_DocumentationFile(
                    documentationFilePath,
                    projectDescription,
                    namespaceName);
            };
        }

        public Func<IProjectContext, Task> Create_InstancesFile(
            INamespaceName projectNamespaceName)
        {
            return context =>
            {
                var instanceFilePath = Instances.ProjectPathsOperator.GetInstancesFilePath(
                    context.ProjectFilePath.Value)
                .ToCSharpFilePath();

                Instances.CodeFileGenerationOperations.Create_InstancesFile(
                    instanceFilePath,
                    projectNamespaceName);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_LaunchSettingsJsonFile_WebServerForBlazorClient()
        {
            return projectContext =>
            {
                var programFilePath = Instances.ProjectPathsOperator.GetLaunchSettingsJsonFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToJsonFilePath();

                Instances.CodeFileGenerationOperations.Create_LaunchSettingsJsonFile_WebServerForBlazorClient(
                    programFilePath,
                    projectContext.ProjectName);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_ProgramFile_Console(
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToCSharpFilePath();

                Instances.CodeFileGenerationOperations.Create_ProgramFile_ForConsole(
                    programFilePath,
                    projectNamespaceName);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_ProgramFile_WebServerForBlazorClient(
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToCSharpFilePath();

                Instances.CodeFileGenerationOperations.Create_ProgramFile_WebServerForBlazorClient(
                    programFilePath,
                    projectNamespaceName);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_ProjectPlanFile(
           IProjectName projectName,
           IProjectDescription projectDescription)
        {
            return context =>
            {
                var projectPlanFilePath = Instances.ProjectPathsOperator.GetProjectPlanMarkdownFilePath(
                    context.ProjectFilePath.Value)
                .ToMarkdownFilePath();

                Instances.CodeFileGenerationOperations.Create_ProjectPlanFile_Markdown(
                    projectPlanFilePath,
                    projectName,
                    projectDescription);

                return Task.CompletedTask;
            };
        }
    }
}
