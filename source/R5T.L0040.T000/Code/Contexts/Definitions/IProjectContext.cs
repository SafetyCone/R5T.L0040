using System;

using R5T.T0137;
using R5T.T0172;
using R5T.T0187;
using R5T.T0194;


namespace R5T.L0040.T000
{
    /// <summary>
    /// .NET project context.
    /// </summary>
    [ContextTypeMarker]
    public interface IProjectContext : IContextDefinitionMarker,
        ITextOutputtedContext
    {
        public IProjectName ProjectName { get; }
        public IProjectFilePath ProjectFilePath { get; }
    }
}