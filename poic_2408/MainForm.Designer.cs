namespace poic_2408
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxViewer = new PictureBox();
            comboBoxPluginVideoCapture = new ComboBox();
            tabControlOption = new TabControl();
            tabPageSettings = new TabPage();
            buttonReload = new Button();
            labelSettingsSave = new Label();
            textBoxSettingsName = new TextBox();
            buttonSettingsSave = new Button();
            labelSettingsList = new Label();
            comboBoxSettingsList = new ComboBox();
            labelDLID = new Label();
            textBoxSettingDownloadID = new TextBox();
            buttonSettingDownload = new Button();
            tabPageObservation = new TabPage();
            buttonScreenShotMatch = new Button();
            labelRectangleImageRecognition_Height = new Label();
            labelRectangleImageRecognition_Width = new Label();
            buttonScreenShot = new Button();
            labelRectangleImageRecognition_Y = new Label();
            labelRectangleImageRecognition_X = new Label();
            labelMatchRateImageRecognition = new Label();
            labelReFPS = new Label();
            tabControlMain = new TabControl();
            tabPageVideoCapture = new TabPage();
            labelCaptureNo = new Label();
            comboBoxCaptureNo = new ComboBox();
            tabControlPluginVideoCapture = new TabControl();
            tabPagetabPageImageRecognition = new TabPage();
            tabControlPluginImageRecognition = new TabControl();
            comboBoxPluginImageRecognition = new ComboBox();
            tabPageCharacterRecognition = new TabPage();
            buttonOverWriteCharacterRecognition = new Button();
            textBoxCharacterRecognitionKeyName = new TextBox();
            labelCharacterRecognitionKeyName = new Label();
            labelSelectPluginCharacterRecognition = new Label();
            buttonCopyCharacterRecognition = new Button();
            tabControlPluginCharacterRecognition = new TabControl();
            comboBoxPluginCharacterRecognition = new ComboBox();
            numericUpDownCRSheight = new NumericUpDown();
            numericUpDownCRSy = new NumericUpDown();
            numericUpDownCRSwidth = new NumericUpDown();
            numericUpDownCRSx = new NumericUpDown();
            labelCRSheight = new Label();
            labelCRSwidth = new Label();
            labelCRSy = new Label();
            labelCRSx = new Label();
            listBoxCharacterRecognitionSettings = new ListBox();
            buttonDeleteCharacterRecognition = new Button();
            buttonAddCharacterRecognition = new Button();
            tabPageImageProcessing = new TabPage();
            buttonIPPluginIncrement = new Button();
            tabControlPluginImageProcessing = new TabControl();
            panelViewer = new Panel();
            labelViewer = new Label();
            radioButtonImageProcessing = new RadioButton();
            radioButtonImageRecognition = new RadioButton();
            radioButtonDefault = new RadioButton();
            buttonTImerStartStop = new Button();
            buttonCharacterRecognition = new Button();
            shellUnitBase = new Panel();
            shellUnit3 = new Panel();
            shellUnit2 = new Panel();
            shellUnit1 = new Panel();
            numericUpDownFPS = new NumericUpDown();
            labelFPS = new Label();
            checkBoxTopMost = new CheckBox();
            buttonIPPluginDecrement = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxViewer).BeginInit();
            tabControlOption.SuspendLayout();
            tabPageSettings.SuspendLayout();
            tabPageObservation.SuspendLayout();
            tabControlMain.SuspendLayout();
            tabPageVideoCapture.SuspendLayout();
            tabPagetabPageImageRecognition.SuspendLayout();
            tabPageCharacterRecognition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSheight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSwidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSx).BeginInit();
            tabPageImageProcessing.SuspendLayout();
            panelViewer.SuspendLayout();
            shellUnitBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFPS).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxViewer
            // 
            pictureBoxViewer.Location = new Point(12, 25);
            pictureBoxViewer.Name = "pictureBoxViewer";
            pictureBoxViewer.Size = new Size(238, 157);
            pictureBoxViewer.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxViewer.TabIndex = 0;
            pictureBoxViewer.TabStop = false;
            // 
            // comboBoxPluginVideoCapture
            // 
            comboBoxPluginVideoCapture.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPluginVideoCapture.FormattingEnabled = true;
            comboBoxPluginVideoCapture.Location = new Point(6, 6);
            comboBoxPluginVideoCapture.Name = "comboBoxPluginVideoCapture";
            comboBoxPluginVideoCapture.Size = new Size(274, 23);
            comboBoxPluginVideoCapture.TabIndex = 5;
            comboBoxPluginVideoCapture.SelectedIndexChanged += comboBoxPluginVideoCapture_SelectedIndexChanged;
            // 
            // tabControlOption
            // 
            tabControlOption.Controls.Add(tabPageSettings);
            tabControlOption.Controls.Add(tabPageObservation);
            tabControlOption.Location = new Point(12, 217);
            tabControlOption.Name = "tabControlOption";
            tabControlOption.SelectedIndex = 0;
            tabControlOption.Size = new Size(238, 221);
            tabControlOption.TabIndex = 6;
            // 
            // tabPageSettings
            // 
            tabPageSettings.Controls.Add(buttonReload);
            tabPageSettings.Controls.Add(labelSettingsSave);
            tabPageSettings.Controls.Add(textBoxSettingsName);
            tabPageSettings.Controls.Add(buttonSettingsSave);
            tabPageSettings.Controls.Add(labelSettingsList);
            tabPageSettings.Controls.Add(comboBoxSettingsList);
            tabPageSettings.Controls.Add(labelDLID);
            tabPageSettings.Controls.Add(textBoxSettingDownloadID);
            tabPageSettings.Controls.Add(buttonSettingDownload);
            tabPageSettings.Location = new Point(4, 24);
            tabPageSettings.Name = "tabPageSettings";
            tabPageSettings.Padding = new Padding(3);
            tabPageSettings.Size = new Size(230, 193);
            tabPageSettings.TabIndex = 1;
            tabPageSettings.Text = "設定";
            tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // buttonReload
            // 
            buttonReload.Location = new Point(65, 79);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(75, 23);
            buttonReload.TabIndex = 8;
            buttonReload.Text = "再読込";
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // labelSettingsSave
            // 
            labelSettingsSave.AutoSize = true;
            labelSettingsSave.Location = new Point(6, 53);
            labelSettingsSave.Name = "labelSettingsSave";
            labelSettingsSave.Size = new Size(43, 15);
            labelSettingsSave.TabIndex = 6;
            labelSettingsSave.Text = "設定名";
            // 
            // textBoxSettingsName
            // 
            textBoxSettingsName.Location = new Point(55, 51);
            textBoxSettingsName.Name = "textBoxSettingsName";
            textBoxSettingsName.Size = new Size(166, 23);
            textBoxSettingsName.TabIndex = 1;
            // 
            // buttonSettingsSave
            // 
            buttonSettingsSave.Location = new Point(146, 79);
            buttonSettingsSave.Name = "buttonSettingsSave";
            buttonSettingsSave.Size = new Size(75, 23);
            buttonSettingsSave.TabIndex = 2;
            buttonSettingsSave.Text = "保存";
            buttonSettingsSave.UseVisualStyleBackColor = true;
            buttonSettingsSave.Click += buttonSettingsSave_Click;
            // 
            // labelSettingsList
            // 
            labelSettingsList.AutoSize = true;
            labelSettingsList.Location = new Point(3, 3);
            labelSettingsList.Name = "labelSettingsList";
            labelSettingsList.Size = new Size(55, 15);
            labelSettingsList.TabIndex = 5;
            labelSettingsList.Text = "設定一覧";
            // 
            // comboBoxSettingsList
            // 
            comboBoxSettingsList.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSettingsList.FormattingEnabled = true;
            comboBoxSettingsList.Location = new Point(6, 21);
            comboBoxSettingsList.Name = "comboBoxSettingsList";
            comboBoxSettingsList.Size = new Size(215, 23);
            comboBoxSettingsList.TabIndex = 0;
            comboBoxSettingsList.SelectedIndexChanged += comboBoxSettingsList_SelectedIndexChanged;
            // 
            // labelDLID
            // 
            labelDLID.AutoSize = true;
            labelDLID.Location = new Point(6, 111);
            labelDLID.Name = "labelDLID";
            labelDLID.Size = new Size(32, 15);
            labelDLID.TabIndex = 7;
            labelDLID.Text = "DLID";
            // 
            // textBoxSettingDownloadID
            // 
            textBoxSettingDownloadID.Location = new Point(55, 108);
            textBoxSettingDownloadID.Name = "textBoxSettingDownloadID";
            textBoxSettingDownloadID.Size = new Size(166, 23);
            textBoxSettingDownloadID.TabIndex = 3;
            // 
            // buttonSettingDownload
            // 
            buttonSettingDownload.Location = new Point(146, 137);
            buttonSettingDownload.Name = "buttonSettingDownload";
            buttonSettingDownload.Size = new Size(75, 23);
            buttonSettingDownload.TabIndex = 4;
            buttonSettingDownload.Text = "ダウンロード";
            buttonSettingDownload.UseVisualStyleBackColor = true;
            buttonSettingDownload.Click += buttonSettingDownload_Click;
            // 
            // tabPageObservation
            // 
            tabPageObservation.Controls.Add(buttonScreenShotMatch);
            tabPageObservation.Controls.Add(labelRectangleImageRecognition_Height);
            tabPageObservation.Controls.Add(labelRectangleImageRecognition_Width);
            tabPageObservation.Controls.Add(buttonScreenShot);
            tabPageObservation.Controls.Add(labelRectangleImageRecognition_Y);
            tabPageObservation.Controls.Add(labelRectangleImageRecognition_X);
            tabPageObservation.Controls.Add(labelMatchRateImageRecognition);
            tabPageObservation.Controls.Add(labelReFPS);
            tabPageObservation.Location = new Point(4, 24);
            tabPageObservation.Name = "tabPageObservation";
            tabPageObservation.Padding = new Padding(3);
            tabPageObservation.Size = new Size(230, 193);
            tabPageObservation.TabIndex = 0;
            tabPageObservation.Text = Properties.Resources.STRING_OBSERVATION;
            tabPageObservation.UseVisualStyleBackColor = true;
            // 
            // buttonScreenShotMatch
            // 
            buttonScreenShotMatch.Location = new Point(149, 62);
            buttonScreenShotMatch.Name = "buttonScreenShotMatch";
            buttonScreenShotMatch.Size = new Size(75, 23);
            buttonScreenShotMatch.TabIndex = 1;
            buttonScreenShotMatch.Text = "PrtScMatch";
            buttonScreenShotMatch.UseVisualStyleBackColor = true;
            buttonScreenShotMatch.Click += buttonScreenShotMatch_Click;
            // 
            // labelRectangleImageRecognition_Height
            // 
            labelRectangleImageRecognition_Height.AutoSize = true;
            labelRectangleImageRecognition_Height.Location = new Point(6, 78);
            labelRectangleImageRecognition_Height.Name = "labelRectangleImageRecognition_Height";
            labelRectangleImageRecognition_Height.Size = new Size(51, 15);
            labelRectangleImageRecognition_Height.TabIndex = 12;
            labelRectangleImageRecognition_Height.Text = "Height:?";
            // 
            // labelRectangleImageRecognition_Width
            // 
            labelRectangleImageRecognition_Width.AutoSize = true;
            labelRectangleImageRecognition_Width.Location = new Point(6, 63);
            labelRectangleImageRecognition_Width.Name = "labelRectangleImageRecognition_Width";
            labelRectangleImageRecognition_Width.Size = new Size(47, 15);
            labelRectangleImageRecognition_Width.TabIndex = 11;
            labelRectangleImageRecognition_Width.Text = "Width:?";
            // 
            // buttonScreenShot
            // 
            buttonScreenShot.Location = new Point(149, 33);
            buttonScreenShot.Name = "buttonScreenShot";
            buttonScreenShot.Size = new Size(75, 23);
            buttonScreenShot.TabIndex = 0;
            buttonScreenShot.Text = "PrtSc";
            buttonScreenShot.UseVisualStyleBackColor = true;
            buttonScreenShot.Click += buttonScreenShot_Click;
            // 
            // labelRectangleImageRecognition_Y
            // 
            labelRectangleImageRecognition_Y.AutoSize = true;
            labelRectangleImageRecognition_Y.Location = new Point(6, 48);
            labelRectangleImageRecognition_Y.Name = "labelRectangleImageRecognition_Y";
            labelRectangleImageRecognition_Y.Size = new Size(21, 15);
            labelRectangleImageRecognition_Y.TabIndex = 10;
            labelRectangleImageRecognition_Y.Text = "Y:?";
            // 
            // labelRectangleImageRecognition_X
            // 
            labelRectangleImageRecognition_X.AutoSize = true;
            labelRectangleImageRecognition_X.Location = new Point(6, 33);
            labelRectangleImageRecognition_X.Name = "labelRectangleImageRecognition_X";
            labelRectangleImageRecognition_X.Size = new Size(22, 15);
            labelRectangleImageRecognition_X.TabIndex = 9;
            labelRectangleImageRecognition_X.Text = "X:?";
            // 
            // labelMatchRateImageRecognition
            // 
            labelMatchRateImageRecognition.AutoSize = true;
            labelMatchRateImageRecognition.Location = new Point(6, 18);
            labelMatchRateImageRecognition.Name = "labelMatchRateImageRecognition";
            labelMatchRateImageRecognition.Size = new Size(61, 15);
            labelMatchRateImageRecognition.TabIndex = 8;
            labelMatchRateImageRecognition.Text = "一致率:?%";
            // 
            // labelReFPS
            // 
            labelReFPS.AutoSize = true;
            labelReFPS.Location = new Point(6, 3);
            labelReFPS.Name = "labelReFPS";
            labelReFPS.Size = new Size(46, 15);
            labelReFPS.TabIndex = 7;
            labelReFPS.Text = "実FPS:?";
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageVideoCapture);
            tabControlMain.Controls.Add(tabPagetabPageImageRecognition);
            tabControlMain.Controls.Add(tabPageCharacterRecognition);
            tabControlMain.Controls.Add(tabPageImageProcessing);
            tabControlMain.Location = new Point(256, 44);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(294, 394);
            tabControlMain.TabIndex = 9;
            // 
            // tabPageVideoCapture
            // 
            tabPageVideoCapture.Controls.Add(labelCaptureNo);
            tabPageVideoCapture.Controls.Add(comboBoxCaptureNo);
            tabPageVideoCapture.Controls.Add(tabControlPluginVideoCapture);
            tabPageVideoCapture.Controls.Add(comboBoxPluginVideoCapture);
            tabPageVideoCapture.Location = new Point(4, 24);
            tabPageVideoCapture.Name = "tabPageVideoCapture";
            tabPageVideoCapture.Padding = new Padding(3);
            tabPageVideoCapture.Size = new Size(286, 366);
            tabPageVideoCapture.TabIndex = 3;
            tabPageVideoCapture.Text = Properties.Resources.STRING_VIDEOCAPTURE;
            tabPageVideoCapture.UseVisualStyleBackColor = true;
            // 
            // labelCaptureNo
            // 
            labelCaptureNo.AutoSize = true;
            labelCaptureNo.Location = new Point(6, 38);
            labelCaptureNo.Name = "labelCaptureNo";
            labelCaptureNo.Size = new Size(66, 15);
            labelCaptureNo.TabIndex = 26;
            labelCaptureNo.Text = "キャプチャNo";
            // 
            // comboBoxCaptureNo
            // 
            comboBoxCaptureNo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCaptureNo.FormattingEnabled = true;
            comboBoxCaptureNo.Location = new Point(97, 35);
            comboBoxCaptureNo.Name = "comboBoxCaptureNo";
            comboBoxCaptureNo.Size = new Size(183, 23);
            comboBoxCaptureNo.TabIndex = 25;
            comboBoxCaptureNo.SelectedIndexChanged += comboBoxCaptureNo_SelectedIndexChanged;
            // 
            // tabControlPluginVideoCapture
            // 
            tabControlPluginVideoCapture.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlPluginVideoCapture.Location = new Point(6, 70);
            tabControlPluginVideoCapture.Name = "tabControlPluginVideoCapture";
            tabControlPluginVideoCapture.SelectedIndex = 0;
            tabControlPluginVideoCapture.Size = new Size(274, 290);
            tabControlPluginVideoCapture.TabIndex = 24;
            // 
            // tabPagetabPageImageRecognition
            // 
            tabPagetabPageImageRecognition.Controls.Add(tabControlPluginImageRecognition);
            tabPagetabPageImageRecognition.Controls.Add(comboBoxPluginImageRecognition);
            tabPagetabPageImageRecognition.Location = new Point(4, 24);
            tabPagetabPageImageRecognition.Name = "tabPagetabPageImageRecognition";
            tabPagetabPageImageRecognition.Padding = new Padding(3);
            tabPagetabPageImageRecognition.Size = new Size(286, 366);
            tabPagetabPageImageRecognition.TabIndex = 2;
            tabPagetabPageImageRecognition.Text = Properties.Resources.STRING_IMAGERECOGNITION;
            tabPagetabPageImageRecognition.UseVisualStyleBackColor = true;
            // 
            // tabControlPluginImageRecognition
            // 
            tabControlPluginImageRecognition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlPluginImageRecognition.Location = new Point(6, 35);
            tabControlPluginImageRecognition.Name = "tabControlPluginImageRecognition";
            tabControlPluginImageRecognition.SelectedIndex = 0;
            tabControlPluginImageRecognition.Size = new Size(274, 325);
            tabControlPluginImageRecognition.TabIndex = 23;
            // 
            // comboBoxPluginImageRecognition
            // 
            comboBoxPluginImageRecognition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPluginImageRecognition.FormattingEnabled = true;
            comboBoxPluginImageRecognition.Location = new Point(6, 6);
            comboBoxPluginImageRecognition.Name = "comboBoxPluginImageRecognition";
            comboBoxPluginImageRecognition.Size = new Size(274, 23);
            comboBoxPluginImageRecognition.TabIndex = 6;
            comboBoxPluginImageRecognition.SelectedIndexChanged += comboBoxPluginImageRecognition_SelectedIndexChanged;
            // 
            // tabPageCharacterRecognition
            // 
            tabPageCharacterRecognition.Controls.Add(buttonOverWriteCharacterRecognition);
            tabPageCharacterRecognition.Controls.Add(textBoxCharacterRecognitionKeyName);
            tabPageCharacterRecognition.Controls.Add(labelCharacterRecognitionKeyName);
            tabPageCharacterRecognition.Controls.Add(labelSelectPluginCharacterRecognition);
            tabPageCharacterRecognition.Controls.Add(buttonCopyCharacterRecognition);
            tabPageCharacterRecognition.Controls.Add(tabControlPluginCharacterRecognition);
            tabPageCharacterRecognition.Controls.Add(comboBoxPluginCharacterRecognition);
            tabPageCharacterRecognition.Controls.Add(numericUpDownCRSheight);
            tabPageCharacterRecognition.Controls.Add(numericUpDownCRSy);
            tabPageCharacterRecognition.Controls.Add(numericUpDownCRSwidth);
            tabPageCharacterRecognition.Controls.Add(numericUpDownCRSx);
            tabPageCharacterRecognition.Controls.Add(labelCRSheight);
            tabPageCharacterRecognition.Controls.Add(labelCRSwidth);
            tabPageCharacterRecognition.Controls.Add(labelCRSy);
            tabPageCharacterRecognition.Controls.Add(labelCRSx);
            tabPageCharacterRecognition.Controls.Add(listBoxCharacterRecognitionSettings);
            tabPageCharacterRecognition.Controls.Add(buttonDeleteCharacterRecognition);
            tabPageCharacterRecognition.Controls.Add(buttonAddCharacterRecognition);
            tabPageCharacterRecognition.Location = new Point(4, 24);
            tabPageCharacterRecognition.Name = "tabPageCharacterRecognition";
            tabPageCharacterRecognition.Padding = new Padding(3);
            tabPageCharacterRecognition.Size = new Size(286, 366);
            tabPageCharacterRecognition.TabIndex = 0;
            tabPageCharacterRecognition.Text = Properties.Resources.STRING_CHARACTERRECOGNITION;
            tabPageCharacterRecognition.UseVisualStyleBackColor = true;
            // 
            // buttonOverWriteCharacterRecognition
            // 
            buttonOverWriteCharacterRecognition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOverWriteCharacterRecognition.Location = new Point(226, 103);
            buttonOverWriteCharacterRecognition.Name = "buttonOverWriteCharacterRecognition";
            buttonOverWriteCharacterRecognition.Size = new Size(54, 23);
            buttonOverWriteCharacterRecognition.TabIndex = 26;
            buttonOverWriteCharacterRecognition.Text = Properties.Resources.BUTTON_OVERWRITE;
            buttonOverWriteCharacterRecognition.UseVisualStyleBackColor = true;
            buttonOverWriteCharacterRecognition.Click += buttonOverWriteCharacterRecognition_Click;
            // 
            // textBoxCharacterRecognitionKeyName
            // 
            textBoxCharacterRecognitionKeyName.Location = new Point(73, 103);
            textBoxCharacterRecognitionKeyName.Name = "textBoxCharacterRecognitionKeyName";
            textBoxCharacterRecognitionKeyName.Size = new Size(147, 23);
            textBoxCharacterRecognitionKeyName.TabIndex = 25;
            // 
            // labelCharacterRecognitionKeyName
            // 
            labelCharacterRecognitionKeyName.AutoSize = true;
            labelCharacterRecognitionKeyName.Location = new Point(31, 106);
            labelCharacterRecognitionKeyName.Name = "labelCharacterRecognitionKeyName";
            labelCharacterRecognitionKeyName.RightToLeft = RightToLeft.Yes;
            labelCharacterRecognitionKeyName.Size = new Size(36, 15);
            labelCharacterRecognitionKeyName.TabIndex = 24;
            labelCharacterRecognitionKeyName.Text = "キー名";
            // 
            // labelSelectPluginCharacterRecognition
            // 
            labelSelectPluginCharacterRecognition.AutoSize = true;
            labelSelectPluginCharacterRecognition.Location = new Point(6, 158);
            labelSelectPluginCharacterRecognition.Name = "labelSelectPluginCharacterRecognition";
            labelSelectPluginCharacterRecognition.RightToLeft = RightToLeft.Yes;
            labelSelectPluginCharacterRecognition.Size = new Size(99, 15);
            labelSelectPluginCharacterRecognition.TabIndex = 23;
            labelSelectPluginCharacterRecognition.Text = "文字認識プラグイン";
            labelSelectPluginCharacterRecognition.Click += labelSelectPluginCharacterRecognition_Click;
            // 
            // buttonCopyCharacterRecognition
            // 
            buttonCopyCharacterRecognition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCopyCharacterRecognition.Location = new Point(226, 35);
            buttonCopyCharacterRecognition.Name = "buttonCopyCharacterRecognition";
            buttonCopyCharacterRecognition.Size = new Size(54, 23);
            buttonCopyCharacterRecognition.TabIndex = 11;
            buttonCopyCharacterRecognition.Text = Properties.Resources.BUTTON_COPY;
            buttonCopyCharacterRecognition.UseVisualStyleBackColor = true;
            buttonCopyCharacterRecognition.Click += buttonCopyCharacterRecognition_Click;
            // 
            // tabControlPluginCharacterRecognition
            // 
            tabControlPluginCharacterRecognition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlPluginCharacterRecognition.Location = new Point(6, 234);
            tabControlPluginCharacterRecognition.Name = "tabControlPluginCharacterRecognition";
            tabControlPluginCharacterRecognition.SelectedIndex = 0;
            tabControlPluginCharacterRecognition.Size = new Size(274, 126);
            tabControlPluginCharacterRecognition.TabIndex = 22;
            // 
            // comboBoxPluginCharacterRecognition
            // 
            comboBoxPluginCharacterRecognition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxPluginCharacterRecognition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPluginCharacterRecognition.FormattingEnabled = true;
            comboBoxPluginCharacterRecognition.Location = new Point(6, 132);
            comboBoxPluginCharacterRecognition.Name = "comboBoxPluginCharacterRecognition";
            comboBoxPluginCharacterRecognition.Size = new Size(274, 23);
            comboBoxPluginCharacterRecognition.TabIndex = 21;
            comboBoxPluginCharacterRecognition.SelectedIndexChanged += comboBoxPluginCharacterRecognition_SelectedIndexChanged;
            // 
            // numericUpDownCRSheight
            // 
            numericUpDownCRSheight.Location = new Point(189, 205);
            numericUpDownCRSheight.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownCRSheight.Name = "numericUpDownCRSheight";
            numericUpDownCRSheight.Size = new Size(91, 23);
            numericUpDownCRSheight.TabIndex = 20;
            numericUpDownCRSheight.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCRSheight.ValueChanged += numericUpDownCRSheight_ValueChanged;
            // 
            // numericUpDownCRSy
            // 
            numericUpDownCRSy.Location = new Point(43, 205);
            numericUpDownCRSy.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownCRSy.Name = "numericUpDownCRSy";
            numericUpDownCRSy.Size = new Size(91, 23);
            numericUpDownCRSy.TabIndex = 19;
            numericUpDownCRSy.ValueChanged += numericUpDownCRSy_ValueChanged;
            // 
            // numericUpDownCRSwidth
            // 
            numericUpDownCRSwidth.Location = new Point(189, 176);
            numericUpDownCRSwidth.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownCRSwidth.Name = "numericUpDownCRSwidth";
            numericUpDownCRSwidth.Size = new Size(91, 23);
            numericUpDownCRSwidth.TabIndex = 18;
            numericUpDownCRSwidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCRSwidth.ValueChanged += numericUpDownCRSwidth_ValueChanged;
            // 
            // numericUpDownCRSx
            // 
            numericUpDownCRSx.Location = new Point(43, 176);
            numericUpDownCRSx.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownCRSx.Name = "numericUpDownCRSx";
            numericUpDownCRSx.Size = new Size(91, 23);
            numericUpDownCRSx.TabIndex = 17;
            numericUpDownCRSx.ValueChanged += numericUpDownCRSx_ValueChanged;
            // 
            // labelCRSheight
            // 
            labelCRSheight.AutoSize = true;
            labelCRSheight.Location = new Point(140, 207);
            labelCRSheight.Name = "labelCRSheight";
            labelCRSheight.RightToLeft = RightToLeft.Yes;
            labelCRSheight.Size = new Size(43, 15);
            labelCRSheight.TabIndex = 16;
            labelCRSheight.Text = "Height";
            // 
            // labelCRSwidth
            // 
            labelCRSwidth.AutoSize = true;
            labelCRSwidth.Location = new Point(144, 178);
            labelCRSwidth.Name = "labelCRSwidth";
            labelCRSwidth.RightToLeft = RightToLeft.Yes;
            labelCRSwidth.Size = new Size(39, 15);
            labelCRSwidth.TabIndex = 15;
            labelCRSwidth.Text = "Width";
            // 
            // labelCRSy
            // 
            labelCRSy.AutoSize = true;
            labelCRSy.Location = new Point(23, 207);
            labelCRSy.Name = "labelCRSy";
            labelCRSy.RightToLeft = RightToLeft.Yes;
            labelCRSy.Size = new Size(14, 15);
            labelCRSy.TabIndex = 14;
            labelCRSy.Text = "Y";
            // 
            // labelCRSx
            // 
            labelCRSx.AutoSize = true;
            labelCRSx.Location = new Point(23, 178);
            labelCRSx.Name = "labelCRSx";
            labelCRSx.RightToLeft = RightToLeft.Yes;
            labelCRSx.Size = new Size(14, 15);
            labelCRSx.TabIndex = 13;
            labelCRSx.Text = "X";
            // 
            // listBoxCharacterRecognitionSettings
            // 
            listBoxCharacterRecognitionSettings.FormattingEnabled = true;
            listBoxCharacterRecognitionSettings.ItemHeight = 15;
            listBoxCharacterRecognitionSettings.Location = new Point(6, 6);
            listBoxCharacterRecognitionSettings.Name = "listBoxCharacterRecognitionSettings";
            listBoxCharacterRecognitionSettings.SelectionMode = SelectionMode.MultiSimple;
            listBoxCharacterRecognitionSettings.Size = new Size(214, 94);
            listBoxCharacterRecognitionSettings.TabIndex = 12;
            listBoxCharacterRecognitionSettings.SelectedIndexChanged += listBoxCharacterRecognitionSettings_SelectedIndexChanged;
            // 
            // buttonDeleteCharacterRecognition
            // 
            buttonDeleteCharacterRecognition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDeleteCharacterRecognition.Location = new Point(226, 64);
            buttonDeleteCharacterRecognition.Name = "buttonDeleteCharacterRecognition";
            buttonDeleteCharacterRecognition.Size = new Size(54, 23);
            buttonDeleteCharacterRecognition.TabIndex = 11;
            buttonDeleteCharacterRecognition.Text = Properties.Resources.BUTTON_DELETE;
            buttonDeleteCharacterRecognition.UseVisualStyleBackColor = true;
            buttonDeleteCharacterRecognition.Click += buttonDeleteCharacterRecognition_Click;
            // 
            // buttonAddCharacterRecognition
            // 
            buttonAddCharacterRecognition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddCharacterRecognition.Location = new Point(226, 6);
            buttonAddCharacterRecognition.Name = "buttonAddCharacterRecognition";
            buttonAddCharacterRecognition.Size = new Size(54, 23);
            buttonAddCharacterRecognition.TabIndex = 10;
            buttonAddCharacterRecognition.Text = Properties.Resources.BUTTON_ADD;
            buttonAddCharacterRecognition.UseVisualStyleBackColor = true;
            buttonAddCharacterRecognition.Click += buttonAddCharacterRecognition_Click;
            // 
            // tabPageImageProcessing
            // 
            tabPageImageProcessing.Controls.Add(buttonIPPluginDecrement);
            tabPageImageProcessing.Controls.Add(buttonIPPluginIncrement);
            tabPageImageProcessing.Controls.Add(tabControlPluginImageProcessing);
            tabPageImageProcessing.Location = new Point(4, 24);
            tabPageImageProcessing.Name = "tabPageImageProcessing";
            tabPageImageProcessing.Padding = new Padding(3);
            tabPageImageProcessing.Size = new Size(286, 366);
            tabPageImageProcessing.TabIndex = 1;
            tabPageImageProcessing.Text = Properties.Resources.STRING_IMAGEPROCESSING;
            tabPageImageProcessing.UseVisualStyleBackColor = true;
            // 
            // buttonIPPluginIncrement
            // 
            buttonIPPluginIncrement.Location = new Point(87, 6);
            buttonIPPluginIncrement.Name = "buttonIPPluginIncrement";
            buttonIPPluginIncrement.Size = new Size(75, 23);
            buttonIPPluginIncrement.TabIndex = 1;
            buttonIPPluginIncrement.Text = ">>";
            buttonIPPluginIncrement.UseVisualStyleBackColor = true;
            buttonIPPluginIncrement.Click += buttonIPPluginIncrement_Click;
            // 
            // tabControlPluginImageProcessing
            // 
            tabControlPluginImageProcessing.Location = new Point(6, 35);
            tabControlPluginImageProcessing.Name = "tabControlPluginImageProcessing";
            tabControlPluginImageProcessing.SelectedIndex = 0;
            tabControlPluginImageProcessing.Size = new Size(274, 325);
            tabControlPluginImageProcessing.TabIndex = 0;
            // 
            // panelViewer
            // 
            panelViewer.Controls.Add(labelViewer);
            panelViewer.Controls.Add(radioButtonImageProcessing);
            panelViewer.Controls.Add(radioButtonImageRecognition);
            panelViewer.Controls.Add(radioButtonDefault);
            panelViewer.Location = new Point(256, 12);
            panelViewer.Name = "panelViewer";
            panelViewer.Size = new Size(290, 26);
            panelViewer.TabIndex = 11;
            // 
            // labelViewer
            // 
            labelViewer.AutoSize = true;
            labelViewer.Location = new Point(3, 5);
            labelViewer.Name = "labelViewer";
            labelViewer.Size = new Size(40, 15);
            labelViewer.TabIndex = 14;
            labelViewer.Text = "ビューア";
            // 
            // radioButtonImageProcessing
            // 
            radioButtonImageProcessing.AutoSize = true;
            radioButtonImageProcessing.Location = new Point(213, 3);
            radioButtonImageProcessing.Name = "radioButtonImageProcessing";
            radioButtonImageProcessing.Size = new Size(73, 19);
            radioButtonImageProcessing.TabIndex = 2;
            radioButtonImageProcessing.Text = Properties.Resources.STRING_IMAGEPROCESSING;
            radioButtonImageProcessing.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageRecognition
            // 
            radioButtonImageRecognition.AutoSize = true;
            radioButtonImageRecognition.Location = new Point(134, 3);
            radioButtonImageRecognition.Name = "radioButtonImageRecognition";
            radioButtonImageRecognition.Size = new Size(73, 19);
            radioButtonImageRecognition.TabIndex = 1;
            radioButtonImageRecognition.Text = Properties.Resources.STRING_IMAGERECOGNITION;
            radioButtonImageRecognition.UseVisualStyleBackColor = true;
            // 
            // radioButtonDefault
            // 
            radioButtonDefault.AutoSize = true;
            radioButtonDefault.Checked = true;
            radioButtonDefault.Location = new Point(60, 3);
            radioButtonDefault.Name = "radioButtonDefault";
            radioButtonDefault.Size = new Size(68, 19);
            radioButtonDefault.TabIndex = 0;
            radioButtonDefault.TabStop = true;
            radioButtonDefault.Text = Properties.Resources.STRING_DEFAULT;
            radioButtonDefault.UseVisualStyleBackColor = true;
            // 
            // buttonTImerStartStop
            // 
            buttonTImerStartStop.Location = new Point(94, 188);
            buttonTImerStartStop.Name = "buttonTImerStartStop";
            buttonTImerStartStop.Size = new Size(75, 23);
            buttonTImerStartStop.TabIndex = 12;
            buttonTImerStartStop.Text = Properties.Resources.BUTTON_STARTSTOP;
            buttonTImerStartStop.UseVisualStyleBackColor = true;
            buttonTImerStartStop.Click += buttonTImerStartStop_Click;
            // 
            // buttonCharacterRecognition
            // 
            buttonCharacterRecognition.Location = new Point(175, 188);
            buttonCharacterRecognition.Name = "buttonCharacterRecognition";
            buttonCharacterRecognition.Size = new Size(75, 23);
            buttonCharacterRecognition.TabIndex = 13;
            buttonCharacterRecognition.Text = Properties.Resources.STRING_CHARACTERRECOGNITION;
            buttonCharacterRecognition.UseVisualStyleBackColor = true;
            buttonCharacterRecognition.Click += buttonCharacterRecognition_Click;
            // 
            // shellUnitBase
            // 
            shellUnitBase.BackColor = Color.Black;
            shellUnitBase.Controls.Add(shellUnit3);
            shellUnitBase.Controls.Add(shellUnit2);
            shellUnitBase.Controls.Add(shellUnit1);
            shellUnitBase.Location = new Point(0, 241);
            shellUnitBase.Name = "shellUnitBase";
            shellUnitBase.Size = new Size(96, 194);
            shellUnitBase.TabIndex = 14;
            // 
            // shellUnit3
            // 
            shellUnit3.BackColor = Color.Lime;
            shellUnit3.Location = new Point(0, 127);
            shellUnit3.Name = "shellUnit3";
            shellUnit3.Size = new Size(96, 15);
            shellUnit3.TabIndex = 2;
            // 
            // shellUnit2
            // 
            shellUnit2.BackColor = Color.Lime;
            shellUnit2.Location = new Point(0, 89);
            shellUnit2.Name = "shellUnit2";
            shellUnit2.Size = new Size(96, 15);
            shellUnit2.TabIndex = 1;
            // 
            // shellUnit1
            // 
            shellUnit1.BackColor = Color.Lime;
            shellUnit1.Location = new Point(0, 51);
            shellUnit1.Name = "shellUnit1";
            shellUnit1.Size = new Size(96, 15);
            shellUnit1.TabIndex = 0;
            // 
            // numericUpDownFPS
            // 
            numericUpDownFPS.Location = new Point(36, 188);
            numericUpDownFPS.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownFPS.Name = "numericUpDownFPS";
            numericUpDownFPS.Size = new Size(52, 23);
            numericUpDownFPS.TabIndex = 15;
            numericUpDownFPS.Value = new decimal(new int[] { 70, 0, 0, 0 });
            numericUpDownFPS.ValueChanged += numericUpDownFPS_ValueChanged;
            // 
            // labelFPS
            // 
            labelFPS.AutoSize = true;
            labelFPS.Location = new Point(4, 192);
            labelFPS.Name = "labelFPS";
            labelFPS.Size = new Size(26, 15);
            labelFPS.TabIndex = 16;
            labelFPS.Text = "FPS";
            // 
            // checkBoxTopMost
            // 
            checkBoxTopMost.AutoSize = true;
            checkBoxTopMost.Location = new Point(2, 0);
            checkBoxTopMost.Name = "checkBoxTopMost";
            checkBoxTopMost.Size = new Size(95, 19);
            checkBoxTopMost.TabIndex = 17;
            checkBoxTopMost.Text = "最前面に表示";
            checkBoxTopMost.UseVisualStyleBackColor = true;
            checkBoxTopMost.CheckedChanged += checkBoxTopMost_CheckedChanged;
            // 
            // buttonIPPluginDecrement
            // 
            buttonIPPluginDecrement.Location = new Point(6, 6);
            buttonIPPluginDecrement.Name = "buttonIPPluginDecrement";
            buttonIPPluginDecrement.Size = new Size(75, 23);
            buttonIPPluginDecrement.TabIndex = 2;
            buttonIPPluginDecrement.Text = "<<";
            buttonIPPluginDecrement.UseVisualStyleBackColor = true;
            buttonIPPluginDecrement.Click += buttonIPPluginDecrement_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 450);
            Controls.Add(checkBoxTopMost);
            Controls.Add(labelFPS);
            Controls.Add(numericUpDownFPS);
            Controls.Add(tabControlOption);
            Controls.Add(shellUnitBase);
            Controls.Add(buttonCharacterRecognition);
            Controls.Add(buttonTImerStartStop);
            Controls.Add(panelViewer);
            Controls.Add(tabControlMain);
            Controls.Add(pictureBoxViewer);
            Name = "MainForm";
            Text = "p-GART";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxViewer).EndInit();
            tabControlOption.ResumeLayout(false);
            tabPageSettings.ResumeLayout(false);
            tabPageSettings.PerformLayout();
            tabPageObservation.ResumeLayout(false);
            tabPageObservation.PerformLayout();
            tabControlMain.ResumeLayout(false);
            tabPageVideoCapture.ResumeLayout(false);
            tabPageVideoCapture.PerformLayout();
            tabPagetabPageImageRecognition.ResumeLayout(false);
            tabPageCharacterRecognition.ResumeLayout(false);
            tabPageCharacterRecognition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSheight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSy).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSwidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCRSx).EndInit();
            tabPageImageProcessing.ResumeLayout(false);
            panelViewer.ResumeLayout(false);
            panelViewer.PerformLayout();
            shellUnitBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownFPS).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxViewer;
        private ComboBox comboBoxPluginVideoCapture;
        private TabControl tabControlOption;
        private Label labelReFPS;
        private TabControl tabControlMain;
        private TabPage tabPageCharacterRecognition;
        private Button buttonAddCharacterRecognition;
        private TabPage tabPageImageProcessing;
        private Button buttonDeleteCharacterRecognition;
        private ListBox listBoxCharacterRecognitionSettings;
        private NumericUpDown numericUpDownCRSheight;
        private NumericUpDown numericUpDownCRSy;
        private NumericUpDown numericUpDownCRSwidth;
        private NumericUpDown numericUpDownCRSx;
        private Label labelCRSheight;
        private Label labelCRSwidth;
        private Label labelCRSy;
        private Label labelCRSx;
        private Button buttonCopyCharacterRecognition;
        private TabControl tabControlPluginCharacterRecognition;
        private ComboBox comboBoxPluginCharacterRecognition;
        private TabControl tabControlPluginImageProcessing;
        private Panel panelViewer;
        private RadioButton radioButtonDefault;
        private RadioButton radioButtonImageProcessing;
        private RadioButton radioButtonImageRecognition;
        private TabPage tabPagetabPageImageRecognition;
        private ComboBox comboBoxPluginImageRecognition;
        private TabControl tabControlPluginImageRecognition;
        private TabPage tabPageVideoCapture;
        private TabControl tabControlPluginVideoCapture;
        private Label labelSelectPluginCharacterRecognition;
        private TabPage tabPageObservation;
        private TextBox textBoxCharacterRecognitionKeyName;
        private Label labelCharacterRecognitionKeyName;
        private Button buttonOverWriteCharacterRecognition;
        private Label labelMatchRateImageRecognition;
        private Label labelRectangleImageRecognition_X;
        private Button buttonTImerStartStop;
        private Button buttonCharacterRecognition;
        private Label labelRectangleImageRecognition_Height;
        private Label labelRectangleImageRecognition_Width;
        private Label labelRectangleImageRecognition_Y;
        private Label labelViewer;
        private Label labelCaptureNo;
        private ComboBox comboBoxCaptureNo;
        private Panel shellUnitBase;
        private Panel shellUnit3;
        private Panel shellUnit2;
        private Panel shellUnit1;
        private TabPage tabPageSettings;
        private Button buttonScreenShotMatch;
        private Button buttonScreenShot;
        private NumericUpDown numericUpDownFPS;
        private Label labelFPS;
        private CheckBox checkBoxTopMost;
        private Button buttonSettingDownload;
        private TextBox textBoxSettingDownloadID;
        private Label labelDLID;
        private Label labelSettingsList;
        private ComboBox comboBoxSettingsList;
        private Label labelSettingsSave;
        private TextBox textBoxSettingsName;
        private Button buttonSettingsSave;
        private Button buttonReload;
        private Button buttonIPPluginIncrement;
        private Button buttonIPPluginDecrement;
    }
}
