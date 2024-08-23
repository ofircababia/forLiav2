namespace Web_Api.Classes
{
    public class Upload
    {
        public string Uploadhandel(IFormFile file)
        {
            List<string> validEXs = new List<string> { ".jpg", ".png", ".gif" };
            string exstention = Path.GetExtension(file.FileName);
            if (validEXs.Contains(exstention))
            {
                return $"not valid({string.Join(',', validEXs)})";
            }

            long size = file.Length;
            if (size > 05 * 1024 * 1024)
                return "max size";

            string fileName = Guid.NewGuid().ToString() + exstention;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder");

            using FileStream stream = new FileStream(path + fileName, FileMode.Create);

            file.CopyTo(stream);

            return fileName;
        }
    }

}

