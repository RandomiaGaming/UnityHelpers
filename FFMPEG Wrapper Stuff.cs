using System;
using System.IO;
using Accord.Video.FFMPEG;
using System.Drawing;
using System.Drawing.Imaging;

namespace VideoScroll
{
    public static class VideoScroll
    {
        public enum ScrollDirection { Down, Up, Left, Right };
        public static void UnscrollVideoToImage(string inputPath, string outputPath, ScrollDirection scrollDirection, int maxScrollDistancePerFrame, ImageFormat outputFormat, bool overwriteExisting)
        {
            if (!File.Exists(inputPath))
            {
                throw new Exception("inputPath was invalid or does not exist.");
            }

            if (File.Exists(outputPath) && !overwriteExisting)
            {
                throw new Exception("outputPath overwrites an existing file.");
            }

            if (outputFormat is null)
            {
                throw new Exception("outputFormat must be a valid image format. We recomend using System.Drawing.Imaging.ImageFormat.Png.");
            }

            if (maxScrollDistancePerFrame <= 0)
            {
                throw new Exception("maxScrollDistancePerFrame must be greater than 0.");
            }

            VideoFileReader inputReader = new VideoFileReader();
            inputReader.Open(inputPath);

            if (maxScrollDistancePerFrame >= inputReader.Height)
            {
                throw new Exception("maxScrollDistancePerFrame must be less than the video height.");
            }

            Bitmap output = inputReader.ReadVideoFrame(0);

            for (int i = 1; i < inputReader.FrameCount; i++)
            {
                Bitmap frame = inputReader.ReadVideoFrame(i);
                for (int o = 1; o <= maxScrollDistancePerFrame; o++)
                {
                    if (true /* Lines up with offset o*/)
                    {
                        Bitmap newOutput = new Bitmap(output.Height, output.Height + o);
                        newOutput.UnlockBits(output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.ReadOnly, PixelFormat.Undefined), );

                    }
                }
            }

            inputReader.Close();
            inputReader.Dispose();
        }
        public static void ThinVideo(string inputPath, string outputPath, int frameskipCount, bool overwriteExisting)
        {
            if (!File.Exists(inputPath))
            {
                throw new Exception("inputPath was invalid or does not exist.");
            }

            if (File.Exists(outputPath) && !overwriteExisting)
            {
                throw new Exception("outputPath overwrites an existing file.");
            }

            if (frameskipCount < 0)
            {
                throw new Exception("frameskipCount must be greater than or equal to 0.");
            }

            if (frameskipCount == 0)
            {
                File.Copy(inputPath, outputPath, true);
                return;
            }

            VideoFileReader inputReader = new VideoFileReader();
            inputReader.Open(inputPath);

            VideoFileWriter outputWriter = new VideoFileWriter();
            outputWriter.Open(outputPath, inputReader.Width, inputReader.Height, (Accord.Math.Rational)(((double)inputReader.FrameRate) / (frameskipCount + 1.0)), VideoCodec.MPEG4);

            int currentFrameskipCount = 0;
            for (int i = 0; i < inputReader.FrameCount; i++)
            {
                if (currentFrameskipCount < frameskipCount)
                {
                    currentFrameskipCount++;
                }
                else if (currentFrameskipCount >= frameskipCount)
                {
                    currentFrameskipCount = 0;

                    Bitmap frame = inputReader.ReadVideoFrame(i);
                    outputWriter.WriteVideoFrame(frame);
                    frame.Dispose();

                    Console.WriteLine($"Finished frame {i} of {inputReader.FrameCount}.");
                }
            }

            outputWriter.Close();
            outputWriter.Dispose();

            inputReader.Close();
            inputReader.Dispose();
        }
        public static void CropVideo(string inputPath, Rectangle selectionRect, string outputPath, bool overwriteExisting)
        {
            if (!File.Exists(inputPath))
            {
                throw new Exception("inputPath was invalid or does not exist.");
            }

            if (File.Exists(outputPath) && !overwriteExisting)
            {
                throw new Exception("outputPath overwrites an existing file.");
            }

            VideoFileReader inputReader = new VideoFileReader();
            inputReader.Open(inputPath);

            if (selectionRect.X < 0 || selectionRect.Y < 0 || selectionRect.Y + selectionRect.Height >= inputReader.Height || selectionRect.X + selectionRect.Width >= inputReader.Width)
            {
                throw new Exception("selectionRect extended beyond the boarder of the video.");
            }

            VideoFileWriter outputWriter = new VideoFileWriter();
            outputWriter.Open(outputPath, selectionRect.Width, selectionRect.Height, inputReader.FrameRate, VideoCodec.MPEG4);

            for (int i = 0; i < inputReader.FrameCount; i++)
            {
                Bitmap frame = inputReader.ReadVideoFrame(i);
                Bitmap croppedFrame = frame.Clone(selectionRect, frame.PixelFormat);
                outputWriter.WriteVideoFrame(croppedFrame);
                frame.Dispose();
                croppedFrame.Dispose();

                Console.WriteLine($"Finished frame {i} of {inputReader.FrameCount}.");
            }

            outputWriter.Close();
            outputWriter.Dispose();

            inputReader.Close();
            inputReader.Dispose();
        }
    }
}
