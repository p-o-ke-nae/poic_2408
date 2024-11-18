using System.Reflection;
using PluginBase_VideoCapture;
using PluginBase_Input;
using System.Security.Policy;
using InputDestination;
using PluginBase_ImageRecognition;
using PluginBase_CharacterRecognition;
using PluginBase_ImageProcessing;
using System.Drawing.Text;
using PluginBase_Scenario;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Resources;
using System.Text.RegularExpressions;
using System.Data;
using pokenaeLibrary;
using System.Net;

namespace poic_2408
{
    public partial class MainForm : Form, IInput
    {
        #region privateなフィールドなど
        /// <summary>
        /// メッセージボックス
        /// </summary>
        Form _messForm = new Form();

        #region ロードしたプラグイン
        /// <summary>
        /// 映像源のプラグイン一覧
        /// </summary>
        IEnumerable<IVideoCapture> _plugins_VideoCapture;
        /// <summary>
        /// 画像認識のプラグイン一覧
        /// </summary>
        IEnumerable<IImageRecognition> _plugins_ImageRecognition;
        /// <summary>
        /// 文字認識のプラグイン一覧
        /// </summary>
        IEnumerable<ICharacterRecognition> _plugins_CharacterRecognition;
        /// <summary>
        /// 画像処理のプラグイン一覧
        /// </summary>
        IEnumerable<IImageProcessing> _plugins_ImageProcessing;
        /// <summary>
        /// シナリオのプラグイン
        /// </summary>
        IScenario _plugin_Scenario;
        #endregion

        /// <summary>
        /// 入力先
        /// </summary>
        InputDestination.IInputDestination _inputDestination;

        /// <summary>
        /// 1つ前の認識フラグを保持
        /// </summary>
        bool _beforeFlg = false;
        /// <summary>
        /// 現在の認識フラグを保持
        /// </summary>
        bool _afterFlg = false;

        /// <summary>
        /// キャプチャ画像を保持するリスト
        /// </summary>
        List<Bitmap> _captureImageList = new List<Bitmap>();

        /// <summary>
        /// 処理後の画像を保持
        /// </summary>
        Bitmap _processingImage;
        /// <summary>
        /// 領域の表示などビューに特化した画像を保持
        /// </summary>
        Bitmap _viewImage;

        /// <summary>
        /// 画像認識結果の一致率を保持
        /// 0-100
        /// </summary>
        double _matchRateImageRecognition;
        /// <summary>
        /// 画像認識結果の一致領域を保持
        /// </summary>
        Rectangle _rectAngleImageRecognition;

        //タイマー処理
        /// <summary>
        /// メインのタイマー
        /// </summary>
        private System.Windows.Forms.Timer _readTimer;
        /// <summary>
        /// タイマーが動いているかを保持
        /// </summary>
        private bool _isTimerPassing = false;
        /// <summary>
        /// タイマー処理でエラーが発生したかを保持
        /// </summary>
        private bool _isTimerError = false;
        /// <summary>
        /// タイマー処理のエラー内容を保持
        /// </summary>
        private string? _timerErrorMessage;

        /// <summary>
        /// FPSを保持するフィールド
        /// 実際の計算結果はFPSプロパティでタイマーに指定する
        /// </summary>
        private int _fps = 70;

        //実FPS取得
        /// <summary>
        /// 実FPSの計測
        /// </summary>
        System.Diagnostics.Stopwatch _swReFPS = new System.Diagnostics.Stopwatch();
        /// <summary>
        /// 実FPSを格納
        /// </summary>
        List<int> _reFPSList = new List<int>();
        /// <summary>
        /// 実FPSの平均値を格納
        /// </summary>
        int _reFPS = 0;

        #region 使用するインターフェース
        IPNWebRequest _pNWebRequest;
        IPNJsonExtensions _pNJsonExtensions;
        IList<IPNImageExtensions> _pNImageExtensionses;
        IPNPluginExtensions _pNPluginExtensions;

        #endregion

        #endregion

        #region プロパティ
        #region 入力インターフェースの実装
        public string ID { get => CharConstants.sTOOLID; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get => "自動認識による入力プラグインです．"; }

        public bool IsClose { get; set; } = false;

        #endregion

        #region 使用するプラグインのID
        /// <summary>
        /// 使用する映像源プラグインID
        /// </summary>
        string UsepluginID_VideoCapture { get; set; }
        /// <summary>
        /// 使用する画像認識プラグインID
        /// </summary>
        string UsepluginID_ImageRecognition { get; set; }

        #endregion

        /// <summary>
        /// 文字認識のリスト
        /// </summary>
        IList<CharacterRecgnitionSettings> CRSList { get; set; } = new List<CharacterRecgnitionSettings>();

        /// <summary>
        /// 映像源から取得した画像
        /// </summary>
        Bitmap CaptureImage
        {
            get
            {
                var captureNo = CaptureNo;
                if (captureNo < _captureImageList.Count)
                {
                    //キャプチャNoに対応した画像が存在する場合
                    var captureIndex = _captureImageList.Count - captureNo - 1;
                    return _captureImageList[captureIndex];
                }
                else if (_captureImageList.Count > 0)
                {
                    //指定したキャプチャNoが存在しないということは最も古い画像ということ
                    return _captureImageList[0];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_captureImageList.Count >= CaptureImageListMax)
                {
                    //キャプチャリストに空きがない場合は古い画像を消す
                    if (_captureImageList[0] != null)
                    {
                        _captureImageList[0].Dispose();
                    }
                    _captureImageList.RemoveAt(0);
                }
                _captureImageList.Add((Bitmap)value);
            }
        }
        /// <summary>
        /// キャプチャNo
        /// </summary>
        int CaptureNo { get; set; } = 0;
        /// <summary>
        /// キャプチャ画像を保持する最大数
        /// </summary>
        int CaptureImageListMax { get; set; } = 5;

        /// <summary>
        /// 自動認識する際の1秒あたりのフレーム数
        /// </summary>
        public int FPS
        {
            get { return _fps; }
            set
            {
                _fps = value;
                if (_readTimer != null)
                {
                    _readTimer.Interval = (int)((double)1 / _fps * 1000);
                }
            }
        }

        /// <summary>
        /// 設定ファイルをダウンロードするAPIのリンク
        /// </summary>
        public string SettingsDownloadAPIURL { get; set; } = "";

        #endregion

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _pNWebRequest = new PNHttpRequest();
            _pNJsonExtensions = new PNJsonExtensions();
            _pNImageExtensionses = new List<IPNImageExtensions>() { new PNImageGoogleDrive()};
            _pNPluginExtensions = new PNPluginExtensions();
        }

        #region 入力インターフェースの実装
        public string GetResult()
        {
            var result = "{";

            try
            {
                //画像処理を行う
                SetProcessingImageFromPlugins();

                try
                {
                    foreach (var item in CRSList)
                    {
                        //文字認識
                        var trimer = new PresetImageProcessing_Trimming();
                        using (var trimImg = trimer.ProcessingResult(_processingImage, new Rectangle(item.X, item.Y, item.Width, item.Height)))
                        {
                            result += "\"" + item.Name + "\": " + "\"" + CharacterRecognition_Single(item.PluginID, item.PluginSettingsJson, trimImg) + "\",";
                        }
                    }
                }
                catch
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR2;
                    throw;
                }
            }
            catch
            {
                if (_isTimerPassing)
                {
                    ReadTimerStop();
                }
                else
                {
                    MessageBox.Show(_messForm, _timerErrorMessage);
                }

                throw;
            }

            result += "}";
            return result;
        }

        public void SetInput(InputDestination.IInputDestination input)
        {
            _inputDestination = input;
            this.Show();
        }

        public void InputEvent()
        {
            try
            {
                _inputDestination.InputEvent();
            }
            catch
            {
                //_timerErrorMessage = string.Format(Properties.Resources.MESS_ERROR7, "メインツール");
                //throw;
                //TODO: メインツールが無い場合，単体で意味なく動かせるのかエラーで止めるのか検討する
            }
        }

        public void InputClosing()
        {
            this.Close();
        }

        #endregion

        #region privateなロジック的メソッド
        #region プラグイン読込の処理
        /// <summary>
        /// 各プラグインを読み込む
        /// </summary>
        private void LoadPlugins()
        {
            try
            {
                //映像源のプラグインの読込
                _plugins_VideoCapture = _pNPluginExtensions.LoadPlugins<IVideoCapture>(CharConstants.sPLUGIN_VIDEOCAPTURE_PATH);
                comboBoxPluginVideoCapture.Items.Clear();
                tabControlPluginVideoCapture.TabPages.Clear();
                foreach (var item in _plugins_VideoCapture)
                {
                    comboBoxPluginVideoCapture.Items.Add(new ComboItem() { Name = item.Name, ID = item.ID });
                    item.SetUp(this.tabControlPluginVideoCapture);
                }

                //画像認識のプラグインの読込
                _plugins_ImageRecognition = _pNPluginExtensions.LoadPlugins<IImageRecognition>(CharConstants.sPLUGIN_IMAGERECOGNITION_PATH);
                comboBoxPluginImageRecognition.Items.Clear();
                tabControlPluginImageRecognition.TabPages.Clear();
                foreach (var item in _plugins_ImageRecognition)
                {
                    comboBoxPluginImageRecognition.Items.Add(new ComboItem() { Name = item.Name, ID = item.ID });
                    item.SetUp(this.tabControlPluginImageRecognition);
                }

                //文字認識のプラグインの読込
                _plugins_CharacterRecognition = _pNPluginExtensions.LoadPlugins<ICharacterRecognition>(CharConstants.sPLUGIN_CHARACTERRECOGNITION_PATH);
                comboBoxPluginCharacterRecognition.Items.Clear();
                tabControlPluginCharacterRecognition.TabPages.Clear();
                foreach (var item in _plugins_CharacterRecognition)
                {
                    comboBoxPluginCharacterRecognition.Items.Add(new ComboItem() { Name = item.Name, ID = item.ID });
                }

                //画像処理のプラグインの読込
                _plugins_ImageProcessing = _pNPluginExtensions.LoadPlugins<IImageProcessing>(CharConstants.sPLUGIN_IMAGEPROCESSING_PATH);
                tabControlPluginImageProcessing.TabPages.Clear();
                foreach (var item in _plugins_ImageProcessing)
                {
                    item.SetUp(this.tabControlPluginImageProcessing);
                }

                //シナリオプラグインの読込
                var _plugins_Scenario = _pNPluginExtensions.LoadPlugins<IScenario>(CharConstants.sPLUGIN_SCENARIO_PATH);
                if (_plugins_Scenario != null)
                {
                    foreach (var item in _plugins_Scenario)
                    {
                        _plugin_Scenario = item;
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR12);
            }

        }

        private void ImageSave(Bitmap bitmap, string dirpath)
        {
            DateTime now = DateTime.Now;
            string datestr = now.ToString("yyyyMMdd");
            string filedirpath = dirpath + datestr + "\\";
            if (!Directory.Exists(filedirpath))
            {
                Directory.CreateDirectory(filedirpath);
            }
            string fullpath = filedirpath + "ScreenShot_" + now.ToString("yyyyMMdd_hhmmssfff") + ".png";
            bitmap.Save(fullpath);
        }

        #endregion

        #region 映像源
        /// <summary>
        /// VideoCaptureプラグインから画像を取得し，変数にセットする
        /// </summary>
        private void SetCaptureImageFromPlugin(string pluginid)
        {
            var isError = true;

            try
            {
                foreach (var item in _plugins_VideoCapture)
                {
                    if (item.ID == pluginid)
                    {
                        CaptureImage = item.VideoSource;
                        isError = false;
                        break;
                    }
                }

                if (isError)
                {
                    throw new Exception();
                }
            }
            catch
            {
                _timerErrorMessage = Properties.Resources.MESS_ERROR4;
                throw;
            }
        }

        #endregion

        #region 画像認識
        /// <summary>
        /// 画像認識プラグインを用いて画像認識を行う
        /// </summary>
        /// <param name="rect">最もマッチングしていた領域を返す</param>
        /// <param name="match">最も高いマッチング率を返す</param>
        /// <returns></returns>
        private bool ImageRecognitionFromPlugin(ref Rectangle rect, out double match)
        {
            var result = false;
            double mymatch = 0;

            var isError = true;

            try
            {
                foreach (var item in _plugins_ImageRecognition)
                {
                    if (item.ID == UsepluginID_ImageRecognition)
                    {
                        result = item.Result(CaptureImage, out mymatch, ref rect);
                        isError = false;
                        break;
                    }
                }

                if (isError)
                {
                    throw new Exception();
                }
            }
            catch
            {
                rect = new Rectangle(0, 0, 1, 1);
                mymatch = 0;

                _timerErrorMessage = Properties.Resources.MESS_ERROR1;

                throw;
            }

            match = mymatch;
            return result;
        }
        #endregion

        #region 文字認識
        /// <summary>
        /// 文字認識の設定を追加する
        /// </summary>
        /// <param name="CRS"></param>
        private void CRSList_Add(CharacterRecgnitionSettings CRS)
        {
            listBoxCharacterRecognitionSettings.Items.Add(CRS);
            CRSList.Add(CRS);
        }
        /// <summary>
        /// 文字認識の設定を削除する
        /// </summary>
        /// <param name="CRS"></param>
        private void CRSList_Delete(CharacterRecgnitionSettings CRS)
        {
            listBoxCharacterRecognitionSettings.Items.Remove(CRS);
            CRSList.Remove(CRS);
        }

        /// <summary>
        /// 設定1つ分の文字認識を行う
        /// </summary>
        /// <param name="trimImage"></param>
        /// <returns></returns>
        private string CharacterRecognition_Single(string plugInID, string plugInSettinsJson, Bitmap trimImage)
        {
            var result = "";

            try
            {
                ICharacterRecognition usePlugIn = null;
                foreach (var item in _plugins_CharacterRecognition)
                {
                    if (item.ID == plugInID)
                    {
                        usePlugIn = item;
                    }
                }

                if (usePlugIn != null && trimImage != null)
                {
                    usePlugIn.LoadSettingsJson(plugInSettinsJson);
                    result = usePlugIn.RecognitionResult(trimImage);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                _timerErrorMessage = Properties.Resources.MESS_ERROR2;
                throw;
            }

            return result;
        }
        #endregion

        #region 画像処理
        /// <summary>
        /// 画像処理プラグインを用いて画像処理を行い，変数にセットする
        /// </summary>
        /// <param name="rect"></param>
        private void SetProcessingImageFromPlugins()
        {
            var isProcessingPhase = false;

            try
            {
                Bitmap processingImage = (Bitmap)CaptureImage.Clone();

                try
                {
                    //画像処理プラグインを用いて各処理を行う
                    isProcessingPhase = true;
                    foreach (var item in _plugins_ImageProcessing)
                    {
                        using (var image = (Bitmap)processingImage.Clone())
                        {
                            if (processingImage != null)
                            {
                                processingImage.Dispose();
                            }
                            processingImage = item.ProcessingResult(image, _rectAngleImageRecognition);
                        }
                    }

                    //画像処理した画像をグローバル変数にセットする
                    if (_processingImage != null)
                    {
                        _processingImage.Dispose();
                    }
                    _processingImage = (Bitmap)processingImage.Clone();
                }
                catch
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR6;
                    throw;
                }
                finally
                {
                    processingImage.Dispose();
                }
            }
            catch
            {
                if (!isProcessingPhase)
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR8;
                }
                throw;
            }
        }

        #endregion

        #region タイマー関連
        /// <summary>
        /// タイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerEvent(object sender, EventArgs e)
        {
            try
            {
                //認識フラグの更新
                _beforeFlg = _afterFlg;

                //VideoCaptureプラグインから画像を取得
                SetCaptureImageFromPlugin(UsepluginID_VideoCapture);
                if (CaptureImage == null)
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR4;
                    throw new Exception();
                }

                //画像認識を行い，どのシナリオを行うかを判定
                _afterFlg = ImageRecognitionFromPlugin(ref _rectAngleImageRecognition, out _matchRateImageRecognition);

                //画像認識の結果で分岐
                //シナリオプラグインを実行する
                if (_plugin_Scenario != null)
                {
                    try
                    {
                        if (_afterFlg == true)
                        {
                            //認識
                            if (_beforeFlg == true)
                            {
                                //認識→認識
                                _plugin_Scenario.TrueTrueEvent(this);
                            }
                            else
                            {
                                //非認識→認識
                                _plugin_Scenario.FalseTrueEvent(this);
                            }
                        }
                        else
                        {
                            //非認識
                            if (_beforeFlg == true)
                            {
                                //認識→非認識
                                _plugin_Scenario.TrueFalseEvent(this);
                            }
                            else
                            {
                                //非認識→非認識
                                _plugin_Scenario.FalseFalseEvent(this);
                            }
                        }
                    }
                    catch
                    {
                        if (_timerErrorMessage == null)
                        {
                            _timerErrorMessage = Properties.Resources.MESS_ERROR3;
                        }
                        throw;
                    }
                }
                else
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR5;
                    throw new Exception();
                }

                //ビューアの表示や後処理
                try
                {
                    //実FPSの計測
                    labelReFPS.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_REFPS, ReFPSMeasurement().ToString());

                    //画像認識の結果
                    labelMatchRateImageRecognition.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_MATCHRATE,
                    string.Format(Properties.Resources.FORMAT_RATE, _matchRateImageRecognition));
                    labelRectangleImageRecognition_X.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_X, _rectAngleImageRecognition.X);
                    labelRectangleImageRecognition_Y.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_Y, _rectAngleImageRecognition.Y);
                    labelRectangleImageRecognition_Width.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_WIDTH, _rectAngleImageRecognition.Width);
                    labelRectangleImageRecognition_Height.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_HEIGHT, _rectAngleImageRecognition.Height);

                    //ビューアへの反映
                    if (pictureBoxViewer.Image != null)
                    {
                        pictureBoxViewer.Image.Dispose();
                    }

                    if (radioButtonDefault.Checked == true)
                    {
                        pictureBoxViewer.Image = (Bitmap)CaptureImage.Clone();
                    }
                    else if (radioButtonImageRecognition.Checked == true)
                    {
                        SetProcessingImageFromPlugins();
                        pictureBoxViewer.Image = (Bitmap)_processingImage.Clone();
                    }
                    else if (radioButtonImageProcessing.Checked == true)
                    {
                        SetProcessingImageFromPlugins();
                        //画像認識範囲を表示する
                        var rectangler = new PresetImageProcessing_DrawRectangle();
                        using (var image = (Bitmap)CaptureImage.Clone())
                        {
                            if (_viewImage != null)
                            {
                                _viewImage.Dispose();
                            }
                            _viewImage = rectangler.ProcessingResult(image, _rectAngleImageRecognition);
                        }

                        //ビューアに文字認識を行う領域を表示する
                        if (_viewImage != null)
                        {
                            _viewImage.Dispose();
                        }
                        _viewImage = (Bitmap)_processingImage.Clone();
                        foreach (var item in CRSList)
                        {
                            var rectanglerCRS = new PresetImageProcessing_DrawRectangle();
                            using (var image = (Bitmap)_viewImage.Clone())
                            {
                                if (_viewImage != null)
                                {
                                    _viewImage.Dispose();
                                }
                                _viewImage = rectanglerCRS.ProcessingResult(image, new Rectangle(item.X, item.Y, item.Width, item.Height));
                            }
                        }

                        pictureBoxViewer.Image = _viewImage;
                    }
                }
                catch
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR11;
                    throw new Exception();
                }
            }
            catch
            {
                //タイマー処理の停止
                ReadTimerStop();
            }
            finally
            {
                ControlLoad();
            }
        }

        /// <summary>
        /// タイマーをスタートする処理
        /// </summary>
        public void ReadTimerStart()
        {
            //エラー状況をリセット
            _isTimerError = false;
            _timerErrorMessage = null;

            //認識前後の状況をリセット
            _beforeFlg = false;
            _afterFlg = false;

            _isTimerPassing = true;
            _readTimer.Start();
        }
        /// <summary>
        /// タイマーを止める処理
        /// </summary>
        public void ReadTimerStop()
        {
            _readTimer.Stop();
            _isTimerPassing = false;

            //一度エラーメッセージを出した後エラーフラグを立てる
            if (!_isTimerError && _timerErrorMessage != null)
            {
                MessageBox.Show(_messForm, _timerErrorMessage);
            }
            _isTimerError = true;

            //認識前後の状況をリセット
            _beforeFlg = false;
            _afterFlg = false;
        }

        /// <summary>
        /// FPSの実値を計測する
        /// </summary>
        private int ReFPSMeasurement()
        {
            //FPSの計測結果を取得する
            if (_swReFPS != null && _swReFPS.IsRunning == true)
            {
                // 計測停止
                _swReFPS.Stop();

                // 結果表示
                _reFPSList.Add((int)_swReFPS.ElapsedMilliseconds);
                int fpsnum = 0;
                if (_reFPSList.Count > 60)
                {
                    _reFPSList.RemoveAt(0);
                }
                foreach (int fps in _reFPSList)
                {
                    fpsnum += fps;
                }

                _reFPS = (int)Math.Round((double)1000 / (fpsnum / _reFPSList.Count));
            }

            // FPS計測開始
            _swReFPS = new System.Diagnostics.Stopwatch();
            _swReFPS.Start();

            return _reFPS;
        }

        #endregion

        #region 設定の保存・読込
        /// <summary>
        /// 設定の保存
        /// </summary>
        /// <returns></returns>
        private Settings SaveSettings()
        {
            var result = new Settings();

            try
            {
                //映像源プラグインの設定の保存
                result.UsePluginID_VideoCapture = UsepluginID_VideoCapture;
                foreach (var item in _plugins_VideoCapture)
                {
                    result.SettingsJson_VideoCapture.Add(item.GetSettingsJson());
                }

                //画像認識プラグインの設定の保存
                result.UsePluginID_ImageRecognition = UsepluginID_ImageRecognition;
                foreach (var item in _plugins_ImageRecognition)
                {
                    result.SettingsJson_ImageRecognition.Add(item.GetSettingsJson());
                }

                //画像処理プラグインの設定の保存
                foreach (var item in _plugins_ImageProcessing)
                {
                    result.SettingsJson_ImageProcessing.Add(item.GetSettingsJson());
                }

                //文字認識プラグインの設定の保存
                foreach (var crs in CRSList)
                {
                    foreach (var item in _plugins_CharacterRecognition)
                    {
                        if (crs.PluginID == item.ID)
                        {
                            crs.PluginSettingsJson = item.GetSettingsJson();
                        }
                    }
                }
                //文字認識リストの設定の保存
                result.CRSListJson = JsonConvert.SerializeObject(CRSList);

                //キャプチャNo
                result.CaptureNo = this.CaptureNo;
                //最大キャプチャ数
                result.CaptureImageListMax = this.CaptureImageListMax;

                //FPS
                result.FPS = this.FPS;

                //設定ファイル
                result.SettingsDownloadAPIURL = this.SettingsDownloadAPIURL;
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR15);
            }

            return result;
        }

        /// <summary>
        /// 自動認識ツールと各プラグインの設定を読み込む
        /// </summary>
        /// <param name="settings"></param>
        private void Load_Settings(Settings settings)
        {
            if (settings != null)
            {
                try
                {
                    //映像源プラグイン
                    UsepluginID_VideoCapture = settings.UsePluginID_VideoCapture;
                    foreach (var item in _plugins_VideoCapture)
                    {
                        foreach (var setting in settings.SettingsJson_VideoCapture)
                        {
                            var flg = item.LoadSettingsJson(setting);
                            if (flg == true)
                            {
                                break;
                            }
                        }
                    }

                    //画像認識プラグイン
                    UsepluginID_ImageRecognition = settings.UsePluginID_ImageRecognition;
                    foreach (var item in _plugins_ImageRecognition)
                    {
                        foreach (var setting in settings.SettingsJson_ImageRecognition)
                        {
                            var flg = item.LoadSettingsJson(setting);
                            if (flg == true)
                            {
                                break;
                            }
                        }
                    }

                    //画像処理プラグイン
                    foreach (var item in _plugins_ImageProcessing)
                    {
                        foreach (var setting in settings.SettingsJson_ImageProcessing)
                        {
                            var flg = item.LoadSettingsJson(setting);
                            if (flg == true)
                            {
                                break;
                            }
                        }
                    }

                    //文字認識リスト
                    var ocrList = JsonConvert.DeserializeObject<List<CharacterRecgnitionSettings>>(settings.CRSListJson);

                    if (ocrList != null)
                    {
                        foreach (var item in ocrList)
                        {
                            CRSList_Add(item);
                        }
                    }

                    //キャプチャNo
                    this.CaptureNo = settings.CaptureNo;
                    //最大キャプチャ数
                    this.CaptureImageListMax = settings.CaptureImageListMax;

                    //FPS
                    this.FPS = settings.FPS;

                    //設定ファイル
                    this.SettingsDownloadAPIURL = settings.SettingsDownloadAPIURL;

                }
                catch
                {
                    MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR13);
                }
                finally
                {
                    ControlLoad();
                }
            }
        }

        #endregion

        #region コントローラ関連
        /// <summary>
        /// 設定値をコントロールに反映する
        /// </summary>
        private void ControlLoad()
        {
            //コンボボックスの選択状態の反映
            foreach (var item in comboBoxCaptureNo.Items)
            {
                if (CaptureNo == (int)item)
                {
                    comboBoxCaptureNo.SelectedItem = item;
                }
            }
            foreach (var item in comboBoxPluginVideoCapture.Items)
            {
                if (item is ComboItem)
                {
                    var plugin = (ComboItem)item;
                    if (plugin != null && UsepluginID_VideoCapture == plugin.ID)
                    {
                        comboBoxPluginVideoCapture.SelectedItem = item;
                    }
                }
            }
            foreach (var item in comboBoxPluginImageRecognition.Items)
            {
                if (item is ComboItem)
                {
                    var plugin = (ComboItem)item;
                    if (plugin != null && UsepluginID_ImageRecognition == plugin.ID)
                    {
                        comboBoxPluginImageRecognition.SelectedItem = item;
                    }
                }
            }

            //numericupdownの反映
            numericUpDownFPS.Value = FPS;

            ShellUnitChange();
        }

        /// <summary>
        /// ツールの状態をシェルユニットに反映する
        /// </summary>
        private void ShellUnitChange()
        {
            List<Panel> shellUnits = new List<Panel>() { shellUnit1, shellUnit2, shellUnit3 };
            if (_isTimerPassing)
            {
                if (_afterFlg == true)
                {
                    shellUnit1.BackColor = Color.DeepPink;
                    shellUnit2.BackColor = Color.DeepPink;
                    shellUnit3.BackColor = Color.MediumPurple;
                }
                else
                {
                    foreach (var item in shellUnits)
                    {
                        item.BackColor = Color.Lime;
                    }
                }
            }
            else
            {
                foreach (var item in shellUnits)
                {
                    item.BackColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// リソースファイルから各コントローラの表示を整える
        /// </summary>
        private void ControlResourcesLoad()
        {
            tabPageSettings.Text = Properties.Resources.STRING_SETTINGS;
            tabPageObservation.Text = Properties.Resources.STRING_OBSERVATION;
            buttonScreenShotMatch.Text = Properties.Resources.BUTTON_SCREENSHOTMATCH;
            labelRectangleImageRecognition_Height.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE,
                Properties.Resources.STRING_HEIGHT, "?");
            labelRectangleImageRecognition_Width.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE,
                Properties.Resources.STRING_WIDTH, "?");
            buttonScreenShot.Text = Properties.Resources.BUTTON_SCREENSHOT;
            labelRectangleImageRecognition_Y.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE,
                Properties.Resources.STRING_Y, "?");
            labelRectangleImageRecognition_X.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE,
                Properties.Resources.STRING_X, "?");
            labelMatchRateImageRecognition.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE,
                Properties.Resources.STRING_MATCHRATE, string.Format(Properties.Resources.FORMAT_RATE, "?"));
            labelReFPS.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE,
                Properties.Resources.STRING_REFPS, "?");
            tabPageVideoCapture.Text = Properties.Resources.STRING_VIDEOCAPTURE;
            labelCaptureNo.Text = Properties.Resources.STRING_CAPTURENO;
            tabPagetabPageImageRecognition.Text = Properties.Resources.STRING_IMAGERECOGNITION;
            tabPageCharacterRecognition.Text = Properties.Resources.STRING_CHARACTERRECOGNITION;
            buttonOverWriteCharacterRecognition.Text = Properties.Resources.BUTTON_OVERWRITE;
            labelCharacterRecognitionKeyName.Text = Properties.Resources.STRING_KEYNAME;
            buttonCopyCharacterRecognition.Text = Properties.Resources.BUTTON_COPY;
            labelCRSheight.Text = Properties.Resources.STRING_HEIGHT;
            labelCRSwidth.Text = Properties.Resources.STRING_WIDTH;
            labelCRSy.Text = Properties.Resources.STRING_Y;
            labelCRSx.Text = Properties.Resources.STRING_X;
            buttonDeleteCharacterRecognition.Text = Properties.Resources.BUTTON_DELETE;
            buttonAddCharacterRecognition.Text = Properties.Resources.BUTTON_ADD;
            tabPageImageProcessing.Text = Properties.Resources.STRING_IMAGEPROCESSING;
            labelViewer.Text = Properties.Resources.STRING_VIEWER;
            radioButtonImageProcessing.Text = Properties.Resources.STRING_IMAGEPROCESSING;
            radioButtonImageRecognition.Text = Properties.Resources.STRING_IMAGERECOGNITION;
            radioButtonDefault.Text = Properties.Resources.STRING_DEFAULT;
            buttonTImerStartStop.Text = Properties.Resources.BUTTON_STARTSTOP;
            buttonCharacterRecognition.Text = Properties.Resources.STRING_CHARACTERRECOGNITION;
            labelFPS.Text = Properties.Resources.STRING_FPS;
            checkBoxTopMost.Text = Properties.Resources.STRING_TOPMOST;
            this.Text = Properties.Resources.TOOL_NAME + " ver" + CharConstants.VERSION;
        }

        #region スクリーンショット
        private void buttonScreenShot_Click(object sender, EventArgs e)
        {
            try
            {
                ImageSave(CaptureImage, CharConstants.sIMAGE_PATH);
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR8);
            }
        }

        private void buttonScreenShotMatch_Click(object sender, EventArgs e)
        {
            try
            {
                IImageProcessing trim = new PresetImageProcessing_Trimming();
                using (Bitmap bitmap = trim.ProcessingResult(CaptureImage, _rectAngleImageRecognition))
                {
                    ImageSave(bitmap, CharConstants.sIMAGE_PATH);
                }
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR8);
            }
        }

        #endregion

        #region 個人の設定ファイルの取得
        /// <summary>
        /// WebAPIから指定のリンク先から設定ファイルと画像をダウンロードする
        /// </summary>
        private async void GetSettingsFromLink(string id)
        {
            try
            {
                var url = SettingsDownloadAPIURL;
                var json = await _pNWebRequest.GetRequest(url, 30000);

                var settingsresponse = JsonConvert.DeserializeObject<SettingsResponse>(json);
                if (settingsresponse != null)
                {
                    var filePath = settingsresponse.SettingsPath; 
                    if(IsFileSave(filePath))
                    {
                        //設定ファイルを保存する
                        _pNJsonExtensions.SerializeToFile<Settings>(settingsresponse.Settings, filePath);

                        //画像を保存する
                        var isError = false;
                        foreach(var item in settingsresponse.Images)
                        {
                            try
                            {
                                ImageSaveFromURL(item);
                            }
                            catch
                            {
                                isError = true;
                            }
                        }

                        if(isError)
                        {
                            MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR14);
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR13);
            }
        }



        #endregion

        #endregion

        #region ファイルの保存やダウンロード
        /// <summary>
        /// 指定されたパスにファイルを保存するかメッセージボックスで確認
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private bool IsFileSave(string filepath)
        {
            //ファイルの上書きをするか確認
            if (File.Exists(filepath))
            {
                DialogResult MSResult = MessageBox.Show(_messForm, Properties.Resources.MESS_FILESAVE, Properties.Resources.STRING_OVERWRITECONF, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (MSResult != System.Windows.Forms.DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 引数のURLより画像を読み込み保存する
        /// </summary>
        /// <param name="image"></param>
        private async void ImageSaveFromURL(IPNImageExtensions.ImageURL image)
        {
            try
            {
                if (!string.IsNullOrEmpty(image.Url))
                {
                    var isSave = false;
                    foreach(var item in _pNImageExtensionses)
                    {
                        isSave = await item.ImageSave(image);

                        if(isSave)
                        {
                            return;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        #endregion

        #endregion

        #region privateなControlのイベントのメソッド
        public void MainForm_Load(object sender, EventArgs e)
        {
            //プラグインの読込
            LoadPlugins();

            //タイマーの初期設定
            //タイマーを初期化
            _readTimer = new System.Windows.Forms.Timer();
            //タイマーにイベントを登録
            _readTimer.Tick += new EventHandler(TimerEvent);

            FPS = 70;

            //GUIの整理
            _messForm.TopMost = true;
            labelSelectPluginCharacterRecognition.Text = "";
            ControlResourcesLoad();
            //キャプチャNoの用意
            for (int i = 0; i < CaptureImageListMax; i++)
            {
                comboBoxCaptureNo.Items.Add(i);
            }

            //設定の読込
            var settings = _pNJsonExtensions.DeserializeFromFile<Settings>(CharConstants.sCONFIG_PATH);
            if (settings != null)
            {
                Load_Settings(settings);
            }

        }

        private void comboBoxPluginVideoCapture_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = (ComboBox)sender;
            if (cmb.SelectedItem is ComboItem)
            {
                var plugin = (ComboItem)cmb.SelectedItem;
                UsepluginID_VideoCapture = plugin.ID;
            }
            else
            {
                UsepluginID_VideoCapture = null;
            }
        }

        private void comboBoxPluginImageRecognition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = (ComboBox)sender;
            if (cmb.SelectedItem is ComboItem)
            {
                var plugin = (ComboItem)cmb.SelectedItem;
                UsepluginID_ImageRecognition = plugin.ID;
            }
            else
            {
                UsepluginID_ImageRecognition = null;
            }
        }

        private void comboBoxPluginCharacterRecognition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = (ComboBox)sender;
            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null && item is CharacterRecgnitionSettings && cmb.SelectedItem is ComboItem)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    var plugin = (ComboItem)cmb.SelectedItem;
                    CRS.PluginID = plugin.ID;
                    CRS.PluginName = plugin.Name;
                    labelSelectPluginCharacterRecognition.Text = plugin.Name;
                }
            }
        }

        private void buttonAddCharacterRecognition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCharacterRecognitionKeyName.Text))
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR10);
                return;
            }
            var newCRS = new CharacterRecgnitionSettings();
            newCRS.Name = textBoxCharacterRecognitionKeyName.Text;
            CRSList_Add(newCRS);
        }

        private void buttonCopyCharacterRecognition_Click(object sender, EventArgs e)
        {
            var CRSs = new List<CharacterRecgnitionSettings>();
            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    CRSs.Add((CharacterRecgnitionSettings)item);
                }
            }

            if (CRSs != null)
            {
                foreach (var item in CRSs)
                {
                    if (item != null)
                    {
                        CRSList_Add(new CharacterRecgnitionSettings(item));
                    }
                }
            }
        }

        private void buttonDeleteCharacterRecognition_Click(object sender, EventArgs e)
        {
            var CRSs = new List<CharacterRecgnitionSettings>();
            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    CRSs.Add((CharacterRecgnitionSettings)item);
                }
            }

            if (CRSs != null)
            {
                foreach (var item in CRSs)
                {
                    if (item != null)
                    {
                        CRSList_Delete(item);
                    }
                }
            }
        }

        private void numericUpDownCRSx_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;

            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    CRS.X = (int)nud.Value;
                }
            }
        }

        private void numericUpDownCRSy_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;

            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    CRS.Y = (int)nud.Value;
                }
            }
        }

        private void numericUpDownCRSwidth_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;

            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    CRS.Width = (int)nud.Value;
                }
            }
        }

        private void numericUpDownCRSheight_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;

            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    CRS.Height = (int)nud.Value;
                }
            }
        }

        private void buttonOverWriteCharacterRecognition_Click(object sender, EventArgs e)
        {
            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    CRS.Name = textBoxCharacterRecognitionKeyName.Text;
                }
            }

            listBoxCharacterRecognitionSettings.Items.Clear();
            foreach (var item in CRSList)
            {
                listBoxCharacterRecognitionSettings.Items.Add(item);
            }

        }

        private void listBoxCharacterRecognitionSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCharacterRecognitionSettings.SelectedItems.Count == 1)
            {
                foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
                {
                    if (item != null)
                    {
                        var CRS = (CharacterRecgnitionSettings)item;

                        numericUpDownCRSx.Value = CRS.X;
                        numericUpDownCRSy.Value = CRS.Y;
                        numericUpDownCRSwidth.Value = CRS.Width;
                        numericUpDownCRSheight.Value = CRS.Height;
                        textBoxCharacterRecognitionKeyName.Text = CRS.Name;
                        if (CRS.PluginName != null)
                        {
                            labelSelectPluginCharacterRecognition.Text = CRS.PluginName;
                        }
                        else
                        {
                            labelSelectPluginCharacterRecognition.Text = Properties.Resources.MESS_ERROR9;
                        }

                        break;
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClose)
            {
                _pNJsonExtensions.SerializeToFile<MainForm.Settings>(SaveSettings(), CharConstants.sCONFIG_PATH);
            }
            else
            {
                e.Cancel = true;
                Form f = (Form)sender;
                f.Visible = false;
            }
        }

        private void labelSelectPluginCharacterRecognition_Click(object sender, EventArgs e)
        {

        }

        private void buttonTImerStartStop_Click(object sender, EventArgs e)
        {
            if (_isTimerPassing)
            {
                ReadTimerStop();
            }
            else
            {
                ReadTimerStart();
            }

            ControlLoad();
        }

        private void buttonCharacterRecognition_Click(object sender, EventArgs e)
        {
            try
            {
                InputEvent();
            }
            catch
            {
                if (_isTimerPassing)
                {
                    ReadTimerStop();
                }
                else
                {
                    MessageBox.Show(_messForm, _timerErrorMessage);
                }
            }
        }

        private void comboBoxCaptureNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = (ComboBox)sender;
            CaptureNo = cmb.SelectedIndex;
        }

        private void numericUpDownFPS_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            FPS = (int)nud.Value;
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            this.TopMost = chk.Checked;
        }

        private void buttonSettingDownload_Click(object sender, EventArgs e)
        {
            GetSettingsFromLink(textBoxSettingDownloadID.Text);
        }

        #endregion

        #region MainFormの設定クラス
        class Settings
        {
            /// <summary>
            /// 使用する映像源プラグインのID
            /// </summary>
            [JsonProperty("usingpluginid_videocapture")]
            public string UsePluginID_VideoCapture { get; set; }
            /// <summary>
            /// 映像源の設定Json一覧
            /// </summary>
            [JsonProperty("settingsjson_videocapture")]
            public IList<string> SettingsJson_VideoCapture { get; set; } = new List<string>();

            /// <summary>
            /// 使用する画像認識プラグインのID
            /// </summary>
            [JsonProperty("usingpluginid_imagerecognition")]
            public string UsePluginID_ImageRecognition { get; set; }
            /// <summary>
            /// 画像認識の設定Json一覧
            /// </summary>
            [JsonProperty("settingsjson_imagerecognition")]
            public IList<string> SettingsJson_ImageRecognition { get; set; } = new List<string>();

            /// <summary>
            /// 画像処理の設定Json一覧
            /// </summary>
            [JsonProperty("settingsjson_imageprocessing")]
            public IList<string> SettingsJson_ImageProcessing { get; set; } = new List<string>();

            /// <summary>
            /// 文字認識のリスト
            /// </summary>
            [JsonProperty("crslistjson")]
            public string CRSListJson { get; set; }

            /// <summary>
            /// キャプチャNo
            /// </summary>
            [JsonProperty("captureno")]
            public int CaptureNo { get; set; } = 0;
            /// <summary>
            /// キャプチャ画像を保持する最大数
            /// </summary>
            [JsonProperty("captureimagelistmax")]
            public int CaptureImageListMax { get; set; } = 5;

            /// <summary>
            /// FPS
            /// </summary>
            [JsonProperty("fps")]
            public int FPS { get; set; } = 70;

            /// <summary>
            /// 設定ファイルをダウンロードするAPIのリンク
            /// </summary>
            [JsonProperty("settingsdownloadapiurl")]
            public string SettingsDownloadAPIURL { get; set; } = "";


            /// <summary>
            /// デフォルトコンストラクタ
            /// </summary>
            public Settings()
            {

            }
        }

        #endregion

        #region 文字認識クラス
        /// <summary>
        /// 文字認識の設定
        /// </summary>
        class CharacterRecgnitionSettings
        {
            /// <summary>
            /// Jsonのキーにもなる名前
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// 使用するプラグインのID
            /// </summary>
            [JsonProperty("pluginid")]
            public string PluginID { get; set; }

            /// <summary>
            /// 使用するプラグインの名前
            /// </summary>
            [JsonProperty("pluginname")]
            public string PluginName { get; set; }

            /// <summary>
            /// 使用するプラグインの設定Json
            /// </summary>
            [JsonProperty("pluginsettingsjson")]
            public string PluginSettingsJson { get; set; }

            /// <summary>
            /// 座標：X
            /// </summary>
            [JsonProperty("x")]
            public int X { get; set; } = 0;
            /// <summary>
            /// 座標：Y
            /// </summary>
            [JsonProperty("y")]
            public int Y { get; set; } = 0;
            /// <summary>
            /// 幅
            /// </summary>
            [JsonProperty("width")]
            public int Width { get; set; } = 40;
            /// <summary>
            /// 高さ
            /// </summary>
            [JsonProperty("height")]
            public int Height { get; set; } = 20;

            public override string ToString()
            {
                return Name;
            }

            /// <summary>
            /// デフォルトコンストラクタ
            /// </summary>
            public CharacterRecgnitionSettings()
            {

            }
            /// <summary>
            /// 引数より取得したインスタンスより生成
            /// </summary>
            /// <param name="source"></param>
            public CharacterRecgnitionSettings(CharacterRecgnitionSettings source)
            {
                Name = source.Name;
                PluginID = source.PluginID;
                PluginSettingsJson = source.PluginSettingsJson;
                X = source.X;
                Y = source.Y;
                Width = source.Width;
                Height = source.Height;
            }

        }

        #endregion

        #region WebAPIから取得した設定ファイルと画像を格納するクラス
        class SettingsResponse
        {
            [JsonProperty("settings")]
            public Settings Settings { get; set; }
            [JsonProperty("settingspath")]
            public string SettingsPath { get; set; }
            [JsonProperty("images")]
            public List<IPNImageExtensions.ImageURL> Images { get; set; }
            
        }

        #endregion

        #region ComboBox格納用クラス
        class ComboItem
        {
            public string Name { get; set; }
            public string ID { get; set; }

            public override string ToString() { return Name; }
        }

        #endregion


        
    }
}
