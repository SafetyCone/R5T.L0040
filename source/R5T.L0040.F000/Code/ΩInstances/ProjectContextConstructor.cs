using System;


namespace R5T.L0040.F000
{
    public class ProjectContextConstructor : IProjectContextConstructor
    {
        #region Infrastructure

        public static IProjectContextConstructor Instance { get; } = new ProjectContextConstructor();


        private ProjectContextConstructor()
        {
        }

        #endregion
    }
}
