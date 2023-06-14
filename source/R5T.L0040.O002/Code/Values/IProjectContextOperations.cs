using System;
using System.Threading.Tasks;

using R5T.F0078;
using R5T.T0131;

using R5T.L0040.T000;


namespace R5T.L0040.O002
{
    [ValuesMarker]
    public partial interface IProjectContextOperations : IValuesMarker
    {
        public Func<IProjectContext, Task> Run_NpmInstall()
        {
            return async projectContext =>
            {
                var projectDirectoryPath = Instances.ProjectPathsOperator.Get_ProjectDirectoryPath(
                    projectContext.ProjectFilePath);

                await CliWrap.Cli.Wrap("npm")
                    .WithArguments("install -y")
                    .WithWorkingDirectory(projectDirectoryPath.Value)
                    .WithConsoleOutput()
                    .WithConsoleError()
                    .ExecuteAsync();
            };
        }
    }
}
