namespace my_Ecommerce_App.Utils;

public static class Image
{

    public static byte[]? IFormFileToByteArray(IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            using MemoryStream memoryStream = new();
            imageFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        return null;

    }

    // public static byte[]? ByteArrToIFormFile(byte[]? ByteArr)
    // {

    // }

    public static string? ByteArrayToSrc(byte[]? ByteArr)
    {
        string base64String = Convert.ToBase64String(ByteArr!);

        string imgSrc = $"data:image/jpeg;base64,{base64String}";
        return imgSrc;
    }

}