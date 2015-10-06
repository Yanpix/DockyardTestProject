namespace BLL
{
    public static class FileExtensions
    {
        public static string GetIconByMimeType(this string mymeType)
        {
            return mymeType == "folder" ? "glyphicon glyphicon-folder-close" : "glyphicon glyphicon-file";
        }
    }
}
