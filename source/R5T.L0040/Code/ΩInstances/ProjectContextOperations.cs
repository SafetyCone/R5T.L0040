using System;


namespace R5T.L0040
{
    public class ProjectContextOperations : IProjectContextOperations
    {
        #region Infrastructure

        public static IProjectContextOperations Instance { get; } = new ProjectContextOperations();


        private ProjectContextOperations()
        {
        }

        #endregion
    }
}
