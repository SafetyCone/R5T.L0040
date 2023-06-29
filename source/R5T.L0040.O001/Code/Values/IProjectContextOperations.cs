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
        public Func<IProjectContext, Task> Create_AppRazorFile()
        {
            return projectContext =>
            {
                var razorFilePath = Instances.ProjectPathsOperator.GetAppRazorFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToRazorFilePath();

                Instances.CodeFileGenerationOperations.Create_AppRazorFile_WebBlazorClient(
                    razorFilePath);

                return Task.CompletedTask;
            };
        }

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

        public Func<IProjectContext, Task> Create_IndexRazorFile()
        {
            return projectContext =>
            {
                var razorFilePath = Instances.ProjectPathsOperator.GetIndexRazorFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToRazorFilePath();

                Instances.CodeFileGenerationOperations.Create_IndexRazorFile_WebBlazorClient(
                    razorFilePath);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_MainLayoutRazorFile(
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                var razorFilePath = Instances.ProjectPathsOperator.GetMainLayoutRazorFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToRazorFilePath();

                Instances.CodeFileGenerationOperations.Create_MainLayoutRazorFile_WebBlazorClient(
                    razorFilePath,
                    projectNamespaceName);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_ImportsRazorFile(
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                var razorFilePath = Instances.ProjectPathsOperator.GetMainImportsRazorFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToRazorFilePath();

                Instances.CodeFileGenerationOperations.Create_ImportsRazorFile_WebBlazorClient_Main(
                    razorFilePath,
                    projectNamespaceName);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_IndexHtmlFile(
            string pageTitle)
        {
            return projectContext =>
            {
                var htmlFilePath = Instances.ProjectPathsOperator.GetWwwRootIndexHtmlFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToHtmlFilePath();

                Instances.CodeFileGenerationOperations.Create_IndexHtmlFile(
                    htmlFilePath,
                    pageTitle);

                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Uses the project name as the index page title.
        /// </summary>
        public Func<IProjectContext, Task> Create_IndexHtmlFile()
        {
            return projectContext =>
            {
                var htmlFilePath = Instances.ProjectPathsOperator.GetWwwRootIndexHtmlFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToHtmlFilePath();

                Instances.CodeFileGenerationOperations.Create_IndexHtmlFile(
                    htmlFilePath,
                    projectContext.ProjectName.Value);

                return Task.CompletedTask;
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

        public Func<IProjectContext, Task> Create_ExampleComponent(
            INamespaceName projectNamespaceName)
        {
            return async projectContext =>
            {
                var exampleComponentFilePath = Instances.ProjectPathsOperator.GetExampleComponentRazorFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToRazorFilePath();

                await Instances.CodeFileGenerationOperations.Create_ExampleComponentRazorFile(
                    exampleComponentFilePath,
                    projectNamespaceName);
            };
        }

        public Func<IProjectContext, Task> Create_PlaceholderHtmlFile()
        {
            return async projectContext =>
            {
                var placeholderHtmlFilePath = Instances.ProjectPathsOperator.Get_PlaceholderHtmlFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToHtmlFilePath();

                await Instances.CodeFileGenerationOperations.Create_PlaceholderHtmlFile(
                    placeholderHtmlFilePath);
            };
        }

        public Func<IProjectContext, Task> Create_PackageJsonFile(
            IProjectName projectName,
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                var jsonFilePath = Instances.ProjectPathsOperator.GetPackageJsonFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToJsonFilePath();

                Instances.CodeFileGenerationOperations.Create_PackageJsonFile(
                    jsonFilePath,
                    projectName,
                    projectDescription);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_ProgramFile_BlazorClient(
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToCSharpFilePath();

                Instances.CodeFileGenerationOperations.Create_ProgramFile_WebBlazorClient(
                    programFilePath,
                    projectNamespaceName);

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

        public Func<IProjectContext, Task> Create_TailwindConfigJsFile()
        {
            return projectContext =>
            {
                var jsFilePath = Instances.ProjectPathsOperator.GetTailwindConfigJsFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToJsFilePath();

                Instances.CodeFileGenerationOperations.Create_TailwindConfigJsFile(
                    jsFilePath);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_TailwindAllContentPathsJsonFile()
        {
            return projectContext =>
            {
                var jsonFilePath = Instances.ProjectPathsOperator.GetTailwindAllContentPathsJsonFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToJsonFilePath();

                Instances.CodeFileGenerationOperations.Create_TailwindCssAllContentPathsJsonFile(
                    jsonFilePath);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_TailwindContentPathsJsonFile()
        {
            return projectContext =>
            {
                var jsonFilePath = Instances.ProjectPathsOperator.GetTailwindContentPathsJsonFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToJsonFilePath();

                Instances.CodeFileGenerationOperations.Create_TailwindCssContentPathsJsonFile(
                    jsonFilePath);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_TailwindCssFile()
        {
            return projectContext =>
            {
                var cssFilePath = Instances.ProjectPathsOperator.GetTailwindCssFilePath(
                    projectContext.ProjectFilePath.Value)
                    .ToCssFilePath();

                Instances.CodeFileGenerationOperations.Create_TailwindCssFile(
                    cssFilePath);

                return Task.CompletedTask;
            };
        }
    }
}
