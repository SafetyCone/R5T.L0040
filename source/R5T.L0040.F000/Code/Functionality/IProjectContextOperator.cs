using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0159;
using R5T.T0172;
using R5T.T0187;

using R5T.L0040.T000;
using System.Linq;

namespace R5T.L0040.F000
{
    [FunctionalityMarker]
    public partial interface IProjectContextOperator : IFunctionalityMarker
    {
        public Task In_ProjectContext(
            IProjectContext projectContext,
            IEnumerable<Func<IProjectContext, Task>> operations)
        {
            return Instances.ContextOperator.In_Context(
                projectContext,
                operations);
        }

        public Task In_ProjectContext(
            Func<IProjectContext> projectContextConstructor,
            IEnumerable<Func<IProjectContext, Task>> operations)
        {
            return Instances.ContextOperator.In_Context(
                projectContextConstructor,
                operations);
        }

        public async Task In_ProjectContext(
            IProjectFilePath projectFilePath,
            IProjectName projectName,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            await Instances.ContextOperator.In_Context(
                () => new ProjectContext
                {
                    ProjectFilePath = projectFilePath,
                    ProjectName = projectName,
                    TextOutput = textOutput,
                },
                operations,
                Instances.ActionOperations.DoNothing_Synchronous);
        }

        public async Task In_ProjectContext(
            IProjectName projectName,
            ISolutionDirectoryPath solutionDirectoryPath,
            ITextOutput textOutput,
            IEnumerable<Func<IProjectContext, Task>> operations)
        {
            await Instances.ContextOperator.In_Context(
                Instances.ProjectContextConstructors.Default(
                    projectName,
                    solutionDirectoryPath),
                operations,
                Instances.ActionOperations.DoNothing_Synchronous);
        }

        public Task In_ProjectContext(
            IProjectName projectName,
            ISolutionDirectoryPath solutionDirectoryPath,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            return this.In_ProjectContext(
                projectName,
                solutionDirectoryPath,
                textOutput,
                operations.AsEnumerable());
        }
    }
}
