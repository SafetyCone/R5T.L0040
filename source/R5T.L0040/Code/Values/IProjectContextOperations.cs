using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.L0031.Extensions;
using R5T.L0033.T000;
using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0161;
using R5T.T0172;
using R5T.T0187;
using R5T.T0195;

using R5T.L0040.T000;


namespace R5T.L0040
{
    [ValuesMarker]
    public partial interface IProjectContextOperations : IValuesMarker
    {
        /// <summary>
        /// Simply adds project file references to a project.
        /// </summary>
        public Func<IProjectContext, Task> Add_ProjectFileReferences(
            IEnumerable<IProjectFileReference> projectFileReferences)
        {
            // Evaluate the enumerable now.
            var projectFileReferenceValues = projectFileReferences
                .Select(x => x.Value)
                .Now();

            return projectContext =>
            {
                Instances.ProjectFileOperator.AddProjectReferences_Idempotent_Synchronous(
                    projectContext.ProjectFilePath.Value,
                    projectFileReferenceValues);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Add_ProjectFileReferences(
            params IProjectFileReference[] projectFileReferences)
        {
            return this.Add_ProjectFileReferences(
                projectFileReferences.AsEnumerable());
        }

        /// <summary>
        /// Adds project file references to a project, then updates all containing solutions by adding all recursive project references.
        /// </summary>
        public Func<IProjectContext, Task> Add_ProjectFileReferences_AndUpdateContainingSolutions(
            IEnumerable<IProjectFileReference> projectFileReferences)
        {
            return async projectContext =>
            {
                await projectContext.Run(
                    this.Add_ProjectFileReferences(projectFileReferences)
                );

                await Instances.ProjectOperations.UpdateSolutions(projectContext.ProjectFilePath.Value);
            };
        }

        public Func<IProjectContext, Task> Add_ProjectFileReferences_AndUpdateContainingSolutions(
            params IProjectFileReference[] projectFileReferences)
        {
            return this.Add_ProjectFileReferences_AndUpdateContainingSolutions(
                projectFileReferences.AsEnumerable());
        }

        public delegate Func<IProjectContext, Task> In_New_ProjectFileContext_Params(
            params Func<IProjectFileContext, Task>[] operations);

        /// <inheritdoc cref="L0033.IProjectFileContextOperator.In_New_ProjectFileContext(IProjectFilePath, IEnumerable{Func{IProjectFileContext, Task}})"/>
        public In_New_ProjectFileContext_Params In_New_ProjectFileContext =>
            operations =>
                context => Instances.ProjectFileContextOperator.In_New_ProjectFileContext(
                context.ProjectFilePath,
                operations);

        public delegate Func<IProjectContext, Task> In_New_ProjectFileContext_Params_Action(
            params Action<IProjectFileContext>[] operations);

        public In_New_ProjectFileContext_Params_Action In_New_ProjectFileContext_Action =>
            operations =>
                context => Instances.ProjectFileContextOperator.In_New_ProjectFileContext_Task(
                context.ProjectFilePath,
                operations);

        public Func<IProjectContext, Task> Create_New_Project(
            ISolutionContext solutionContext,
            Func<IProjectFileContext, Task> createNewProjectFileOperation,
            Func<IProjectContext, Task> createNewProjectOperation,
            Func<IProjectFilePath, Task> projectFilePathHandler)
        {
            return projectContext => projectContext.Run(
                this.In_New_ProjectFileContext(
                    createNewProjectFileOperation),
                createNewProjectOperation,
                projectContext => projectFilePathHandler(projectContext.ProjectFilePath),
                this.Add_ToSolution(
                    solutionContext.SolutionFilePath)
            );
        }

        public Func<IProjectContext, Task> Create_New_Project(
            ISolutionContext solutionContext,
            Func<IProjectFileContext, Task> createNewProjectFileOperation,
            Func<IProjectContext, Task> createNewProjectOperation,
            IHasProjectFilePath hasProjectFilePath)
        {
            return this.Create_New_Project(
                solutionContext,
                createNewProjectFileOperation,
                createNewProjectOperation,
                projectFilePath =>
                {
                    hasProjectFilePath.ProjectFilePath = projectFilePath;

                    return Task.CompletedTask;
                });
        }

        public Func<IProjectContext, Task> Create_New_Project(
            Func<IProjectFileContext, Task> createNewProjectFileOperation,
            Func<IProjectFilePath, Task> projectFilePathHandler,
            Func<IProjectContext, Task> createNewProjectOperation)
        {
            return projectContext => projectContext.Run(
                this.In_New_ProjectFileContext(
                    createNewProjectFileOperation),
                createNewProjectOperation,
                projectContext => projectFilePathHandler(projectContext.ProjectFilePath)
            );
        }

        /// <summary>
        /// Because the is a 'new' method, it will throw if the project file already exists.
        /// </summary>
        public Func<IProjectContext, Task> Create_New_Project(
            Func<IProjectFileContext, Task> createNewProjectFileOperation,
            IHasProjectFilePath hasProjectFilePath,
            Func<IProjectContext, Task> createNewProjectOperation)
        {
            return projectContext => projectContext.Run(
                // Create the project file.
                this.In_New_ProjectFileContext(
                    createNewProjectFileOperation),
                // Handle the project file path.
                projectContext =>
                {
                    hasProjectFilePath.ProjectFilePath = projectContext.ProjectFilePath;

                    return Task.CompletedTask;
                },
                // Create the project.
                createNewProjectOperation
            );
        }

        /// <summary>
        /// Adds a project to the solution, and adds all recursive project references as well.
        /// </summary>
        public Func<IProjectContext, Task> Add_ToSolution(
            ISolutionFilePath solutionFilePath,
            bool addRecursiveProjectReferences = true)
        {
            Task Internal(IProjectContext context)
            {
                Instances.SolutionFileOperator.AddProject(
                    solutionFilePath.Value,
                    context.ProjectFilePath.Value);

                var output = addRecursiveProjectReferences
                    ? Instances.SolutionOperations.AddAllRecursiveProjectReferenceDependencies(
                        solutionFilePath.Value)
                    : Task.CompletedTask
                    ;

                return output;
            }

            return Internal;
        }

        public Func<IProjectContext, Task> Create_ProjectPlanFile(
            IProjectName projectName,
            IProjectDescription projectDescription)
        {
            return context =>
            {
                var projectPlanFilePath = Instances.ProjectPathsOperator.GetProjectPlanMarkdownFilePath(
                    context.ProjectFilePath.Value);

                Instances.TextFileGenerator.CreateProjectPlanMarkdownFile(
                    projectPlanFilePath,
                    projectName.Value,
                    projectDescription.Value);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_InstancesFile(
            INamespaceName projectNamespaceName)
        {
            return context =>
            {
                var instanceFilePath = Instances.ProjectPathsOperator.GetInstancesFilePath(
                    context.ProjectFilePath.Value);

                Instances.CodeFileGenerator.CreateInstancesFile(
                    instanceFilePath,
                    projectNamespaceName.Value);

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Create_DocumentationFile(
            INamespaceName projectNamespaceName,
            IProjectDescription projectDescription)
        {
            return context =>
            {
                var documentationFilePath = Instances.ProjectPathsOperator.GetDocumentationFilePath(
                    context.ProjectFilePath.Value);

                Instances.CodeFileGenerator.CreateDocumentationFile(
                    documentationFilePath,
                    projectNamespaceName.Value,
                    projectDescription.Value);

                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// The default console project creation operation.
        /// Does not create the project file, just the project's files.
        /// </summary>
        /// <returns></returns>
        public Func<IProjectContext, Task> Create_ConsoleProject(
            IProjectName projectName,
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                projectContext.Run(
                    this.Create_ProjectPlanFile(
                projectName,
                        projectDescription),
                    this.Create_InstancesFile(
                        projectNamespaceName),
                    this.Create_DocumentationFile(
                        projectNamespaceName,
                        projectDescription),
                    Create_ProgramFile
                );

                Task Create_ProgramFile(
                    IProjectContext _)
                {
                    var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(
                        projectContext.ProjectFilePath.Value);

                    Instances.CodeFileGenerator.CreateProgramFile(
                        programFilePath,
                        projectNamespaceName.Value);

                    return Task.CompletedTask;
                }

                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// The default library project creation operation.
        /// Does not create the project file, just the project's files.
        /// </summary>
        /// <returns></returns>
        public Func<IProjectContext, Task> Create_LibraryProject(
            IProjectName projectName,
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                projectContext.Run(
                    this.Create_ProjectPlanFile(
                        projectName,
                        projectDescription),
                    this.Create_InstancesFile(
                        projectNamespaceName),
                    this.Create_DocumentationFile(
                        projectNamespaceName,
                        projectDescription),
                    this.Add_ProjectFileReferences(
                        Instances.ProjectFileReferences.For_NET_6_FoundationLibrary)
                );

                return Task.CompletedTask;
            };
        }

        public Func<IProjectContext, Task> Set_ProjectFilePath(IHasProjectFilePath hasProjectFilePath)
        {
            return context =>
            {
                hasProjectFilePath.ProjectFilePath = context.ProjectFilePath;

                return Task.CompletedTask;
            };
        }
    }
}
