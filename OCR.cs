using Tesseract;

namespace Audiobook_Maker
{
    public static class OCR
    {
        public static string GetTextFromImage(string imageFilePath)
        {
            TesseractEngine engine = new TesseractEngine(@"C:\Users\RandomiaGaming\Desktop\tessdata", "eng", EngineMode.Default);
            Pix img = Pix.LoadFromFile(imageFilePath);
            Page page = engine.Process(img);
            return page.GetText();
        }
    }
}