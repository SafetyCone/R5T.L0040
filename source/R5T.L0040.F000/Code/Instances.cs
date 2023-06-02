using System;


namespace R5T.L0040.F000
{
    public static class Instances
    {
        public static F0000.IActionOperations ActionOperations => F0000.ActionOperations.Instance;
        public static L0031.IContextOperator ContextOperator => L0031.ContextOperator.Instance;
        public static IProjectContextConstructor ProjectContextConstructor => F000.ProjectContextConstructor.Instance;
        public static IProjectContextConstructors ProjectContextConstructors => F000.ProjectContextConstructors.Instance;
        public static F0130.IProjectPathConventions ProjectPathConventions => F0130.ProjectPathConventions.Instance;
    }
}