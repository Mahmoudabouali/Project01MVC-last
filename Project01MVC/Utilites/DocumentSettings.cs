namespace Mvc.PresentationLayer.Utilites
{
    public static class DocumentSettings 
    {
        public static async Task<string> UplodeFile(IFormFile file , string folderName)
        {
            //create folderpath
            // //E:/NewFolder/MvcDemoSln/Demo.PL/wwwroot/Files/folderName
            //string folderpath = Directory.GetCurrentDirectory() + @"\wwwroot\Files";
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName);
            //create unique name for file
            string filename = $"{Guid.NewGuid}-{file.FileName}";
            //create filr path
            // //E:/NewFolder/MvcDemoSln/Demo.PL/wwwroot/Files/folderName/Image.png
            string filepath = Path.Combine(folderpath, filename);
            //craete fileStream to save File
            using var stream = new FileStream(filepath,FileMode.Create);
            // copy file to FileStream
            await file.CopyToAsync(stream);

            return filename;
        }

        public static void DeleteFile(string folderName , string filename)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName,filename);

            if(File.Exists(filepath))
                File.Delete(filepath);
        }
    }
}
