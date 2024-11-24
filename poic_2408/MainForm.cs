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
using PluginBase_ScreenShot;

namespace poic_2408
{
    public partial class MainForm : Form, IInput
    {
        #region ���[�h�����v���O�C��
        /// <summary>
        /// �f�����̃v���O�C���ꗗ
        /// </summary>
        IEnumerable<IVideoCapture> _plugins_VideoCapture;
        /// <summary>
        /// �摜�F���̃v���O�C���ꗗ
        /// </summary>
        IEnumerable<IImageRecognition> _plugins_ImageRecognition;
        /// <summary>
        /// �����F���̃v���O�C���ꗗ
        /// </summary>
        IEnumerable<ICharacterRecognition> _plugins_CharacterRecognition;
        /// <summary>
        /// �摜�����̃v���O�C���ꗗ
        /// </summary>
        IEnumerable<IImageProcessing> _plugins_ImageProcessing;
        /// <summary>
        /// �V�i���I�̃v���O�C��
        /// </summary>
        IScenario _plugin_Scenario;
        /// <summary>
        /// �X�N���[���V���b�g�̃v���O�C��
        /// </summary>
        IScreenShot _plugin_ScreenShot;

        #endregion

        #region �g�p����C���^�[�t�F�[�X
        IPNWebRequest _pNWebRequest;
        IPNJsonExtensions _pNJsonExtensions;
        IList<IPNImageExtensions> _pNImageExtensionses;
        IPNPluginExtensions _pNPluginExtensions;
        IPNAPIRequest _pNAPIRequest;

        #endregion

        #region private�ȃt�B�[���h�Ȃ�
        /// <summary>
        /// ���b�Z�[�W�{�b�N�X
        /// </summary>
        Form _messForm = new Form();

        /// <summary>
        /// ���͐�
        /// </summary>
        InputDestination.IInputDestination _inputDestination;

        /// <summary>
        /// 1�O�̔F���t���O��ێ�
        /// </summary>
        bool _beforeFlg = false;
        /// <summary>
        /// ���݂̔F���t���O��ێ�
        /// </summary>
        bool _afterFlg = false;

        /// <summary>
        /// �L���v�`���摜��ێ����郊�X�g
        /// </summary>
        List<Bitmap> _captureImageList = new List<Bitmap>();

        /// <summary>
        /// ������̉摜��ێ�
        /// </summary>
        Bitmap _processingImage;
        /// <summary>
        /// �̈�̕\���Ȃǃr���[�ɓ��������摜��ێ�
        /// </summary>
        Bitmap _viewImage;

        /// <summary>
        /// �摜�F�����ʂ̈�v����ێ�
        /// 0-100
        /// </summary>
        double _matchRateImageRecognition;
        /// <summary>
        /// �摜�F�����ʂ̈�v�̈��ێ�
        /// </summary>
        Rectangle _rectAngleImageRecognition;

        //�^�C�}�[����
        /// <summary>
        /// ���C���̃^�C�}�[
        /// </summary>
        private System.Windows.Forms.Timer _readTimer;
        /// <summary>
        /// �^�C�}�[�������Ă��邩��ێ�
        /// </summary>
        private bool _isTimerPassing = false;
        /// <summary>
        /// �^�C�}�[�����ŃG���[��������������ێ�
        /// </summary>
        private bool _isTimerError = false;
        /// <summary>
        /// �^�C�}�[�����̃G���[���e��ێ�
        /// </summary>
        private string? _timerErrorMessage;

        /// <summary>
        /// FPS��ێ�����t�B�[���h
        /// ���ۂ̌v�Z���ʂ�FPS�v���p�e�B�Ń^�C�}�[�Ɏw�肷��
        /// </summary>
        private int _fps = 70;

        //��FPS�擾
        /// <summary>
        /// ��FPS�̌v��
        /// </summary>
        System.Diagnostics.Stopwatch _swReFPS = new System.Diagnostics.Stopwatch();
        /// <summary>
        /// ��FPS���i�[
        /// </summary>
        List<int> _reFPSList = new List<int>();
        /// <summary>
        /// ��FPS�̕��ϒl���i�[
        /// </summary>
        int _reFPS = 0;

        #endregion

        #region �v���p�e�B
        #region ���̓C���^�[�t�F�[�X�̎���
        public string ID { get => CharConstants.sTOOLID; }

        /// <summary>
        /// ����
        /// </summary>
        public string Description { get => "�����F���ɂ����̓v���O�C���ł��D"; }

        public bool IsClose { get; set; } = false;

        #endregion

        #region �g�p����v���O�C����ID
        /// <summary>
        /// �g�p����f�����v���O�C��ID
        /// </summary>
        string UsepluginID_VideoCapture { get; set; }
        /// <summary>
        /// �g�p����摜�F���v���O�C��ID
        /// </summary>
        string UsepluginID_ImageRecognition { get; set; }

        #endregion

        /// <summary>
        /// �����F���̃��X�g
        /// </summary>
        IList<CharacterRecgnitionSettings> CRSList { get; set; } = new List<CharacterRecgnitionSettings>();

        /// <summary>
        /// �f��������擾�����摜
        /// </summary>
        Bitmap CaptureImage
        {
            get
            {
                var captureNo = CaptureNo;
                if (captureNo < _captureImageList.Count)
                {
                    //�L���v�`��No�ɑΉ������摜�����݂���ꍇ
                    var captureIndex = _captureImageList.Count - captureNo - 1;
                    return _captureImageList[captureIndex];
                }
                else if (_captureImageList.Count > 0)
                {
                    //�w�肵���L���v�`��No�����݂��Ȃ��Ƃ������Ƃ͍ł��Â��摜�Ƃ�������
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
                    //�L���v�`�����X�g�ɋ󂫂��Ȃ��ꍇ�͌Â��摜������
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
        /// �L���v�`��No
        /// </summary>
        int CaptureNo { get; set; } = 0;
        /// <summary>
        /// �L���v�`���摜��ێ�����ő吔
        /// </summary>
        int CaptureImageListMax { get; set; } = 5;

        /// <summary>
        /// �����F������ۂ�1�b������̃t���[����
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
        /// �ݒ�t�@�C�����_�E�����[�h����API�̃����N
        /// </summary>
        public string SettingsDownloadAPIURL { get; set; } = "";

        #endregion

        /// <summary>
        /// �f�t�H���g�R���X�g���N�^
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // �E�B���h�E�̃T�C�Y���Œ肷��ݒ�
            // �g���Œ�
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // �ő剻�{�^���𖳌���
            this.MaximizeBox = false;
            // �ŏ����{�^���͗L���̂܂�
            this.MinimizeBox = true;

            try
            {
                _pNWebRequest = new PNHttpRequest();
                _pNJsonExtensions = new PNJsonExtensions();
                _pNImageExtensionses = new List<IPNImageExtensions>() { new PNImageGoogleDrive() };
                _pNPluginExtensions = new PNPluginExtensions();
                _pNAPIRequest = new PNAPIRequest();
            }
            catch
            {

            }
        }

        #region ���̓C���^�[�t�F�[�X�̎���
        public string GetResult()
        {
            var result = "{";

            try
            {
                //�摜�������s��
                SetProcessingImageFromPlugins();

                try
                {
                    foreach (var item in CRSList)
                    {
                        //�����F��
                        var trimer = new PresetImageProcessing_Trimming();
                        using (var trimImg = trimer.ProcessingResult(_processingImage, new Rectangle(item.X, item.Y, item.Width, item.Height)))
                        {
                            result += "\"" + item.Name + "\": " + "\"" + CharacterRecognition_Single(item.PluginID, item.PluginSettingsJson, trimImg) + "\",";
                        }
                    }

                    //UI���Ō�ɓǂݍ��܂ꂽ���̂�\�����Ă���̂Ń��Z�b�g����
                    ClearCharacterPluginUI();
                }
                catch
                {
                    _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.STRING_CHARACTERRECOGNITION);
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
                    ControlLoad();
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
                _timerErrorMessage = string.Format(Properties.Resources.MESS_ERROR4, "���C���c�[��");
                throw;
            }
        }

        public void InputClosing()
        {
            this.Close();
        }

        #endregion

        #region private�ȃ��W�b�N�I���\�b�h
        #region �v���O�C���Ǎ��̏���
        /// <summary>
        /// �e�v���O�C����ǂݍ���
        /// </summary>
        private void LoadPlugins()
        {
            try
            {
                //�f�����̃v���O�C���̓Ǎ�
                _plugins_VideoCapture = _pNPluginExtensions.LoadPlugins<IVideoCapture>(CharConstants.sPLUGIN_VIDEOCAPTURE_PATH);
                comboBoxPluginVideoCapture.Items.Clear();
                tabControlPluginVideoCapture.TabPages.Clear();
                foreach (var item in _plugins_VideoCapture)
                {
                    comboBoxPluginVideoCapture.Items.Add(new ComboItemPlugin() { Name = item.Name, ID = item.ID });
                    item.SetUp(this.tabControlPluginVideoCapture);
                }

                //�摜�F���̃v���O�C���̓Ǎ�
                _plugins_ImageRecognition = _pNPluginExtensions.LoadPlugins<IImageRecognition>(CharConstants.sPLUGIN_IMAGERECOGNITION_PATH);
                comboBoxPluginImageRecognition.Items.Clear();
                tabControlPluginImageRecognition.TabPages.Clear();
                foreach (var item in _plugins_ImageRecognition)
                {
                    comboBoxPluginImageRecognition.Items.Add(new ComboItemPlugin() { Name = item.Name, ID = item.ID });
                    item.SetUp(this.tabControlPluginImageRecognition);
                }

                //�����F���̃v���O�C���̓Ǎ�
                _plugins_CharacterRecognition = _pNPluginExtensions.LoadPlugins<ICharacterRecognition>(CharConstants.sPLUGIN_CHARACTERRECOGNITION_PATH);
                comboBoxPluginCharacterRecognition.Items.Clear();
                tabControlPluginCharacterRecognition.TabPages.Clear();
                foreach (var item in _plugins_CharacterRecognition)
                {
                    comboBoxPluginCharacterRecognition.Items.Add(new ComboItemPlugin() { Name = item.Name, ID = item.ID });
                    item.SetUp(this.tabControlPluginCharacterRecognition);
                }
                //�����F���̃^�u�������ׂẴR���g���[���Ƀ`�F���W�C�x���g��ǉ�
                foreach (TabPage page in tabControlPluginCharacterRecognition.TabPages)
                {
                    foreach (Control item in page.Controls)
                    {
                        if (item is TextBox txb)
                        {
                            txb.TextChanged += TabPageControlChanged;
                        }
                        else if(item is CheckBox chb)
                        {
                            chb.CheckedChanged += TabPageControlChanged;
                        }
                        else if(item is NumericUpDown nud)
                        {
                            nud.ValueChanged += TabPageControlChanged;
                        }
                        else if (item is ComboBox cmb)
                        {
                            cmb.SelectedIndexChanged += TabPageControlChanged;
                        }
                    }
                }

                //�摜�����̃v���O�C���̓Ǎ�
                _plugins_ImageProcessing = _pNPluginExtensions.LoadPlugins<IImageProcessing>(CharConstants.sPLUGIN_IMAGEPROCESSING_PATH);
                tabControlPluginImageProcessing.TabPages.Clear();
                foreach (var item in _plugins_ImageProcessing)
                {
                    item.SetUp(this.tabControlPluginImageProcessing);
                }

                //�V�i���I�v���O�C���̓Ǎ�
                var _plugins_Scenario = _pNPluginExtensions.LoadPlugins<IScenario>(CharConstants.sPLUGIN_SCENARIO_PATH);
                if (_plugins_Scenario != null)
                {
                    foreach (var item in _plugins_Scenario)
                    {
                        _plugin_Scenario = item;
                        break;
                    }
                }

                //�X�N���[���V���b�g�v���O�C���̓Ǎ�
                var _plugins_ScreenShot = _pNPluginExtensions.LoadPlugins<IScreenShot>(CharConstants.sPLUGIN_SCREENSHOT_PATH);
                if (_plugins_ScreenShot != null)
                {
                    foreach (var item in _plugins_ScreenShot)
                    {
                        _plugin_ScreenShot = item;
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    string.Format(Properties.Resources.FORMAT_READERROR, Properties.Resources.STRING_PLUGIN)
                    ));
            }

        }

        private void TabPageTextBoxChanged(object sender, EventArgs e)
        {

        }
        private void TabPageCheckBoxChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// �^�u�y�[�W���̑S�ẴR���g���[���̊e�`�F���W�C�x���g��Json�ݒ蔽�f��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPageControlChanged(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                TabPage parentTabPage = GetParentTabPage(control);
                if (parentTabPage is ICharacterRecognition icr)
                {
                    for (int i = 0; i < listBoxCharacterRecognitionSettings.Items.Count; i++)
                    {
                        foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
                        {
                            if (listBoxCharacterRecognitionSettings.Items[i] == item && CRSList[i].PluginID == icr.ID)
                            {
                                CRSList[i].PluginSettingsJson = icr.GetSettingsJson();
                            }
                        }
                    }
                }
            }
        }

        private TabPage GetParentTabPage(Control control)
        { 
            // �R���g���[���̐e�K�w��H����TabPage��T��
            Control parent = control.Parent; 
            while (parent != null && !(parent is TabPage)) 
            { 
                parent = parent.Parent; 
            } 
            return parent as TabPage; 
        }


            #endregion

        #region �f����
        /// <summary>
        /// VideoCapture�v���O�C������摜���擾���C�ϐ��ɃZ�b�g����
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
                _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.MESS_ERROR2);
                throw;
            }
        }

        #endregion

        #region �摜�F��
        /// <summary>
        /// �摜�F���v���O�C����p���ĉ摜�F�����s��
        /// </summary>
        /// <param name="rect">�ł��}�b�`���O���Ă����̈��Ԃ�</param>
        /// <param name="match">�ł������}�b�`���O����Ԃ�</param>
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

                _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.STRING_IMAGERECOGNITION);

                throw;
            }

            match = mymatch;
            return result;
        }
        #endregion

        #region �����F��
        /// <summary>
        /// �����F���̐ݒ��ǉ�����
        /// </summary>
        /// <param name="CRS"></param>
        private void CRSList_Add(CharacterRecgnitionSettings CRS)
        {
            listBoxCharacterRecognitionSettings.Items.Add(CRS);
            CRSList.Add(CRS);
        }
        /// <summary>
        /// �����F���̐ݒ���폜����
        /// </summary>
        /// <param name="CRS"></param>
        private void CRSList_Delete(CharacterRecgnitionSettings CRS)
        {
            listBoxCharacterRecognitionSettings.Items.Remove(CRS);
            CRSList.Remove(CRS);
        }
        /// <summary>
        /// �����F���̐ݒ���N���A����
        /// </summary>
        /// <param name="CRS"></param>
        private void CRSList_Clear()
        {
            listBoxCharacterRecognitionSettings.Items.Clear();
            CRSList.Clear();
        }

        /// <summary>
        /// �ݒ�1���̕����F�����s��
        /// </summary>
        /// <param name="trimImage"></param>
        /// <returns></returns>
        private string CharacterRecognition_Single(string plugInID, string plugInSettinsJson, Bitmap trimImage)
        {
            var result = "";

            try
            {
                //UI���f�h�~�̂��߁C�����F�����X�g�̃Z���N�g����������
                listBoxCharacterRecognitionSettings.ClearSelected();
                
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
                _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.STRING_CHARACTERRECOGNITION);
                throw;
            }

            return result;
        }

        private void ClearCharacterPluginUI()
        {
            //�����F���̃^�u�������ׂẴR���g���[���Ƀ`�F���W�C�x���g��ǉ�
            foreach (TabPage page in tabControlPluginCharacterRecognition.TabPages)
            {
                foreach (Control item in page.Controls)
                {
                    if (item is TextBox txb)
                    {
                        txb.Text = "";
                    }
                    else if (item is CheckBox chb)
                    {
                        chb.Checked = false;
                    }
                    else if (item is NumericUpDown nud)
                    {
                        nud.Value = nud.Minimum;
                    }
                    else if (item is ComboBox cmb)
                    {
                        cmb.SelectedIndex = -1;
                    }
                }
            }
        }

        #endregion

        #region �摜����
        /// <summary>
        /// �摜�����v���O�C����p���ĉ摜�������s���C�ϐ��ɃZ�b�g����
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
                    //�摜�����v���O�C����p���Ċe�������s��
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

                    //�摜���������摜���O���[�o���ϐ��ɃZ�b�g����
                    if (_processingImage != null)
                    {
                        _processingImage.Dispose();
                    }
                    _processingImage = (Bitmap)processingImage.Clone();
                }
                catch
                {
                    _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.STRING_IMAGEPROCESSING);
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
                    _timerErrorMessage = Properties.Resources.MESS_ERROR5;
                }
                throw;
            }
        }

        #endregion

        #region �^�C�}�[�֘A
        /// <summary>
        /// �^�C�}�[����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerEvent(object sender, EventArgs e)
        {
            try
            {
                //�F���t���O�̍X�V
                _beforeFlg = _afterFlg;

                //VideoCapture�v���O�C������摜���擾
                SetCaptureImageFromPlugin(UsepluginID_VideoCapture);
                if (CaptureImage == null)
                {
                    _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.MESS_ERROR2);
                    throw new Exception();
                }

                //�摜�F�����s���C�ǂ̃V�i���I���s�����𔻒�
                _afterFlg = ImageRecognitionFromPlugin(ref _rectAngleImageRecognition, out _matchRateImageRecognition);

                //�摜�F���̌��ʂŕ���
                //�V�i���I�v���O�C�������s����
                if (_plugin_Scenario != null)
                {
                    try
                    {
                        if (_afterFlg == true)
                        {
                            //�F��
                            if (_beforeFlg == true)
                            {
                                //�F�����F��
                                _plugin_Scenario.TrueTrueEvent(this);
                            }
                            else
                            {
                                //��F�����F��
                                _plugin_Scenario.FalseTrueEvent(this);
                            }
                        }
                        else
                        {
                            //��F��
                            if (_beforeFlg == true)
                            {
                                //�F������F��
                                _plugin_Scenario.TrueFalseEvent(this);
                            }
                            else
                            {
                                //��F������F��
                                _plugin_Scenario.FalseFalseEvent(this);
                            }
                        }
                    }
                    catch
                    {
                        if (_timerErrorMessage == null)
                        {
                            _timerErrorMessage = Properties.Resources.MESS_ERROR1;
                        }
                        throw;
                    }
                }
                else
                {
                    _timerErrorMessage = Properties.Resources.MESS_ERROR3;
                    throw new Exception();
                }

                //�r���[�A�̕\����㏈��
                try
                {
                    //��FPS�̌v��
                    labelReFPS.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_REFPS, ReFPSMeasurement().ToString());

                    //�摜�F���̌���
                    labelMatchRateImageRecognition.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_MATCHRATE,
                    string.Format(Properties.Resources.FORMAT_RATE, _matchRateImageRecognition.ToString("F3")));
                    labelRectangleImageRecognition_X.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_X, _rectAngleImageRecognition.X);
                    labelRectangleImageRecognition_Y.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_Y, _rectAngleImageRecognition.Y);
                    labelRectangleImageRecognition_Width.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_WIDTH, _rectAngleImageRecognition.Width);
                    labelRectangleImageRecognition_Height.Text = string.Format(Properties.Resources.FORMAT_KEYVALUE, Properties.Resources.STRING_HEIGHT, _rectAngleImageRecognition.Height);

                    //�r���[�A�ւ̔��f
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
                        //�摜�F���͈͂�\������
                        var rectangler = new PresetImageProcessing_DrawRectangle();
                        using (var image = (Bitmap)CaptureImage.Clone())
                        {
                            if (_viewImage != null)
                            {
                                _viewImage.Dispose();
                            }
                            _viewImage = rectangler.ProcessingResult(image, _rectAngleImageRecognition);
                        }

                        //�r���[�A�ɕ����F�����s���̈��\������
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
                    _timerErrorMessage = string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.MESS_ERROR7);
                    throw new Exception();
                }
            }
            catch
            {
                //�^�C�}�[�����̒�~
                ReadTimerStop();
            }
            finally
            {
                ControlLoad();
            }
        }

        /// <summary>
        /// �^�C�}�[���X�^�[�g���鏈��
        /// </summary>
        public void ReadTimerStart()
        {
            //�G���[�󋵂����Z�b�g
            _isTimerError = false;
            _timerErrorMessage = null;

            //�F���O��̏󋵂����Z�b�g
            _beforeFlg = false;
            _afterFlg = false;

            _isTimerPassing = true;
            _readTimer.Start();
        }
        /// <summary>
        /// �^�C�}�[���~�߂鏈��
        /// </summary>
        public void ReadTimerStop()
        {
            _readTimer.Stop();
            _isTimerPassing = false;

            //��x�G���[���b�Z�[�W���o������G���[�t���O�𗧂Ă�
            if (!_isTimerError && _timerErrorMessage != null)
            {
                MessageBox.Show(_messForm, _timerErrorMessage);
            }
            _isTimerError = true;

            //�F���O��̏󋵂����Z�b�g
            _beforeFlg = false;
            _afterFlg = false;

            ControlLoad();
        }

        /// <summary>
        /// FPS�̎��l���v������
        /// </summary>
        private int ReFPSMeasurement()
        {
            //FPS�̌v�����ʂ��擾����
            if (_swReFPS != null && _swReFPS.IsRunning == true)
            {
                // �v����~
                _swReFPS.Stop();

                // ���ʕ\��
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

            // FPS�v���J�n
            _swReFPS = new System.Diagnostics.Stopwatch();
            _swReFPS.Start();

            return _reFPS;
        }

        #endregion

        #region �ݒ�̕ۑ��E�Ǎ�
        /// <summary>
        /// ���݂̐ݒ��ǂݍ���
        /// </summary>
        /// <returns></returns>
        private Settings CurrentSettings()
        {
            var result = new Settings();

            try
            {
                //�f�����v���O�C���̐ݒ�̕ۑ�
                result.UsePluginID_VideoCapture = UsepluginID_VideoCapture;
                foreach (var item in _plugins_VideoCapture)
                {
                    result.SettingsJson_VideoCapture.Add(item.GetSettingsJson());
                }

                //�摜�F���v���O�C���̐ݒ�̕ۑ�
                result.UsePluginID_ImageRecognition = UsepluginID_ImageRecognition;
                foreach (var item in _plugins_ImageRecognition)
                {
                    result.SettingsJson_ImageRecognition.Add(item.GetSettingsJson());
                }

                //�摜�����v���O�C���̐ݒ�̕ۑ�
                foreach (var item in _plugins_ImageProcessing)
                {
                    result.SettingsJson_ImageProcessing.Add(item.GetSettingsJson());
                }

                //�����F���v���O�C���̐ݒ�̕ۑ�
                //GUI�̃`�F���W�C�x���g�ōs���̂ł����ł͂��Ȃ�
                
                //�����F�����X�g�̐ݒ�̕ۑ�
                result.CRSListJson = JsonConvert.SerializeObject(CRSList);

                //�L���v�`��No
                result.CaptureNo = this.CaptureNo;
                //�ő�L���v�`����
                result.CaptureImageListMax = this.CaptureImageListMax;

                //FPS
                result.FPS = this.FPS;

            }
            catch
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    Properties.Resources.MESS_ERROR9
                    ));
            }

            return result;
        }

        /// <summary>
        /// �����F���c�[���Ɗe�v���O�C���̐ݒ��ǂݍ���
        /// </summary>
        /// <param name="settings"></param>
        private void LoadSettings(Settings settings)
        {
            if (settings != null)
            {
                try
                {
                    //�f�����v���O�C��
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

                    //�摜�F���v���O�C��
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

                    //�摜�����v���O�C��
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

                    //�����F�����X�g
                    if (settings.CRSListJson != null)
                    {
                        var ocrList = JsonConvert.DeserializeObject<List<CharacterRecgnitionSettings>>(settings.CRSListJson);
                        CRSList_Clear();
                        if (ocrList != null)
                        {
                            foreach (var item in ocrList)
                            {
                                CRSList_Add(item);
                            }
                        }
                    }
                    else
                    {
                        CRSList_Clear();
                    }

                    //�L���v�`��No
                    this.CaptureNo = settings.CaptureNo;
                    //�ő�L���v�`����
                    this.CaptureImageListMax = settings.CaptureImageListMax;

                    //FPS
                    this.FPS = settings.FPS;
                }
                catch
                {
                    MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.STRING_SETTINGS));
                }
                finally
                {
                    ControlLoad();
                }
            }
        }

        /// <summary>
        /// ���݂̃V�X�e���ݒ��ǂݍ���
        /// </summary>
        /// <returns></returns>
        private SystemSettings CurrentSystemSettings()
        {
            var result = new SystemSettings();

            try
            {
                //�ݒ�t�@�C���̃_�E�����[�hAPI�����N
                result.SettingsDownloadAPIURL = this.SettingsDownloadAPIURL;
            }
            catch
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    Properties.Resources.MESS_ERROR9
                    ));
            }

            return result;
        }

        /// <summary>
        /// �����F���c�[���̃V�X�e���ݒ��ǂݍ���
        /// �����ł͎����F���̐ݒ�͓ǂݍ��܂Ȃ�
        /// </summary>
        /// <param name="settings"></param>
        private void LoadSystemSettings(SystemSettings settings)
        {
            if (settings != null)
            {
                try
                {
                    //�ݒ�t�@�C���̃_�E�����[�hAPI�����N
                    this.SettingsDownloadAPIURL = settings.SettingsDownloadAPIURL;
                }
                catch
                {
                    MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR, Properties.Resources.STRING_SYSTEMSETTINGS));
                }
                finally
                {
                    ControlLoad();
                }
            }
        }

        #endregion

        #region �R���g���[���֘A
        /// <summary>
        /// �ݒ�l���R���g���[���ɔ��f����
        /// </summary>
        private void ControlLoad()
        {
            //�R���{�{�b�N�X�̑I����Ԃ̔��f
            foreach (var item in comboBoxCaptureNo.Items)
            {
                if (CaptureNo == (int)item)
                {
                    comboBoxCaptureNo.SelectedItem = item;
                }
            }
            foreach (var item in comboBoxPluginVideoCapture.Items)
            {
                if (item is ComboItemPlugin)
                {
                    var plugin = (ComboItemPlugin)item;
                    if (plugin != null && UsepluginID_VideoCapture == plugin.ID)
                    {
                        comboBoxPluginVideoCapture.SelectedItem = item;
                    }
                }
            }
            foreach (var item in comboBoxPluginImageRecognition.Items)
            {
                if (item is ComboItemPlugin)
                {
                    var plugin = (ComboItemPlugin)item;
                    if (plugin != null && UsepluginID_ImageRecognition == plugin.ID)
                    {
                        comboBoxPluginImageRecognition.SelectedItem = item;
                    }
                }
            }

            //numericupdown�̔��f
            numericUpDownFPS.Value = FPS;

            ShellUnitChange();
        }

        /// <summary>
        /// �c�[���̏�Ԃ��V�F�����j�b�g�ɔ��f����
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
        /// ���\�[�X�t�@�C������e�R���g���[���̕\���𐮂���
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
            labelDLID.Text = Properties.Resources.STRING_DLID;
            labelSettingsList.Text = Properties.Resources.STRING_SETTINGSLIST;
            labelSettingsSave.Text = Properties.Resources.STRING_SETTINGSNAME;
            buttonSettingsSave.Text = Properties.Resources.BUTTON_SAVE;
            this.Text = Properties.Resources.TOOL_NAME + " ver" + CharConstants.VERSION;
        }

        #region �l�̐ݒ�t�@�C���̎擾
        /// <summary>
        /// WebAPI����w��̃����N�悩��ݒ�t�@�C���Ɖ摜���_�E�����[�h����
        /// </summary>
        private async void GetSettingsFromLink(string id)
        {
            try
            {
                var url = SettingsDownloadAPIURL + textBoxSettingDownloadID.Text;
                var json = await _pNWebRequest.GetRequest(url, 30000);
                var settingsresponse = JsonConvert.DeserializeObject<SettingsResponse>(json);
                if (settingsresponse != null)
                {
                    var filePath = CharConstants.sSETTINGS_PATH + settingsresponse.SettingsPath;
                    if (IsFileSave(filePath))
                    {
                        //�ݒ�t�@�C����ۑ�����
                        _pNJsonExtensions.SerializeToFile<Settings>(settingsresponse.Settings, filePath);

                        //�摜��ۑ�����
                        var isError = false;
                        foreach (var item in settingsresponse.Images)
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

                        if (isError)
                        {
                            MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                                string.Format(Properties.Resources.FORMAT_READERROR, Properties.Resources.MESS_ERROR8)
                                ));
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
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    string.Format(Properties.Resources.FORMAT_READERROR, Properties.Resources.STRING_SETTINGS)
                    ));
            }
        }

        /// <summary>
        /// �ݒ�t�@�C���ꗗ���R���{�{�b�N�X�ɃZ�b�g����
        /// </summary>
        private void SettingsIntoCombo()
        {
            try
            {
                //�f�B���N�g�������݂��Ă��Ȃ��Ƃ��͍쐬����
                if (!(Directory.Exists(CharConstants.sSETTINGS_PATH)))
                {
                    Directory.CreateDirectory(CharConstants.sSETTINGS_PATH);
                }

                string[] settings = Directory.GetFiles(CharConstants.sSETTINGS_PATH, "*.json", System.IO.SearchOption.AllDirectories);

                comboBoxSettingsList.Items.Clear();

                foreach (var item in settings)
                {
                    ComboItemSettings file = new ComboItemSettings();
                    file.FilePath = item;
                    file.FileName = Path.GetFileName(item).Replace(".json", "");
                    comboBoxSettingsList.Items.Add(file);
                }

            }
            catch
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    string.Format(Properties.Resources.FORMAT_READERROR, Properties.Resources.STRING_SETTINGS)
                    ));
            }
        }


        #endregion

        #endregion

        #region �X�N���[���V���b�g
        private void buttonScreenShot_Click(object sender, EventArgs e)
        {
            try
            {
                _plugin_ScreenShot.ScreenShot(CaptureImage, CharConstants.sIMAGE_PATH, this);
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR5);
            }
        }

        private void buttonScreenShotMatch_Click(object sender, EventArgs e)
        {
            try
            {
                IImageProcessing trim = new PresetImageProcessing_Trimming();
                using (Bitmap bitmap = trim.ProcessingResult(CaptureImage, _rectAngleImageRecognition))
                {
                    _plugin_ScreenShot.ScreenShot(bitmap, CharConstants.sIMAGE_PATH, this);
                }
            }
            catch
            {
                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR5);
            }
        }

        #endregion

        #region �t�@�C���̕ۑ���_�E�����[�h
        /// <summary>
        /// �w�肳�ꂽ�p�X�Ƀt�@�C����ۑ����邩���b�Z�[�W�{�b�N�X�Ŋm�F
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private bool IsFileSave(string filepath)
        {
            //�t�@�C���̏㏑�������邩�m�F
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
        /// ������URL���摜��ǂݍ��ݕۑ�����
        /// </summary>
        /// <param name="image"></param>
        private async void ImageSaveFromURL(IPNImageExtensions.ImageURL image)
        {
            try
            {
                var imagepath = CharConstants.sSETTINGS_PATH + image.Path;
                if (!string.IsNullOrEmpty(imagepath))
                {
                    var isSave = false;
                    image.Path = imagepath;
                    foreach (var item in _pNImageExtensionses)
                    {
                        isSave = await item.ImageSave(image);

                        if (isSave)
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

        #region private��Control�̃C�x���g�̃��\�b�h
        public void MainForm_Load(object sender, EventArgs e)
        {
            //�v���O�C���̓Ǎ�
            LoadPlugins();

            //�^�C�}�[�̏����ݒ�
            //�^�C�}�[��������
            _readTimer = new System.Windows.Forms.Timer();
            //�^�C�}�[�ɃC�x���g��o�^
            _readTimer.Tick += new EventHandler(TimerEvent);

            FPS = 70;

            //GUI�̐���
            _messForm.TopMost = true;
            labelSelectPluginCharacterRecognition.Text = "";
            //�R���{�{�b�N�X�̓ǂݍ���
            SettingsIntoCombo();
            ControlResourcesLoad();
            //�L���v�`��No�̗p��
            for (int i = 0; i < CaptureImageListMax; i++)
            {
                comboBoxCaptureNo.Items.Add(i);
            }

            //�ݒ�̓Ǎ�
            try
            {
                var settings = _pNJsonExtensions.DeserializeFromFile<SystemSettings>(CharConstants.sCONFIG_PATH);
                if (settings != null)
                {
                    LoadSystemSettings(settings);
                    LoadSettings(settings.Settings);
                }
            }
            catch
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    string.Format(Properties.Resources.FORMAT_READERROR, Properties.Resources.STRING_SETTINGS)
                    ));

                ControlLoad();
            }

        }

        private void comboBoxPluginVideoCapture_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = (ComboBox)sender;
            if (cmb.SelectedItem is ComboItemPlugin)
            {
                var plugin = (ComboItemPlugin)cmb.SelectedItem;
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
            if (cmb.SelectedItem is ComboItemPlugin)
            {
                var plugin = (ComboItemPlugin)cmb.SelectedItem;
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
                if (item != null && item is CharacterRecgnitionSettings && cmb.SelectedItem is ComboItemPlugin)
                {
                    var CRS = (CharacterRecgnitionSettings)item;
                    var plugin = (ComboItemPlugin)cmb.SelectedItem;
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
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_INPUT, Properties.Resources.STRING_KEYNAME));
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
            var cRSs = new List<CharacterRecgnitionSettings>();
            foreach (var item in listBoxCharacterRecognitionSettings.SelectedItems)
            {
                if (item != null)
                {
                    cRSs.Add((CharacterRecgnitionSettings)item);
                }
            }

            if (cRSs != null)
            {
                foreach (var item in cRSs)
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
                            labelSelectPluginCharacterRecognition.Text = Properties.Resources.MESS_ERROR6;
                        }

                        //�Y������v���O�C���̃^�u�y�[�W������Ε\������
                        foreach(TabPage tabpage in tabControlPluginCharacterRecognition.TabPages)
                        {
                            try
                            {
                                if (tabpage is ICharacterRecognition icr && icr.ID == CRS.PluginID)
                                {
                                    icr.LoadSettingsJson(CRS.PluginSettingsJson);
                                    tabControlPluginCharacterRecognition.SelectedTab = tabpage;
                                }
                            }
                            catch
                            {
                                MessageBox.Show(_messForm, Properties.Resources.MESS_ERROR6);
                            }
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
                var settngs = CurrentSystemSettings();
                settngs.Settings = CurrentSettings();
                _pNJsonExtensions.SerializeToFile<MainForm.SystemSettings>(settngs, CharConstants.sCONFIG_PATH);
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

        private void comboBoxSettingsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;

                if (cmb.SelectedItem is ComboItemSettings)
                {
                    var cmbitem = (ComboItemSettings)cmb.SelectedItem;

                    //�ݒ�̓Ǎ�
                    var settings = _pNJsonExtensions.DeserializeFromFile<Settings>(cmbitem.FilePath);
                    if (settings != null)
                    {
                        LoadSettings(settings);
                        textBoxSettingsName.Text = cmb.Text;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_ERROR,
                    string.Format(Properties.Resources.FORMAT_READERROR, Properties.Resources.STRING_SETTINGS)
                    ));
            }
        }

        private void buttonSettingsSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSettingsName.Text))
            {
                var path = string.Format(CharConstants.sSETTINGS_FORMATPATH, textBoxSettingsName.Text);
                if (IsFileSave(path))
                {
                    //�ݒ�̕ۑ�
                    _pNJsonExtensions.SerializeToFile<MainForm.Settings>(CurrentSettings(), path);
                    SettingsIntoCombo();
                }
            }
            else
            {
                MessageBox.Show(_messForm, string.Format(Properties.Resources.FORMAT_INPUT, Properties.Resources.STRING_SETTINGSNAME));
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            SettingsIntoCombo();
        }

        #endregion

        #region MainForm�̐ݒ�N���X
        class Settings
        {
            /// <summary>
            /// �g�p����f�����v���O�C����ID
            /// </summary>
            [JsonProperty("usingpluginid_videocapture")]
            public string UsePluginID_VideoCapture { get; set; }
            /// <summary>
            /// �f�����̐ݒ�Json�ꗗ
            /// </summary>
            [JsonProperty("settingsjson_videocapture")]
            public IList<string> SettingsJson_VideoCapture { get; set; } = new List<string>();

            /// <summary>
            /// �g�p����摜�F���v���O�C����ID
            /// </summary>
            [JsonProperty("usingpluginid_imagerecognition")]
            public string UsePluginID_ImageRecognition { get; set; }
            /// <summary>
            /// �摜�F���̐ݒ�Json�ꗗ
            /// </summary>
            [JsonProperty("settingsjson_imagerecognition")]
            public IList<string> SettingsJson_ImageRecognition { get; set; } = new List<string>();

            /// <summary>
            /// �摜�����̐ݒ�Json�ꗗ
            /// </summary>
            [JsonProperty("settingsjson_imageprocessing")]
            public IList<string> SettingsJson_ImageProcessing { get; set; } = new List<string>();

            /// <summary>
            /// �����F���̃��X�g
            /// </summary>
            [JsonProperty("crslistjson")]
            public string CRSListJson { get; set; }

            /// <summary>
            /// �L���v�`��No
            /// </summary>
            [JsonProperty("captureno")]
            public int CaptureNo { get; set; } = 0;
            /// <summary>
            /// �L���v�`���摜��ێ�����ő吔
            /// </summary>
            [JsonProperty("captureimagelistmax")]
            public int CaptureImageListMax { get; set; } = 5;

            /// <summary>
            /// FPS
            /// </summary>
            [JsonProperty("fps")]
            public int FPS { get; set; } = 70;

            /// <summary>
            /// �f�t�H���g�R���X�g���N�^
            /// </summary>
            public Settings()
            {

            }
        }
        class SystemSettings
        {
            /// <summary>
            /// �ݒ�t�@�C�����_�E�����[�h����API�̃����N
            /// </summary>
            [JsonProperty("settingsdownloadapiurl")]
            public string SettingsDownloadAPIURL { get; set; } = "";

            /// <summary>
            /// �ݒ�t�@�C��
            /// </summary>
            [JsonProperty("settings")]
            public Settings Settings { get; set; }

        }

        #endregion

        #region �����F���N���X
        /// <summary>
        /// �����F���̐ݒ�
        /// </summary>
        class CharacterRecgnitionSettings
        {
            /// <summary>
            /// Json�̃L�[�ɂ��Ȃ閼�O
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// �g�p����v���O�C����ID
            /// </summary>
            [JsonProperty("pluginid")]
            public string PluginID { get; set; }

            /// <summary>
            /// �g�p����v���O�C���̖��O
            /// </summary>
            [JsonProperty("pluginname")]
            public string PluginName { get; set; }

            /// <summary>
            /// �g�p����v���O�C���̐ݒ�Json
            /// </summary>
            [JsonProperty("pluginsettingsjson")]
            public string PluginSettingsJson { get; set; }

            /// <summary>
            /// ���W�FX
            /// </summary>
            [JsonProperty("x")]
            public int X { get; set; } = 0;
            /// <summary>
            /// ���W�FY
            /// </summary>
            [JsonProperty("y")]
            public int Y { get; set; } = 0;
            /// <summary>
            /// ��
            /// </summary>
            [JsonProperty("width")]
            public int Width { get; set; } = 40;
            /// <summary>
            /// ����
            /// </summary>
            [JsonProperty("height")]
            public int Height { get; set; } = 20;

            public override string ToString()
            {
                return Name;
            }

            /// <summary>
            /// �f�t�H���g�R���X�g���N�^
            /// </summary>
            public CharacterRecgnitionSettings()
            {

            }
            /// <summary>
            /// �������擾�����C���X�^���X��萶��
            /// </summary>
            /// <param name="source"></param>
            public CharacterRecgnitionSettings(CharacterRecgnitionSettings source)
            {
                Name = source.Name;
                PluginID = source.PluginID;
                PluginName = source.PluginName;
                PluginSettingsJson = source.PluginSettingsJson;
                X = source.X;
                Y = source.Y;
                Width = source.Width;
                Height = source.Height;
            }

        }

        #endregion

        #region WebAPI����擾�����ݒ�t�@�C���Ɖ摜���i�[����N���X
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

        #region Plugin��ComboBox�i�[�p�N���X
        /// <summary>
        /// Plugin��ComboBox�Ɋi�[���邽�߂̃N���X
        /// </summary>
        class ComboItemPlugin
        {
            public string Name { get; set; }
            public string ID { get; set; }

            public override string ToString() { return Name; }
        }

        #endregion

        #region �ݒ�pComboBox�i�[�p�N���X
        /// <summary>
        /// �ݒ�ꗗComboBox�Ɋi�[���邽�߂̃N���X
        /// </summary>
        public class ComboItemSettings
        {
            public string FileName { get; set; }

            public string FilePath { get; set; }

            public override string ToString()
            {
                return FileName;
            }
        }

        #endregion



        
    }
}
