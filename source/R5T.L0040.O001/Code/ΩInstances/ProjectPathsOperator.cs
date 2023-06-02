using System;


namespace R5T.L0040.O001
{
    public class ProjectPathsOperator : IProjectPathsOperator
    {
        #region Infrastructure

        public static IProjectPathsOperator Instance { get; } = new ProjectPathsOperator();


        private ProjectPathsOperator()
        {
        }

        #endregion
    }
}
