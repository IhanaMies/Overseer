using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace Overseer
{
    static class PngDecoder
    {
        public static BitmapSource GetIconFromPath(string inPath)
        {
            Stream imageStreamSource = new FileStream(inPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            return decoder.Frames[0];
        }

        public static /*BitmapSource*/ void GetIconFromStream(MemoryStream inStream)
        {
            //Stream imageStreamSource = inStream.;
            //PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //BitmapSource bitmap;

            //return bitmap;
        }
    }
}
