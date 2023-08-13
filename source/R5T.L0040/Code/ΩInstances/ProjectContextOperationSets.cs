using System;


namespace R5T.L0040
{
    public class ProjectContextOperationSets : IProjectContextOperationSets
    {
        #region Infrastructure

        public static IProjectContextOperationSets Instance { get; } = new ProjectContextOperationSets();


        private ProjectContextOperationSets()
        {
        }

        #endregion
    }
}
