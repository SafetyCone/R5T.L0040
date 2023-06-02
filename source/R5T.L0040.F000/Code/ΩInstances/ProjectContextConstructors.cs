using System;


namespace R5T.L0040.F000
{
    public class ProjectContextConstructors : IProjectContextConstructors
    {
        #region Infrastructure

        public static IProjectContextConstructors Instance { get; } = new ProjectContextConstructors();


        private ProjectContextConstructors()
        {
        }

        #endregion
    }
}
