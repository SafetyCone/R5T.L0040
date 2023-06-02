using System;

using R5T.T0132;
using R5T.T0172;
using R5T.T0187;

using R5T.L0040.T000;


namespace R5T.L0040.F000
{
    [FunctionalityMarker]
    public partial interface IProjectContextConstructor : IFunctionalityMarker
    {
        public IProjectContext Default(
            IProjectName projectName,
            ISolutionDirectoryPath solutionDirectoryPath)
        {
            var projectFilePath = Instances.ProjectPathConventions.Get_ProjectFilePath(
                solutionDirectoryPath,
                projectName);

            var projectContext = new ProjectContext
            {
                ProjectName = projectName,
                ProjectFilePath = projectFilePath,
            };

            return projectContext;
    }
    }
}
