using Imaging.DDSReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapComparer.Model
{
    /// <summary>
    /// Perceptive hash: https://habr.com/ru/post/120562/. 
    /// 5\64 is good similarity for this algorithm.
    /// </summary>
    static class Hash
    {
        public static byte  Threshold   = 5;

        // TODO: SetPixel is slow - Rewrite with unsafe.
        public static BitArray Create (Bitmap bitmap)
        {
            var size    = 8;
            var map     = new Bitmap(bitmap, new Size(size, size));
            var scales  = new List<byte>();
            var hash    = new BitArray(size*size);

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var pixel = map.GetPixel(x, y);
                    var grayscale = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    scales.Add(grayscale);
                }
            }

            if (map != null) { map.Dispose(); }

            var average = 0;
            foreach (var color in scales)
            {
                average += color;
            }
            average = average / scales.Count;

            for (int i = 0; i < scales.Count; i++)
            {
                hash[i] = scales[i] >= average;
            }

            return hash;
        }

        private static Bitmap ToGrayscale (Bitmap source)
        {
            // Промежуточные переменные ускоряют код в несколько раз.
            var width       = source.Width;
            var height      = source.Height;

            var sourceData  = source.LockBits(new Rectangle(new Point(0, 0), source.Size),
                             ImageLockMode.ReadOnly, source.PixelFormat);

            var result      = new Bitmap(width, height, source.PixelFormat);
            var resultData  = result.LockBits(new Rectangle(new Point(0, 0), result.Size), 
                                ImageLockMode.ReadWrite, source.PixelFormat);

            var sourceStride = sourceData.Stride;
            var resultStride = resultData.Stride;

            var sourceScan0 = sourceData.Scan0;
            var resultScan0 = resultData.Scan0;

            var resultPixelSize = resultStride / width;

            unsafe
            {
                for (var y = 0; y < height; y++)
                {
                    var sourceRow   = (byte*)sourceScan0 + (y * sourceStride);
                    var resultRow   = (byte*)resultScan0 + (y * resultStride);
                    for (var x = 0; x < width; x++)
                    {
                        var v = (byte)(
                                0.3 * sourceRow[x * resultPixelSize + 2] 
                                + 0.59 * sourceRow[x * resultPixelSize + 1] 
                                + 0.11 * sourceRow[x * resultPixelSize]
                            );
                        resultRow[x * resultPixelSize]      = v;
                        resultRow[x * resultPixelSize + 1]  = v;
                        resultRow[x * resultPixelSize + 2]  = v;
                    }
                }
            }

            source.UnlockBits(sourceData);
            result.UnlockBits(resultData);
            return result;
        }

        public static byte CalcDifference (BitArray hash1, BitArray hash2)
        {
            byte differentBits = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])   { differentBits++; }
            }
            return differentBits;
        }

        public static bool IsSimilar (BitArray hash1, BitArray hash2)
        {
            byte differentBits = CalcDifference(hash1, hash2);
            return  differentBits < Threshold;
        }
    }


}
