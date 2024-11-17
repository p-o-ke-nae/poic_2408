using PluginBase_ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace poic_2408
{
    internal class PresetImageProcessing_Trimming : IImageProcessing
    {
        public string ID { get => "IP0001"; }
        public string Name { get => "画像トリミングプラグイン"; }
        public string Description { get => "画像のトリミングを行うプラグインです．"; }

        public void SetUp(TabControl tabs)
        {
            //tabs.TabPages.Add(this);
        }

        public Bitmap ProcessingResult(Bitmap InputImage)
        {
            return ProcessingResult(InputImage, new Rectangle(0, 0, InputImage.Width, InputImage.Height));
        }
        public Bitmap ProcessingResult(Bitmap InputImage, Point point)
        {
            return ProcessingResult(InputImage, new Rectangle(point.X,point.Y,InputImage.Width,InputImage.Height));
        }
        public Bitmap ProcessingResult(Bitmap InputImage, Rectangle rectangle)
        {
            if(InputImage.Width < 1 || InputImage.Height < 1)
            {
                return InputImage;
            }
            
            //調整結果を格納
            Rectangle reRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width,rectangle.Height);

            //座標についてマイナス側を検証
            if (0 > reRectangle.X)
            {
                reRectangle.X = 0;
            }
            if (0 > reRectangle.Y)
            {
                reRectangle.Y = 0;
            }

            //座標についてプラス側を検証
            if (InputImage.Width < reRectangle.X)
            {
                reRectangle.X = InputImage.Width;
            }
            if (InputImage.Height < reRectangle.Y)
            {
                reRectangle.Y = InputImage.Height;
            }

            //サイズについて検証
            if (InputImage.Width < reRectangle.X + reRectangle.Width)
            {
                reRectangle.Width = InputImage.Width - reRectangle.X;
            }
            if (InputImage.Height < reRectangle.Y + reRectangle.Height)
            {
                reRectangle.Height = InputImage.Height - reRectangle.Y;
            }

            //最低でも1×1のサイズにする
            if(reRectangle.Width < 1)
            {
                reRectangle.Width = 1;
                reRectangle.X = reRectangle.X - reRectangle.Width;
            }
            if (reRectangle.Height < 1)
            {
                reRectangle.Height = 1;
                reRectangle.Y = reRectangle.Y - reRectangle.Height;
            }

            Bitmap result = new Bitmap(reRectangle.Width, reRectangle.Height);

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(result))
            {
                //描画する部分の範囲を決定する。
                Rectangle desRect = new Rectangle(0, 0, reRectangle.Width, reRectangle.Height);
                //画像の一部を描画する
                g.DrawImage(InputImage, 0, 0, reRectangle, GraphicsUnit.Pixel);

            }

            return result;
        }

        public string GetSettingsJson()
        {
            return "";
        }

        public bool LoadSettingsJson(string json)
        {
            return true;
        }

    }
}
