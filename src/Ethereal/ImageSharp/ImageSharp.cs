// Copyright (c) Ethereal. All rights reserved.

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace System.ImageSharp
{
    /// <summary>
    /// ImageSharp
    /// </summary>
    public class ImageSharp
    {
        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task MergeImageAsync(string inputPath, string mergePath, string outputPath, int x, int y)
        {
            using var image = await Image.LoadAsync(inputPath);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task<Stream> MergeImageAsync(string inputPath, string mergePath, int x, int y)
        {
            Stream stream = new MemoryStream();
            using var image = await Image.LoadAsync(inputPath);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(stream, image.DetectEncoder(inputPath));
            return stream;
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task MergeImageAsync(Stream inputStream, string mergePath, string outputPath, int x, int y)
        {
            using var image = await Image.LoadAsync(inputStream);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// <para>MergeImage</para>
        /// <code><para>var imageEncoder = new JpegEncoder();</para><para>var imageEncoder = new PngEncoder();</para><para>var imageEncoder = new GifEncoder();</para><para>var imageEncoder = new BmpEncoder();</para><para>var imageEncoder = new TgaEncoder();</para></code>
        /// </summary>
        public async Task<Stream> MergeImageAsync(Stream inputStream, string mergePath, int x, int y, IImageEncoder imageEncoder)
        {
            Stream stream = new MemoryStream();
            using var image = await Image.LoadAsync(inputStream);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(stream, imageEncoder);
            return stream;
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task MergeImageAsync(string inputPath, string mergePath, string outputPath, int x, int y, int scaling)
        {
            using var image = await Image.LoadAsync(inputPath);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(mergeImage.Height / scaling, mergeImage.Width / scaling));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task<Stream> MergeImageAsync(string inputPath, string mergePath, int x, int y, int scaling)
        {
            Stream stream = new MemoryStream();
            using var image = await Image.LoadAsync(inputPath);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(mergeImage.Height / scaling, mergeImage.Width / scaling));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(stream, image.DetectEncoder(inputPath));
            return stream;
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task MergeImageAsync(Stream inputStream, string mergePath, string outputPath, int x, int y, int scaling)
        {
            using var image = await Image.LoadAsync(inputStream);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(mergeImage.Height / scaling, mergeImage.Width / scaling));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// <para>MergeImage</para>
        /// <code><para>var imageEncoder = new JpegEncoder();</para><para>var imageEncoder = new PngEncoder();</para><para>var imageEncoder = new GifEncoder();</para><para>var imageEncoder = new BmpEncoder();</para><para>var imageEncoder = new TgaEncoder();</para></code>
        /// </summary>
        public async Task<Stream> MergeImageAsync(Stream inputStream, string mergePath, int x, int y, int scaling, IImageEncoder imageEncoder)
        {
            var stream = new MemoryStream();
            using var image = await Image.LoadAsync(inputStream);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(mergeImage.Height / scaling, mergeImage.Width / scaling));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(stream, imageEncoder);
            return stream;
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task MergeImageAsync(string inputPath, string mergePath, string outputPath, int x, int y, int height, int width)
        {
            using var image = await Image.LoadAsync(inputPath);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(height, width));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task<Stream> MergeImageAsync(string inputPath, string mergePath, int x, int y, int height, int width)
        {
            Stream stream = new MemoryStream();
            using var image = await Image.LoadAsync(inputPath);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(height, width));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(stream, image.DetectEncoder(inputPath));
            return stream;
        }

        /// <summary>
        /// MergeImage
        /// </summary>
        public async Task MergeImageAsync(Stream inputStream, string mergePath, string outputPath, int x, int y, int height, int width)
        {
            using var image = await Image.LoadAsync(inputStream);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(height, width));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// <para>MergeImage</para>
        /// <code><para>var imageEncoder = new JpegEncoder();</para><para>var imageEncoder = new PngEncoder();</para><para>var imageEncoder = new GifEncoder();</para><para>var imageEncoder = new BmpEncoder();</para><para>var imageEncoder = new TgaEncoder();</para></code>
        /// </summary>
        public async Task<Stream> MergeImageAsync(Stream inputStream, string mergePath, int x, int y, int height, int width, IImageEncoder imageEncoder)
        {
            Stream stream = new MemoryStream();
            using var image = await Image.LoadAsync(inputStream);
            image.Mutate(async o =>
            {
                var mergeImage = await Image.LoadAsync(mergePath);
                mergeImage.Mutate(m => m.Resize(height, width));
                o.DrawImage(mergeImage, new Point(x, y), 1);
            });
            await image.SaveAsync(stream, imageEncoder);
            return stream;
        }

        /// <summary>
        /// Resize
        /// </summary>
        public async Task ResizeAsync(string inputPath, string outputPath, int scaling = 3)
        {
            using var image = Image.Load(inputPath);
            image.Mutate(m => m.Resize(image.Height / scaling, image.Width / scaling));
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// Resize
        /// </summary>
        public async Task ResizeAsync(Stream inputStream, string outputPath, int scaling = 3)
        {
            using var image = Image.Load(inputStream);
            image.Mutate(m => m.Resize(image.Height / scaling, image.Width / scaling));
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// Resize
        /// </summary>
        public async Task ResizeAsync(string inputPath, string outputPath, int height, int width)
        {
            using var image = Image.Load(inputPath);
            image.Mutate(m => m.Resize(height, width));
            await image.SaveAsync(outputPath);
        }

        /// <summary>
        /// Resize
        /// </summary>
        public async Task ResizeAsync(Stream inputStream, string outputPath, int height, int width)
        {
            using var image = Image.Load(inputStream);
            image.Mutate(m => m.Resize(height, width));
            await image.SaveAsync(outputPath);
        }
    }
}