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
    public partial interface IProjectContextOperations : IValuesMarker,
        O002.IProjectContextOperations
    {
        /// <summary>
        /// Simply adds project file references to a project.
        /// </summary>
        public Func<IProjectContext, Task> Add_ProjectFileReferences(
            IEnumerable<IProjectFilePath> projectFilePaths)
        {
            // Evaluate the enumerable now.
            var projectFileReferenceValues = projectFilePaths
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
            params IProjectFilePath[] projectFilePaths)
        {
            return this.Add_ProjectFileReferences(
                projectFilePaths.AsEnumerable());
        }

        public Func<IProjectContext, Task> Add_ProjectFileReference(
            IProjectFilePath projectFilePath)
        {
            return this.Add_ProjectFileReferences(
                projectFilePath);
        }

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

        /// <inheritdoc cref="L0033.IProjectFileContextOperator.In_New_ProjectFileContext(IProjectFilePath, IEnumerable{Func{IProjectFileContext, Task}})"/>
        public Func<IProjectContext, Task> In_New_ProjectFileContext(
            params Func<IProjectFileContext, Task>[] operations)
            =>
                context => Instances.ProjectFileContextOperator.In_New_ProjectFileContext(
                    context.ProjectFilePath,
                    operations);

        public Func<IProjectContext, Task> In_New_ProjectFileContext_Action(
            params Action<IProjectFileContext>[] operations)
            =>
                context => Instances.ProjectFileContextOperator.In_New_ProjectFileContext_Task(
                context.ProjectFilePath,
                operations);

        public Func<IProjectContext, Task> Create_New_Project(
            ISolutionContext solutionContext,
            Func<IProjectFileContext, Task> setupProjectFileOperation,
            Func<IProjectContext, Task> setupProjectOperation,
            Func<IProjectFilePath, Task> projectFilePathHandler = default)
        {
            return projectContext => projectContext.Run(
                this.In_New_ProjectFileContext(
                    setupProjectFileOperation),
                setupProjectOperation,
                projectContext => Instances.ActionOperator.Run(
                    projectFilePathHandler,
                    projectContext.ProjectFilePath
                ),
                this.Add_ToSolution(
                    solutionContext.SolutionFilePath)
            );
        }

        public Func<IProjectContext, Task> Create_New_Project(
            ISolutionContext solutionContext,
            Func<IProjectFileContext, Task> setupProjectFileOperation,
            Func<IProjectContext, Task> setupProjectOperation,
            IWithProjectFilePath withProjectFilePath)
        {
            return this.Create_New_Project(
                solutionContext,
                setupProjectFileOperation,
                setupProjectOperation,
                projectFilePath =>
                {
                    withProjectFilePath.ProjectFilePath = projectFilePath;

                    return Task.CompletedTask;
                });
        }

        /// <summary>
        /// Creates a new project file using the provided creation operation, then sets up the new project using the provided setup operation.
        /// <inheritdoc cref="In_New_ProjectFileContext(Func{IProjectFileContext, Task}[])"/>
        /// </summary>
        public Func<IProjectContext, Task> Create_New_Project(
            Func<IProjectFileContext, Task> setupProjectFileOperation,
            Func<IProjectContext, Task> setupProjectOperation,
            Func<IProjectFilePath, Task> projectFilePathHandler = default)
        {
            return projectContext => projectContext.Run(
                this.In_New_ProjectFileContext(
                    setupProjectFileOperation),
                setupProjectOperation,
                projectContext => Instances.ActionOperator.Run(
                    projectFilePathHandler,
                    projectContext.ProjectFilePath
                )
            );
        }

        /// <summary>
        /// Because the is a 'new' method, it will throw if the project file already exists.
        /// </summary>
        public Func<IProjectContext, Task> Create_New_Project(
            Func<IProjectFileContext, Task> setupProjectFileOperation,
            Func<IProjectContext, Task> setupProjectOperation,
            IWithProjectFilePath withProjectFilePath)
        {
            return projectContext => projectContext.Run(
                // Create the project file.
                this.In_New_ProjectFileContext(
                    setupProjectFileOperation),
                // Handle the project file path.
                projectContext =>
                {
                    withProjectFilePath.ProjectFilePath = projectContext.ProjectFilePath;

                    return Task.CompletedTask;
                },
                // Create the project.
                setupProjectOperation
            );
        }

        /// <summary>
        /// Adds a project to the solution, and adds all recursive project references as well.
        /// </summary>
        public Func<IProjectContext, Task> Add_ToSolution(
            ISolutionFilePath solutionFilePath,
            bool addRecursiveProjectReferences = F0063.IValues.Default_AddRecursiveProjectReferences_Constant)
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

        /// <summary>
        /// The default console project setup operation.
        /// Does not create the project file, just sets up the project's files.
        /// </summary>
        /// <returns></returns>
        public Func<IProjectContext, Task> Setup_ConsoleProject(
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_ConsoleProject(
                    projectContext,
                    projectDescription,
                    projectNamespaceName);
            };
        }

        public Func<IProjectContext, Task> Setup_BlazorClient(
            IProjectDescription projectDescription)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_BlazorClient(
                    projectContext,
                    projectDescription);
            };
        }

        public Func<IProjectContext, Task> Setup_RazorClassLibrary(
            IProjectDescription projectDescription)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_RazorClassLibrary(
                    projectContext,
                    projectDescription);
            };
        }

        public Func<IProjectContext, Task> Setup_WebServerForBlazorClient(
            IProjectDescription projectDescription)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_WebServerForBlazorClient(
                    projectContext,
                    projectDescription);
            };
        }

        public Func<IProjectContext, Task> Setup_ConsoleProject(
            IProjectDescription projectDescription)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_ConsoleProject(
                    projectContext,
                    projectDescription);
            };
        }

        /// <summary>
        /// The default library project setup operation.
        /// Does not create the project file, just sets up the project's files.
        /// </summary>
        /// <returns></returns>
        public Func<IProjectContext, Task> Setup_LibraryProject(
            IProjectDescription projectDescription,
            INamespaceName projectNamespaceName)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_LibraryProject(
                    projectContext,
                    projectDescription,
                    projectNamespaceName);
            };
        }

        public Func<IProjectContext, Task> Setup_LibraryProject(
           IProjectDescription projectDescription)
        {
            return projectContext =>
            {
                return Instances.ProjectContextOperator_Internal.Setup_LibraryProject(
                    projectContext,
                    projectDescription);
            };
        }

        public Func<IProjectContext, Task> Set_ProjectFilePath(IWithProjectFilePath withProjectFilePath)
        {
            return context =>
            {
                withProjectFilePath.ProjectFilePath = context.ProjectFilePath;

                return Task.CompletedTask;
            };
        }
    }
}
