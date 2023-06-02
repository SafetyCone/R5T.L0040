using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0159;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0187;
using R5T.T0187.Extensions;

using R5T.L0040.T000;


namespace R5T.L0040
{
    [FunctionalityMarker]
    public partial interface IProjectContextOperator : IFunctionalityMarker,
        F000.IProjectContextOperator
    {
        /// <summary>
        /// Because this is a 'new' method, it will throw an exception if the project file already exists.
        /// </summary>
        public Task In_New_ProjectContext(
            IProjectFilePath projectFilePath,
            IProjectName projectName,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            Instances.FileSystemOperator.VerifyFileDoesNotExists(projectFilePath.Value);

            return this.In_ProjectContext(
                projectFilePath,
                projectName,
                textOutput,
                operations);
        }

        public Task In_New_ProjectContext(
            IProjectDirectoryPath projectDirectoryPath,
            IProjectName projectName,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            var projectFilePath = Instances.ProjectPathsOperator.Get_ProjectFilePath(
                projectDirectoryPath,
                projectName);

            return this.In_New_ProjectContext(
                projectFilePath,
                projectName,
                textOutput,
                operations);
        }

        /// <summary>
        /// Because this is a 'modify' method, it will throw an exception if the project file does not exist.
        /// </summary>
        public Task In_Modify_ProjectContext(
            IProjectFilePath projectFilePath,
            IProjectName projectName,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            Instances.FileSystemOperator.VerifyFileExists(projectFilePath.Value);

            return this.In_ProjectContext(
                projectFilePath,
                projectName,
                textOutput,
                operations);
        }

        /// <inheritdoc cref="In_Modify_ProjectContext(IProjectFilePath, IProjectName, ITextOutput, Func{IProjectContext, Task}[])"/>
        public Task In_Modify_ProjectContext(
            IProjectFilePath projectFilePath,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            var projectName = Instances.ProjectPathsOperator.Get_ProjectName(
                projectFilePath);

            return this.In_Modify_ProjectContext(
                projectFilePath,
                projectName,
                textOutput,
                operations);
        }

        public Task In_ProjectContext(
            IProjectDirectoryPath projectDirectoryPath,
            IProjectName projectName,
            ITextOutput textOutput,
            params Func<IProjectContext, Task>[] operations)
        {
            var projectFilePath = Instances.ProjectPathsOperator.Get_ProjectFilePath(
                projectDirectoryPath,
                projectName);

            return this.In_ProjectContext(
                projectFilePath,
                projectName,
                textOutput,
                operations);
        }
    }
}
