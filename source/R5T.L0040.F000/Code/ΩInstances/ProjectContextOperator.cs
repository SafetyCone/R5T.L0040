using System;


namespace R5T.L0040.F000
{
    public class ProjectContextOperator : IProjectContextOperator
    {
        #region Infrastructure

        public static IProjectContextOperator Instance { get; } = new ProjectContextOperator();


        private ProjectContextOperator()
        {
        }

        #endregion
    }
}
