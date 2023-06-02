using System;

using R5T.T0131;
using R5T.T0172;
using R5T.T0187;

using R5T.L0040.T000;


namespace R5T.L0040.F000
{
    [ValuesMarker]
    public partial interface IProjectContextConstructors : IValuesMarker
    {
        public Func<IProjectContext> Default(
            IProjectName projectName,
            ISolutionDirectoryPath solutionDirectoryPath)
        {
            return () => Instances.ProjectContextConstructor.Default(
                projectName,
                solutionDirectoryPath);
        }
    }
}
