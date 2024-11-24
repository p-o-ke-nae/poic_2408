using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace poic_2408
{
    static class CharConstants
    {
        /// <summary>
        /// ツールID
        /// </summary>
        public static string sTOOLID = "poic_2408";

        /// <summary>
        /// バージョン
        /// </summary>
        public static string VERSION
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly().GetName();
                var version = assembly.Version;
                return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            }
        }

        /// <summary>
        /// 本dllのパス
        /// </summary>
        public static string sINPUTDIRPATH = Directory.GetParent(Assembly.GetExecutingAssembly().Location) + "\\";

        /// <summary>
        /// 映像ソースプラグインの格納パス
        /// </summary>
        public static string sPLUGIN_VIDEOCAPTURE_PATH = sINPUTDIRPATH + "Plugin_VideoCapture\\";
        /// <summary>
        /// 画像認識プラグインの格納パス
        /// </summary>
        public static string sPLUGIN_IMAGERECOGNITION_PATH = sINPUTDIRPATH + "Plugin_ImageRecognition\\";
        /// <summary>
        /// 文字認識プラグインの格納パス
        /// </summary>
        public static string sPLUGIN_CHARACTERRECOGNITION_PATH = sINPUTDIRPATH + "Plugin_CharacterRecognition\\";
        /// <summary>
        /// 画像処理プラグインの格納パス
        /// </summary>
        public static string sPLUGIN_IMAGEPROCESSING_PATH = sINPUTDIRPATH + "Plugin_ImageProcessing\\";
        /// <summary>
        /// シナリオプラグインの格納パス
        /// </summary>
        public static string sPLUGIN_SCENARIO_PATH = sINPUTDIRPATH + "Plugin_Scenario\\";
        /// <summary>
        /// シナリオプラグインの格納パス
        /// </summary>
        public static string sPLUGIN_SCREENSHOT_PATH = sINPUTDIRPATH + "Plugin_ScreenShot\\";

        /// <summary>
        /// 画像フォルダのパス
        /// </summary>
        public static string sIMAGE_PATH = sINPUTDIRPATH + "images\\";
        /// <summary>
        /// 設定フォルダのパス
        /// </summary>
        public static string sSETTINGS_PATH = sINPUTDIRPATH + "settings\\";
        /// <summary>
        /// 起動時の設定ファイルのパス
        /// </summary>
        public static string sCONFIG_PATH = sINPUTDIRPATH + "config.json";
        /// <summary>
        /// 設定ファイルとそのディレクトリのフォーマット
        /// </summary>
        public static string sSETTINGS_FORMATPATH = sINPUTDIRPATH + "settings\\{0}\\{0}.json";

    }
}
