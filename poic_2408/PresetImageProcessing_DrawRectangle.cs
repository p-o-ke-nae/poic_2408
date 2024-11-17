using PluginBase_ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poic_2408
{
    public class PresetImageProcessing_DrawRectangle : IImageProcessing
    {
        public string ID { get => "IP0002"; }
        public string Name { get => "四角形描画プラグイン"; }
        public string Description { get => "四角形の描画を行うプラグインです．"; }
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
            return ProcessingResult(InputImage, new Rectangle(point.X, point.Y, InputImage.Width, InputImage.Height));
        }
        public Bitmap ProcessingResult(Bitmap InputImage, Rectangle rectangle)
        {
            Bitmap result = (Bitmap)InputImage.Clone();

            int penpoint = InputImage.Width / 100;
            if(penpoint < 1)
            {
                penpoint = 1;
            }
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(result))
            using (Pen p = new Pen(Color.Red, penpoint))
            {
                //位置(10, 20)に100x80の長方形を描く
                g.DrawRectangle(p, rectangle);
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
