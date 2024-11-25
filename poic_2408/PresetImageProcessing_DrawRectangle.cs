using Newtonsoft.Json;
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
        public Color Color { get; set; } = Color.Red;

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

            int penpoint = InputImage.Width / 50;
            if(penpoint < 1)
            {
                penpoint = 1;
            }
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(result))
            using (Pen p = new Pen(Color, penpoint))
            {
                //位置(10, 20)に100x80の長方形を描く
                g.DrawRectangle(p, rectangle);
            }

            return result;
        }

        public string GetSettingsJson()
        {
            var settings = JsonConvert.SerializeObject(new Settings(this));

            return settings;
        }

        public bool LoadSettingsJson(string json)
        {
            var settings = JsonConvert.DeserializeObject<Settings>(json);

            if (settings != null && settings.ID == this.ID)
            {
                this.Color = settings.Color;

                return true;
            }
            else
            {
                return false;
            }
        }

        public class Settings
        {
            [JsonProperty("id")]
            public string ID { get; set; }

            [JsonProperty("color")]
            public Color Color { get; set; }

            public Settings()
            {

            }
            public Settings(PresetImageProcessing_DrawRectangle source)
            {
                this.ID = source.ID;
                this.Color = source.Color;
            }
        }
    }
}
