using System;


namespace R5T.L0040
{
    public static class Instances
    {
        public static F0000.IActionOperations ActionOperations => F0000.ActionOperations.Instance;
        public static F0053.ICodeFileGenerator CodeFileGenerator => F0053.CodeFileGenerator.Instance;
        public static L0031.IContextOperator ContextOperator => L0031.ContextOperator.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator => F0000.FileSystemOperator.Instance;
        public static IProjectContextOperations ProjectContextOperations => L0040.ProjectContextOperations.Instance;
        public static O001.IProjectContextOperations ProjectContextOperations_FileGeneration => O001.ProjectContextOperations.Instance;
        public static Internal.IProjectContextOperator ProjectContextOperator_Internal => Internal.ProjectContextOperator.Instance;
        public static F0056.IProjectOperations ProjectOperations => F0056.ProjectOperations.Instance;
        public static L0033.IProjectFileContextOperator ProjectFileContextOperator => L0033.ProjectFileContextOperator.Instance;
        public static F0020.IProjectFileOperator ProjectFileOperator => F0020.ProjectFileOperator.Instance;
        public static Z0045.IProjectFileReferences ProjectFileReferences => Z0045.ProjectFileReferences.Instance;
        public static O001.IProjectPathsOperator ProjectPathsOperator => O001.ProjectPathsOperator.Instance;
        public static F0132.IProjectNamespaceNamesOperator ProjectNamespaceNamesOperator => F0132.ProjectNamespaceNamesOperator.Instance;
        public static F0024.ISolutionFileOperator SolutionFileOperator => F0024.SolutionFileOperator.Instance;
        public static F0063.ISolutionOperations SolutionOperations => F0063.SolutionOperations.Instance;
        public static F0054.ITextFileGenerator TextFileGenerator => F0054.TextFileGenerator.Instance;
    }
}