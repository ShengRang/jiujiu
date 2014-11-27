using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace JiujiuXiaohantu
{
    public class ImgHelper
    {
        public static byte[] ImageToByteArray(BitmapImage imageSource)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap(imageSource);

                // write an image into the stream
                Extensions.SaveJpeg(btmMap, ms,
                    imageSource.PixelWidth, imageSource.PixelHeight, 0, 100);

                return ms.ToArray();
            }
        }
        public static Image ByteArrayToImage(byte[] bits)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(bits))
            {
                bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmapImage.SetSource(ms);
                Image image = new Image();
                image.Source = bitmapImage;
                return image;
            }
        }

        public static WriteableBitmap GetImgPainted(Image img)
        {
            WriteableBitmap wb = new WriteableBitmap(img, null);
            int[] ImageData = wb.Pixels;
            WriteableBitmap wb_result = new WriteableBitmap(wb.PixelWidth, wb.PixelHeight);
            for (int i = 0; i < wb.PixelHeight; i++)
            {
                for (int j = 0; j < wb.PixelWidth; j++)
                {
                    int curColor = ImageData[i * wb.PixelWidth + j];
                    byte RedValue = (byte)(curColor >> 16 & 0xFF);  //0xFF实际上没有意义!!!
                    byte GreenValue = (byte)(curColor >> 8 & 0xFF);
                    byte BlueValue = (byte)(curColor & 0xFF);
                    byte AlphaValue = (byte)(curColor >> 24 & 0xFF);
                    //获取RGB

                    //生成byte的数组表示一个像素点，
                    byte[] RedValueArr = new byte[4];
                    RedValueArr[3] = AlphaValue;  //0x00
                    RedValueArr[2] = 255;
                    RedValueArr[1] = 0;
                    RedValueArr[0] = 0;

                    //把这个byte数组转化成那个像素点。
                    int RedPixel = BitConverter.ToInt32(RedValueArr, 0);
                    unchecked
                    {
                        ////wb_gray.Pixels[i * wb.PixelWidth + j] = (int)0xFFFF0000;  //成功!!!
                        wb_result.Pixels[i * wb_result.PixelWidth + j] = RedPixel;   //OK->成功啦!!!
                    }
                }
            }

            wb_result.Invalidate();
            return wb_result;
            //经过测试，这个方法可以把一个笔画涂红。
            //可以做一个封装，传入Image.Source,传出writeableBitmap
        }
    }
}
