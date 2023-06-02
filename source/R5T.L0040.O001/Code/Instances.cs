using System;


namespace R5T.L0040.O001
{
    public static class Instances
    {
        public static O0008.ICodeFileGenerationOperations CodeFileGenerationOperations => O0008.CodeFileGenerationOperations.Instance;
        public static IProjectPathsOperator ProjectPathsOperator => O001.ProjectPathsOperator.Instance;
    }
}