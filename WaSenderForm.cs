
using FluentValidation.Results;
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.Models;
using WASender.Validators;


namespace WASender
{
    public partial class WaSenderForm : MaterialForm
    {
        MaterialSkin.MaterialSkinManager materialSkinManager;
        WASenderSingleTransModel wASenderSingleTransModel;
        WASenderGroupTransModel wASenderGroupTransModel;
        GeneralSettingsModel generalSettingsModel;
        List<ButtonsModel> buttonsModelList1;
        List<ButtonsModel> buttonsModelList2;
        List<ButtonsModel> buttonsModelList3;
        List<ButtonsModel> buttonsModelList4;
        List<ButtonsModel> buttonsModelList5;

        WebBrowser _browser1, _browser2, _browser3, _browser4, _browser5;
        public WaSenderForm()
        {

            InitializeComponent();
            groupBox11.Hide();
            Utils.SetColorScheme(materialSkinManager, this);
            buttonsModelList1 = new List<ButtonsModel>();
            buttonsModelList2 = new List<ButtonsModel>();
            buttonsModelList3 = new List<ButtonsModel>();
            buttonsModelList4 = new List<ButtonsModel>();
            buttonsModelList5 = new List<ButtonsModel>();

            _browser1 = webBrowser1;
            _browser2 = webBrowser2;
            _browser3 = webBrowser3;
            _browser4 = webBrowser4;
            _browser5 = webBrowser5;
            webBrowser1.DocumentText = Storage.DocumentHtmlString;
            webBrowser2.DocumentText = Storage.DocumentHtmlString;
            webBrowser3.DocumentText = Storage.DocumentHtmlString;
            webBrowser4.DocumentText = Storage.DocumentHtmlString;
            webBrowser5.DocumentText = Storage.DocumentHtmlString;
            this._browser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser1_DocumentCompleted);
            this._browser2.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser2_DocumentCompleted);
            this._browser3.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser3_DocumentCompleted);
            this._browser4.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser4_DocumentCompleted);
            this._browser5.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser5_DocumentCompleted);

        }




        private void browser1_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this._browser1.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown1);
        }

        private void browser2_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this._browser2.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown2);
        }
        private void browser3_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this._browser3.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown3);
        }
        private void browser4_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this._browser4.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown4);
        }
        private void browser5_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this._browser5.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown5);
        }

        private void Body_MouseDown1(Object sender, HtmlElementEventArgs e)
        {
            switch (e.MouseButtonsPressed)
            {
                case MouseButtons.Left:
                    HtmlElement element = this._browser1.Document.GetElementFromPoint(e.ClientMousePosition);
                    var btnId = element.GetAttribute("id");
                    if (btnId != "")
                    {
                        try
                        {
                            ButtonsModel b = buttonsModelList1.Where(x => x.id == btnId).FirstOrDefault();
                            b.editMode = true;
                            AddButton addButton = new AddButton(this, b);
                            addButton.ShowDialog();
                        }
                        catch (Exception ex)
                        {

                        }

                    }


                    break;
            }
        }

        private void Body_MouseDown2(Object sender, HtmlElementEventArgs e)
        {
            switch (e.MouseButtonsPressed)
            {
                case MouseButtons.Left:
                    HtmlElement element = this._browser2.Document.GetElementFromPoint(e.ClientMousePosition);
                    var ss = element.GetAttribute("id");

                    break;
            }
        }

        private void Body_MouseDown3(Object sender, HtmlElementEventArgs e)
        {
            switch (e.MouseButtonsPressed)
            {
                case MouseButtons.Left:
                    HtmlElement element = this._browser3.Document.GetElementFromPoint(e.ClientMousePosition);
                    var ss = element.GetAttribute("id");

                    break;
            }
        }
        private void Body_MouseDown4(Object sender, HtmlElementEventArgs e)
        {
            switch (e.MouseButtonsPressed)
            {
                case MouseButtons.Left:
                    HtmlElement element = this._browser4.Document.GetElementFromPoint(e.ClientMousePosition);
                    var ss = element.GetAttribute("id");

                    break;
            }
        }

        private void Body_MouseDown5(Object sender, HtmlElementEventArgs e)
        {
            switch (e.MouseButtonsPressed)
            {
                case MouseButtons.Left:
                    HtmlElement element = this._browser5.Document.GetElementFromPoint(e.ClientMousePosition);
                    var ss = element.GetAttribute("id");

                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            init();
            setTooltips();
           // CHeckForActivation();

            if (Config.IsDemoMode == true)
            {
                //this.Text = "Wa Sender 1.0 (Trial Version)";
            }
            else
            {
                //linkLabel1.Hide();
            }
        }

        private void CHeckForActivation()
        {
            if (!Config.IsProductActive())
            {
                Activate activate = new Activate(this);
                this.Hide();
                activate.ShowDialog();
            }
        }

        private void setTooltips()
        {
            Utils.setTooltiop(btnDownloadSample, Strings.DownloadSample);
            Utils.setTooltiop(btnUploadExcel, Strings.UploadSampleExcel);

            Utils.setTooltiop(btnDownloadSampleGroup, Strings.DownloadSample);
            Utils.setTooltiop(btnUploadExcelGroup, Strings.UploadSampleExcel);
        }

        private void init()
        {
            materialCheckbox1.Checked = true;
            materialCheckbox2.Checked = true;

            materialCheckbox4.Checked = true;
            materialCheckbox3.Checked = true;
            //materialCheckbox6.Checked = true;
            defaultColorSchime();
            lblSection.Text = Strings.ContectSender;
            chkDarkMode.Visible = false;

            SetLanguagesDropdown();
            getSelectedLanguage();

            comboBox1.SelectedText = generalSettingsModel.selectedLanguage;

            initLanguage();
        }

        public void CountryCOdeAdded(string code)
        {
            foreach (DataGridViewRow item in gridTargets.Rows)
            {
                if (item.Cells[0].Value != "" && item.Cells[0].Value != null)
                {
                    item.Cells[0].Value = code + item.Cells[0].Value;
                }
            }
        }
        private void SetLanguagesDropdown()
        {
            DirectoryInfo d = new DirectoryInfo("languages");
            FileInfo[] Files = d.GetFiles("*.json"); //Getting Text files
            string str = "";

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            foreach (FileInfo file in Files)
            {
                // str = str + ", " + file.Name;

                try
                {
                    comboBox1.Items.Add(new { Text = file.Name.Split('.')[0], Value = file.Name });
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void getSelectedLanguage()
        {
            string settingPath = Config.GetGeneralSettingsFilePath();

            if (!File.Exists(settingPath))
            {
                File.Create(settingPath).Close();
            }
            generalSettingsModel = new GeneralSettingsModel();
            generalSettingsModel.selectedLanguage = "English";
            try
            {
                string GeneralSettingJson = "";
                using (StreamReader r = new StreamReader(settingPath))
                {
                    GeneralSettingJson = r.ReadToEnd();
                }
                var dict = JsonConvert.DeserializeObject<GeneralSettingsModel>(GeneralSettingJson);
                if (dict != null)
                {
                    generalSettingsModel = dict;
                }
                Strings.selectedLanguage = generalSettingsModel.selectedLanguage;
            }
            catch (Exception ex)
            {

            }

        }

        private void initLanguage()
        {
            this.Text = Strings.AppName;
            tabMain.TabPages[0].Text = Strings.ContectSender;
            tabMain.TabPages[1].Text = Strings.GroupSender;
            tabMain.TabPages[2].Text = Strings.Tools;
            materialLabel1.Text = Strings.Target;
            materialLabel10.Text = Strings.Target;
            btnUploadExcel.Text = Strings.UploadSampleExcel;
            btnDownloadSample.Text = Strings.DownloadSample;
            materialLabel2.Text = Strings.Messages;
            materialTabControl2.TabPages[0].Text = Strings.MessageOne;
            materialTabControl2.TabPages[1].Text = Strings.MessageTwo;
            materialTabControl2.TabPages[2].Text = Strings.MessageThree;
            materialTabControl2.TabPages[3].Text = Strings.MessageFour;
            materialTabControl2.TabPages[4].Text = Strings.MessageFive;

            groupBox1.Text = Strings.Attachments;
            groupBox2.Text = Strings.Attachments;
            groupBox3.Text = Strings.Attachments;
            groupBox4.Text = Strings.Attachments;
            groupBox5.Text = Strings.Attachments;

            btnAddFileOne.Text = Strings.Addfile;
            btnAddFileTwo.Text = Strings.Addfile;
            btnAddFileThree.Text = Strings.Addfile;
            btnAddFileFour.Text = Strings.Addfile;
            btnAddFileFive.Text = Strings.Addfile;

            txtMsgOne.Hint = Strings.Yourfirstmessage;
            txtMsgTwo.Hint = Strings.YourSecondmessage;
            txtMsgThree.Hint = Strings.YourThirdmessage;
            txtMsgFour.Hint = Strings.YourFourthmessage;
            txtMsgFive.Hint = Strings.YourFifthmessage;


            txtMsgOneGroup.Hint = Strings.Yourfirstmessage;
            txtMsgTwoGroup.Hint = Strings.YourSecondmessage;
            txtMsgTHreeGroup.Hint = Strings.YourThirdmessage;
            txtMsgFourGroup.Hint = Strings.YourFourthmessage;
            txtMsgFiveGroup.Hint = Strings.YourFifthmessage;

            De.Text = Strings.DelaySettings;
            materialLabel3.Text = Strings.Wait;
            materialLabel9.Text = Strings.Wait;
            materialLabel5.Text = Strings.secondsafterevery;
            materialLabel4.Text = Strings.to;
            materialLabel8.Text = Strings.to;
            materialLabel6.Text = Strings.Messages;
            materialLabel7.Text = Strings.secondsbeforeeverymessage;

            materialButton1.Text = Strings.Clear;
            btnStart.Text = Strings.StartCampaign;

            contextMenuStrip1.Items[0].Text = Strings.AddKeyMarkers;
            contextMenuStrip1.Items[1].Text = Strings.RandomNumber;


            btnUploadExcelGroup.Text = Strings.UploadSampleExcel;
            materialLabel1.Text = Strings.Target;
            gridTargetsGroup.Columns[0].HeaderText = Strings.GroupNames;

            gridTargets.Columns[0].HeaderText = Strings.Number;
            gridTargets.Columns[1].HeaderText = Strings.Name;

            btnDownloadSampleGroup.Text = Strings.DownloadSample;
            materialLabel18.Text = Strings.Message;
            materialLabel2.Text = Strings.Message;

            materialTabControl1.TabPages[0].Text = Strings.MessageOne;
            materialTabControl1.TabPages[1].Text = Strings.MessageTwo;
            materialTabControl1.TabPages[2].Text = Strings.MessageThree;
            materialTabControl1.TabPages[3].Text = Strings.MessageFour;
            materialTabControl1.TabPages[4].Text = Strings.MessageFive;

            groupBox6.Text = Strings.Attachments;
            groupBox7.Text = Strings.Attachments;
            groupBox8.Text = Strings.Attachments;
            groupBox9.Text = Strings.Attachments;
            groupBox10.Text = Strings.Attachments;

            materialButton4.Text = Strings.Addfile;
            materialButton5.Text = Strings.Addfile;
            materialButton6.Text = Strings.Addfile;
            materialButton7.Text = Strings.Addfile;
            materialButton8.Text = Strings.Addfile;

            materialLabel17.Text = Strings.DelaySettings;
            materialLabel16.Text = Strings.Wait;
            materialLabel12.Text = Strings.Wait;

            materialLabel15.Text = Strings.to;
            materialLabel11.Text = Strings.to;
            materialLabel14.Text = Strings.secondsafterevery;
            materialLabel13.Text = Strings.Messages;
            materialLabel10.Text = Strings.secondsbeforeeverymessage;
            materialButton2.Text = Strings.Clear;
            btnStartGroup.Text = Strings.StartCampaign;

            //materialLabel20.Text = Strings.GrabChatList;
            //materialLabel21.Text = Strings.ClickbellowButton;
            //materialLabel23.Text = Strings.ScanQRCode;
            //materialLabel24.Text = Strings.WaitfortheExcel;

            //materialButton3.Text = Strings.GrabNow;
            materialButton9.Text = Strings.GrabNow;
            materialButton10.Text = Strings.GrabNow;

            materialButton12.Text = Strings.StartNow;
            materialButton11.Text = Strings.StartNow;
            materialButton15.Text = Strings.StartNow;

            materialLabel28.Text = Strings.GrabGroupMembers;
            materialLabel27.Text = Strings.ClickbellowButtonScanQRCode;
            materialLabel26.Text = Strings.OpenAnyGroup;
            materialLabel25.Text = Strings.WaitfortheExcel;
            materialLabel32.Text = Strings.GrabWhatsAppGroupLinksfromwebpage;
            materialLabel31.Text = Strings.ClickbellowButtonScanQRCode;
            materialLabel30.Text = Strings.OpenAnywebpagewheregivengrouplinks;
            materialLabel29.Text = Strings.ThenClickonSTARTButton;
            materialLabel35.Text = Strings.AutoGroupJoiner;
            materialLabel34.Text = Strings.AddUploadGroupLinks;
            materialLabel33.Text = Strings.ScanWAQRCode;
            materialLabel22.Text = Strings.ThenClickonSTARTButton;
            materialLabel39.Text = Strings.WhatsAppAutoResponderBot;
            materialLabel38.Text = Strings.SetRules;
            materialLabel37.Text = Strings.AddReplyMessages;
            materialLabel36.Text = Strings.ThenClickonSTARTButton;

            label5.Text = Strings.WhatsAppNumberFilter;
            label4.Text = Strings.AddUploadNumbers;
            label9.Text = Strings.ContactListGrabber;
            label7.Text = Strings.HitGrabNowButton;
            label3.Text = Strings.ScanWAQRCode;
            label2.Text = Strings.ThenClickonSTARTButton;
            label8.Text = Strings.ClickbellowButtonScanQRCode;
            label6.Text = Strings.WaitfortheExcel;

            label13.Text = Strings.GrabActiveGroupMembers;

            label12.Text = Strings.ClickbellowButtonScanQRCode;
            label11.Text = Strings.OpenAnyGroup;
            label10.Text = Strings.WaitfortheExcel;
            materialButton17.Text = Strings.GrabNow;

            materialButton18.Text = Strings.GrabNow;
            materialButton16.Text = Strings.GrabNow;

            label17.Text = Strings.GoogleMapDataEExtractor;
            label16.Text = Strings.One + Strings.OpenBrowser;
            // Search Your QUery on G Map
            label15.Text = Strings.Two + Strings.SearchYourQUeryonGMap;
            label14.Text = Strings.Three + Strings.HitStart;

            addCountryCodeToolStripMenuItem.Text = Strings.AddCountryCode;
            importNumbersToolStripMenuItem.Text = Strings.CopyPasteNumber;

            contextMenuStrip1.Items[2].Text = Strings.AddButtons;
            groupBox11.Text = Strings.Buttons;
            //label18.Text = Strings.ButtonMessage;
            materialButton19.Text = Strings.AddButton;



        }

        private void defaultColorSchime()
        {
            lblSection.BackColor = System.Drawing.ColorTranslator.FromOle((int)Primary.Green700);
            lblSection.ForeColor = System.Drawing.Color.White;
            chkDarkMode.BackColor = System.Drawing.ColorTranslator.FromOle((int)Primary.Green700);
            chkDarkMode.ForeColor = System.Drawing.Color.White;
            label1.BackColor = System.Drawing.ColorTranslator.FromOle((int)Primary.Green700);
            label1.ForeColor = System.Drawing.Color.White;
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedIndex == 0)
            {
                lblSection.Text = Strings.ContectSender;
            }
            if (tabMain.SelectedIndex == 1)
            {
                lblSection.Text = Strings.GroupSender;
            }
            if (tabMain.SelectedIndex == 2)
            {
                lblSection.Text = Strings.Tools;
            }

        }

        private void chkDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDarkMode.Checked)
            {
                enableDarkMode();
            }
            else
            {
                disableDarkMode();
            }
            defaultColorSchime();
        }

        private void disableDarkMode()
        {
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
        }

        private void enableDarkMode()
        {
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
        }

        private void btnDownloadSample_Click(object sender, EventArgs e)
        {
            savesampleExceldialog.FileName = "SingleSenderTemplate.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy("templets/SingleSenderTemplate.xlsx", savesampleExceldialog.FileName);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }


        public void ReturnPasteNumber(List<string> numbers)
        {
            var globalCounter = gridTargets.Rows.Count - 1;
            for (int i = 0; i < numbers.Count(); i++)
            {
                try
                {
                    gridTargets.Rows.Add();
                    string MobileNumber = numbers[i];
                    MobileNumber = MobileNumber.Replace("+", "");
                    MobileNumber = MobileNumber.Replace(" ", "");
                    MobileNumber = MobileNumber.Replace(" ", "");
                    gridTargets.Rows[globalCounter].Cells[0].Value = MobileNumber;
                    globalCounter++;
                }
                catch (Exception ec)
                {
                    
                }
            }
        }
        private void btnUploadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Strings.SelectExcel;
            openFileDialog.DefaultExt = "xlsx";
            openFileDialog.Filter = "Excel Files|*.xlsx;";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                FileInfo fi = new FileInfo(file);
                if ((fi.Extension != ".xlsx"))
                {
                    Utils.showAlert(Strings.PleaseselectExcelfilesformatonly, Alerts.Alert.enmType.Error);
                    return;
                }

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(fi))
                {
                    try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        var globalCounter = gridTargets.Rows.Count - 1;
                        var ColumnsCOunt = worksheet.Dimension.Columns;
                        if (ColumnsCOunt > 2)
                        {
                            for (int i = 3; i <= ColumnsCOunt; i++)
                            {
                                try
                                {
                                    string Header = worksheet.Cells[1, i].Value.ToString();
                                    gridTargets.Columns.Add("NewColumn" + i, Header);
                                }
                                catch (Exception ex)
                                {
                                    string exs = "";
                                }
                            }
                        }


                        for (int i = 1; i < worksheet.Dimension.Rows; i++)
                        {

                            try
                            {
                                gridTargets.Rows.Add();

                                string MobileNumber = worksheet.Cells[i + 1, 1].Value.ToString();
                                try
                                {
                                    MobileNumber = MobileNumber.Replace("+", "");
                                    MobileNumber = MobileNumber.Replace(" ", "");
                                    MobileNumber = MobileNumber.Replace(" ", "");
                                    Int64 temp = Convert.ToInt64(MobileNumber);
                                }
                                catch (Exception ex)
                                {

                                }


                                string name = "";
                                try
                                {
                                    name = worksheet.Cells[i + 1, 2].Value.ToString();
                                }
                                catch (Exception ex)
                                {

                                }

                                gridTargets.Rows[globalCounter].Cells[0].Value = MobileNumber;
                                gridTargets.Rows[globalCounter].Cells[1].Value = name;

                                try
                                {
                                    if (ColumnsCOunt > 1)
                                    {
                                        for (int j = 2; j <= ColumnsCOunt; j++)
                                        {
                                            try
                                            {
                                                string CelValue = worksheet.Cells[i + 1, j].Value.ToString();
                                                gridTargets.Rows[globalCounter].Cells[j - 1].Value = CelValue;
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                string ss = "";
                            }

                            globalCounter++;

                        }
                    }
                    catch (Exception ex)
                    {
                        Utils.showAlert(ex.Message, Alerts.Alert.enmType.Error);
                    }
                }
            }
        }

        private void setCounter()
        {
            lblCount.Text = (gridTargets.Rows.Count - 1).ToString();
        }

        private void setCounterGroup()
        {
            lblCountGroup.Text = (gridTargetsGroup.Rows.Count - 1).ToString();
        }

        private void gridTargets_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setCounter();
        }

        private void gridTargets_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            setCounter();
        }

        private void btnAddFileOne_Click_1(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewOne);
        }

        private void btnAddFileTwo_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewTwo);
        }

        private void btnAddFileThree_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewThree);
        }

        private void btnAddFileFour_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewFour);
        }

        private void btnAddFileFive_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewFive);
        }

        private void lstViewOne_KeyDown(object sender, KeyEventArgs e)
        {
            Utils.removeListViewItem(e, lstViewOne);
        }

        private void lstViewTwo_KeyDown(object sender, KeyEventArgs e)
        {
            Utils.removeListViewItem(e, lstViewTwo);
        }

        private void lstViewThree_KeyDown(object sender, KeyEventArgs e)
        {
            Utils.removeListViewItem(e, lstViewThree);
        }

        private void lstViewFour_KeyDown(object sender, KeyEventArgs e)
        {
            Utils.removeListViewItem(e, lstViewFour);
        }

        private void lstViewFive_KeyDown(object sender, KeyEventArgs e)
        {
            Utils.removeListViewItem(e, lstViewFive);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ValidateControls();
        }


        private void ValidateControlsGroup()
        {
            wASenderGroupTransModel = new WASenderGroupTransModel();
            wASenderGroupTransModel.groupList = new List<GroupModel>();
            GroupModel group = new GroupModel();

            for (int i = 0; i < gridTargetsGroup.Rows.Count; i++)
            {
                if (!(gridTargetsGroup.Rows[i].Cells[0].Value == null))
                {
                    group = new GroupModel
                    {
                        Name = gridTargetsGroup.Rows[i].Cells[0].Value == null ? "" : gridTargetsGroup.Rows[i].Cells[0].Value.ToString(),
                        GroupId = gridTargetsGroup.Rows[i].Cells[1].Value == null ? "" : gridTargetsGroup.Rows[i].Cells[1].Value.ToString(),
                        sendStatusModel = new SendStatusModel { isDone = false }
                    };

                    group.validationFailures = new GroupModelValidator().Validate(group).Errors;
                    wASenderGroupTransModel.groupList.Add(group);
                }
            }


            wASenderGroupTransModel.settings = new SingleSettingModel();
            wASenderGroupTransModel.settings.delayAfterMessages = Convert.ToInt32(txtdelayAfterMessagesGroup.Text);
            wASenderGroupTransModel.settings.delayAfterMessagesFrom = Convert.ToInt32(txtdelayAfterMessagesFromGroup.Text);
            wASenderGroupTransModel.settings.delayAfterMessagesTo = Convert.ToInt32(txtdelayAfterMessagesToGroup.Text);
            wASenderGroupTransModel.settings.delayAfterEveryMessageFrom = Convert.ToInt32(txtdelayAfterEveryMessageFromGroup.Text);
            wASenderGroupTransModel.settings.delayAfterEveryMessageTo = Convert.ToInt32(txtdelayAfterEveryMessageToGroup.Text);

            wASenderGroupTransModel.settings.validationFailures = new SingleSettingModelValidator().Validate(wASenderGroupTransModel.settings).Errors;


            wASenderGroupTransModel.messages = new List<MesageModel>();
            wASenderGroupTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgOneGroup, lstViewOneGroup));
            wASenderGroupTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgTwoGroup, lstViewTwoGroup));
            wASenderGroupTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgTHreeGroup, lstViewThreeGroup));
            wASenderGroupTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgFourGroup, lstViewFourGroup));
            wASenderGroupTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgFiveGroup, lstViewFiveGroup));
            wASenderGroupTransModel.validationFailures = new WASenderGroupTransModelValidator().Validate(wASenderGroupTransModel).Errors;

            if (showValidationErrorIfAnyGroup())
            {
                DialogResultModel promptValue = Utils.ShowDialog(Strings.PleaseEnterCampaignName, Strings.CampaignName);
                if (promptValue.CampaignName == "")
                { promptValue.CampaignName = Strings.UntitledCampaign; }
                wASenderGroupTransModel.CampaignName = promptValue.CampaignName;
                wASenderGroupTransModel.MessageSendingType = promptValue.MessageType;
                RunGroup run = new RunGroup(wASenderGroupTransModel, this);
                this.Hide();
                run.ShowDialog();
            }
        }

        private void ValidateControls()
        {
            wASenderSingleTransModel = new WASenderSingleTransModel();
            wASenderSingleTransModel.contactList = new List<ContactModel>();
            ContactModel contact;

            for (int i = 0; i < gridTargets.Rows.Count; i++)
            {
                if (!(gridTargets.Rows[i].Cells[0].Value == null && gridTargets.Rows[i].Cells[1].Value == null))
                {
                    contact = new ContactModel
                    {
                        number = gridTargets.Rows[i].Cells[0].Value == null ? "" : gridTargets.Rows[i].Cells[0].Value.ToString(),
                        name = gridTargets.Rows[i].Cells[1].Value == null ? "" : gridTargets.Rows[i].Cells[1].Value.ToString(),
                        sendStatusModel = new SendStatusModel { isDone = false }
                    };

                    if (gridTargets.ColumnCount > 1)
                    {
                        contact.parameterModelList = new List<ParameterModel>();
                        ParameterModel parameterModel;
                        for (int j = 2; j <= gridTargets.ColumnCount; j++)
                        {
                            try
                            {
                                string cellValue = gridTargets.Rows[i].Cells[j - 1].Value.ToString();
                                string cellHeader = gridTargets.Columns[j - 1].HeaderText;
                                parameterModel = new ParameterModel();
                                parameterModel.ParameterName = cellHeader;
                                parameterModel.ParameterValue = cellValue;
                                contact.parameterModelList.Add(parameterModel);
                            }
                            catch (Exception ex)
                            {


                            }
                        }
                    }


                    contact.validationFailures = new ContactModelValidator().Validate(contact).Errors;
                    wASenderSingleTransModel.contactList.Add(contact);
                }
            }

            wASenderSingleTransModel.settings = new SingleSettingModel();
            wASenderSingleTransModel.settings.delayAfterMessages = Convert.ToInt32(txtdelayAfterMessages.Text);
            wASenderSingleTransModel.settings.delayAfterMessagesFrom = Convert.ToInt32(txtdelayAfterMessagesFrom.Text);
            wASenderSingleTransModel.settings.delayAfterMessagesTo = Convert.ToInt32(txtdelayAfterMessagesTo.Text);
            wASenderSingleTransModel.settings.delayAfterEveryMessageFrom = Convert.ToInt32(txtdelayAfterEveryMessageFrom.Text);
            wASenderSingleTransModel.settings.delayAfterEveryMessageTo = Convert.ToInt32(txtdelayAfterEveryMessageTo.Text);

            wASenderSingleTransModel.settings.validationFailures = new SingleSettingModelValidator().Validate(wASenderSingleTransModel.settings).Errors;

            wASenderSingleTransModel.messages = new List<MesageModel>();
            wASenderSingleTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgOne, lstViewOne, buttonsModelList1));
            wASenderSingleTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgTwo, lstViewTwo, buttonsModelList2));
            wASenderSingleTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgThree, lstViewThree, buttonsModelList3));
            wASenderSingleTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgFour, lstViewFour, buttonsModelList4));
            wASenderSingleTransModel.messages.Add(CheckAndSendtoMessageModel(txtMsgFive, lstViewFive, buttonsModelList5));
            wASenderSingleTransModel.validationFailures = new WASenderSingleTransModelValidator().Validate(wASenderSingleTransModel).Errors;

            if (showValidationErrorIfAny())
            {
                DialogResultModel promptValue = Utils.ShowDialog(Strings.PleaseEnterCampaignName, Strings.CampaignName);
                if (promptValue.CampaignName == "")
                { promptValue.CampaignName = Strings.UntitledCampaign; }
                wASenderSingleTransModel.CampaignName = promptValue.CampaignName;
                wASenderSingleTransModel.MessageSendingType = promptValue.MessageType;
                RunSingle run = new RunSingle(wASenderSingleTransModel, this);
                this.Hide();
                run.ShowDialog();
            }
        }

        public void formReturn(bool success)
        {
            this.Show();
        }
        public void gmapDataReturn(List<GMapModel> gmapModel)
        {
            this.Show();
            gridTargets.Rows.Add();
            for (int i = 0; i < gmapModel.Where(x => x.mobilenumber != "" && x.mobilenumber != null).Count(); i++)
            {
                gridTargets.Rows.Add();
            }

            int j = 0;
            foreach (var item in gmapModel.Where(x => x.mobilenumber != "" && x.mobilenumber != null))
            {
                try
                {
                    string MobileNumber = item.mobilenumber;
                    MobileNumber = MobileNumber.Replace("+", "");
                    MobileNumber = MobileNumber.Replace(" ", "");
                    MobileNumber = MobileNumber.Replace(" ", "");
                    gridTargets.Rows[j].Cells[0].Value = MobileNumber;
                    j++;
                }
                catch (Exception)
                {

                }
            }
            //for (int i = 0; i < gmapModel.Where(x => x.mobilenumber != "" && x.mobilenumber != null).Count(); i++)
            //{
            //    try
            //    {
            //        string MobileNumber = gmapModel[i].mobilenumber;
            //        MobileNumber = MobileNumber.Replace("+", "");
            //        MobileNumber = MobileNumber.Replace(" ", "");
            //        MobileNumber = MobileNumber.Replace(" ", "");
            //        //Int64 temp = Convert.ToInt64(MobileNumber);
            //        gridTargets.Rows[i].Cells[0].Value = MobileNumber;
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
            tabMain.SelectedIndex = 0;
        }

        private bool showValidationErrorIfAnyGroup()
        {
            bool validationFail = true;
            if (CheckValidationMessage(wASenderGroupTransModel.validationFailures))
            {
                if (CheckValidationMessage(wASenderGroupTransModel.settings.validationFailures))
                {
                    for (int i = 0; i < wASenderGroupTransModel.groupList.Count(); i++)
                    {
                        if (CheckValidationMessage(wASenderGroupTransModel.groupList[i].validationFailures, Strings.RowNo + " - " + Convert.ToString(i + 1)))
                        {
                            string ss = "";
                        }
                        else
                        {
                            i = wASenderGroupTransModel.groupList.Count;
                            validationFail = false;
                        }
                    }
                }
                else
                {
                    validationFail = false;
                }
            }
            else
            {
                validationFail = false;
            }
            return validationFail;
        }

        private bool showValidationErrorIfAny()
        {
            bool validationFail = true;
            if (CheckValidationMessage(wASenderSingleTransModel.validationFailures))
            {
                if (CheckValidationMessage(wASenderSingleTransModel.settings.validationFailures))
                {
                    for (int i = 0; i < wASenderSingleTransModel.contactList.Count(); i++)
                    {
                        if (CheckValidationMessage(wASenderSingleTransModel.contactList[i].validationFailures, Strings.RowNo + "- " + Convert.ToString(i + 1)))
                        {
                            string ss = "";
                        }
                        else
                        {
                            i = wASenderSingleTransModel.contactList.Count;
                            validationFail = false;
                        }
                    }
                }
                else
                {
                    validationFail = false;
                }
            }
            else
            {
                validationFail = false;
            }
            return validationFail;
        }

        private bool CheckValidationMessage(IList<ValidationFailure> validationFailures, string AdditionalMessage = "")
        {
            string Messages = "";
            if (validationFailures != null && validationFailures.Count() > 0)
            {
                foreach (var item in validationFailures)
                {
                    Messages = Messages + item.ErrorMessage + "\n\n";
                }
            }
            if (Messages == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(AdditionalMessage + " " + Messages, Strings.Errors, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private MesageModel CheckAndSendtoMessageModel(MaterialMultiLineTextBox2 txtMsg, MaterialListView list, List<ButtonsModel> _buttonsModel = null)
        {
            MesageModel mesageModel;
            if (txtMsg.Text != null && txtMsg.Text != "")
            {
                mesageModel = new MesageModel();
                mesageModel.longMessage = txtMsg.Text;

                mesageModel.files = new List<FilesModel>();
                foreach (ListViewItem item in list.Items)
                {
                    mesageModel.files.Add(new FilesModel { filePath = item.Text });
                }
                mesageModel.buttons = _buttonsModel;

                return mesageModel;
            }
            else
            {
                return null;
            }
        }

        private void btnGroupDownloadExcel_Click(object sender, EventArgs e)
        {
            savesampleExceldialog.FileName = "GroupSenderTemplate.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy("GroupSenderTemplate.xlsx", savesampleExceldialog.FileName);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }

        private void btnUploadExcelGroup_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Strings.SelectExcel;
            openFileDialog.DefaultExt = "xlsx";
            openFileDialog.Filter = "Excel Files|*.xlsx;";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                FileInfo fi = new FileInfo(file);
                if ((fi.Extension != ".xlsx"))
                {
                    Utils.showAlert(Strings.PleaseselectExcelfilesformatonly, Alerts.Alert.enmType.Error);
                    return;
                }

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(fi))
                {
                    try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        var globalCounter = gridTargetsGroup.Rows.Count - 1;
                        for (int i = 1; i < worksheet.Dimension.Rows; i++)
                        {
                            if (Config.IsDemoMode == true && globalCounter > 5)
                            {
                                Utils.showAlert("You can import only 5 Groups in trial Version", Alerts.Alert.enmType.Error);
                                return;
                            }
                            gridTargetsGroup.Rows.Add();
                            gridTargetsGroup.Rows[globalCounter].Cells[0].Value = worksheet.Cells[i + 1, 1].Value.ToString();
                            gridTargetsGroup.Rows[globalCounter].Cells[1].Value = worksheet.Cells[i + 1, 2].Value.ToString();
                            globalCounter++;

                        }
                    }
                    catch (Exception ex)
                    {
                        Utils.showAlert(ex.Message, Alerts.Alert.enmType.Error);
                    }
                }
            }
        }

        private void gridTargetsGroup_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            setCounterGroup();
        }

        private void gridTargetsGroup_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setCounterGroup();
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewFiveGroup);
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewOneGroup);
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewTwoGroup);
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewThreeGroup);
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewFourGroup);
        }

        private void btnStartGroup_Click(object sender, EventArgs e)
        {
            ValidateControlsGroup();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            GrabChatList grabGroups = new GrabChatList(this);
            this.Hide();
            grabGroups.ShowDialog();
        }

        private void materialButton9_Click(object sender, EventArgs e)
        {
            GetGroupMember getGroupMember = new GetGroupMember(this);
            this.Hide();
            getGroupMember.ShowDialog();
        }

        private void materialButton10_Click(object sender, EventArgs e)
        {
            GrabGroupLinks grabGroupLinks = new GrabGroupLinks(this);
            this.Hide();
            grabGroupLinks.ShowDialog();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void materialButton11_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://codecanyon.net/item/wasender-bulk-whatsapp-sender-group-sender-wahtsapp-bot/35762285");
        }

        private void materialButton11_Click_1(object sender, EventArgs e)
        {
            GroupsJoiner groupsJoiner = new GroupsJoiner(this);
            this.Hide();
            groupsJoiner.ShowDialog();
        }

        private void materialButton12_Click(object sender, EventArgs e)
        {
            WaAutoReplyBot.WaAutoReplyForm waAutoReplyForm = new WaAutoReplyBot.WaAutoReplyForm(this);
            this.Hide();
            waAutoReplyForm.ShowDialog();
        }

        private void materialButton13_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(materialButton13, new Point(0, materialButton13.Height));
        }

        private void keyMarkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyMarker keyMarker = new KeyMarker(this);
            keyMarker.ShowDialog();
        }

        public void AddKeyMarker(string KeyMarker)
        {
            SingleInsertKeyParams(KeyMarker);
        }

        private void SingleInsertKeyParams(string paramVals)
        {

            int MainTabIndex = tabMain.SelectedIndex;
            if (MainTabIndex == 0)
            {
                int tabIndex = materialTabControl2.SelectedIndex;
                if (tabIndex == 0)
                {
                    txtMsgOne.Text += paramVals;
                }
                else if (tabIndex == 1)
                {
                    txtMsgTwo.Text += paramVals;
                }
                else if (tabIndex == 2)
                {
                    txtMsgThree.Text += paramVals;
                }
                else if (tabIndex == 3)
                {
                    txtMsgFour.Text += paramVals;
                }
                else if (tabIndex == 4)
                {
                    txtMsgFive.Text += paramVals;
                }
            }
            else if (MainTabIndex == 1)
            {
                int tabIndex = materialTabControl1.SelectedIndex;
                if (tabIndex == 0)
                {
                    txtMsgOneGroup.Text += paramVals;
                }
                else if (tabIndex == 1)
                {
                    txtMsgTwoGroup.Text += paramVals;
                }
                else if (tabIndex == 2)
                {
                    txtMsgTHreeGroup.Text += paramVals;
                }
                else if (tabIndex == 3)
                {
                    txtMsgFourGroup.Text += paramVals;
                }
                else if (tabIndex == 4)
                {
                    txtMsgFiveGroup.Text += paramVals;
                }
            }

        }

        private void randomNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingleInsertKeyParams("{{ RANDOM }}");
        }

        private void materialButton14_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(materialButton13, new Point(0, materialButton13.Height));
        }

        private void materialButton16_Click(object sender, EventArgs e)
        {
            OpenGeneralSettings();
        }

        private void materialButton15_Click(object sender, EventArgs e)
        {
            OpenGeneralSettings();
        }


        private void OpenGeneralSettings()
        {
            //GeneralSettings generalSettings = new GeneralSettings(this);
            //this.Hide();
            //generalSettings.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            generalSettingsModel.selectedLanguage = comboBox1.Text;

            String GetGeneralSettingsFilePath = Config.GetGeneralSettingsFilePath();
            if (!File.Exists(GetGeneralSettingsFilePath))
            {
                File.Create(GetGeneralSettingsFilePath).Close();
            }

            string Json = JsonConvert.SerializeObject(generalSettingsModel, Formatting.Indented);
            File.WriteAllText(GetGeneralSettingsFilePath, Json);

            MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.LanguageIsSet, Strings.OK, true);
            SnackBarMessage.Show(this);
        }

        private void materialButton15_Click_1(object sender, EventArgs e)
        {
            NumberFilter numberFilter = new NumberFilter(this);
            this.Hide();
            numberFilter.ShowDialog();
        }

        private void materialButton16_Click_1(object sender, EventArgs e)
        {
            ContactGrabber contactGrabber = new ContactGrabber(this);
            this.Hide();
            contactGrabber.ShowDialog();
        }

        private void materialButton17_Click(object sender, EventArgs e)
        {
            GrabGroupActiveMembers grabGroupActiveMembers = new GrabGroupActiveMembers(this);
            this.Hide();
            grabGroupActiveMembers.ShowDialog();
        }

        private void gridTargets_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(gridTargets, new Point(e.Location.X, e.Location.Y));
            }
        }

        private void addCountryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CountryCodeInput countryCodeInput = new CountryCodeInput(this);
            countryCodeInput.ShowDialog();
        }

        private void materialButton18_Click(object sender, EventArgs e)
        {
            GMapExtractor gMapExtractor = new GMapExtractor(this);
            this.Hide();
            gMapExtractor.Show();

        }
        public void reEnableAutoReply()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            WaAutoReplyBot.WaAutoReplyForm waAutoReplyForm = new WaAutoReplyBot.WaAutoReplyForm(this, true);
            this.Hide();
            waAutoReplyForm.ShowDialog();
        }

        private void recycleChromeProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAddButtonDialog();
        }

        private void ShowAddButtonDialog()
        {
            ButtonsModel buttonsModel = new ButtonsModel();
            buttonsModel.buttonTypeEnum = enums.ButtonTypeEnum.NONE;
            AddButton keyMarker = new AddButton(this, buttonsModel);
            keyMarker.ShowDialog();
        }

        private void commonAddButtonList(ButtonsModel _buttonsModel, List<ButtonsModel> _buttonsModelList)
        {
            if (_buttonsModel.editMode == true)
            {
                int index = _buttonsModelList.FindIndex(x => x.id == _buttonsModel.id);
                _buttonsModel.editMode = false;
                _buttonsModelList[index] = _buttonsModel;
            }
            else
            {
                _buttonsModelList.Add(_buttonsModel);
            }
        }

        private void commonRemoveButtonList(ButtonsModel _buttonsModel, List<ButtonsModel> _buttonsModelList)
        {
            int index = _buttonsModelList.FindIndex(x => x.id == _buttonsModel.id);
            _buttonsModelList.Remove(_buttonsModelList[index]);

        }

        public void RemoveButton(ButtonsModel buttonsModel)
        {
            int MainTabIndex = materialTabControl2.SelectedIndex;
            if (MainTabIndex == 0)
            {
                commonRemoveButtonList(buttonsModel, buttonsModelList1);
            }
            if (MainTabIndex == 1)
            {
                commonRemoveButtonList(buttonsModel, buttonsModelList2);
            }
            if (MainTabIndex == 2)
            {
                commonRemoveButtonList(buttonsModel, buttonsModelList3);
            }
            if (MainTabIndex == 3)
            {
                commonRemoveButtonList(buttonsModel, buttonsModelList4);
            }
            if (MainTabIndex == 4)
            {
                commonRemoveButtonList(buttonsModel, buttonsModelList5);
            }

            geterateButtons();
        }
        public void RecievButton(ButtonsModel buttonsModel)
        {
            int MainTabIndex = materialTabControl2.SelectedIndex;
            if (MainTabIndex == 0)
            {
                commonAddButtonList(buttonsModel, buttonsModelList1);
            }
            if (MainTabIndex == 1)
            {
                commonAddButtonList(buttonsModel, buttonsModelList2);
            }
            if (MainTabIndex == 2)
            {
                commonAddButtonList(buttonsModel, buttonsModelList3);
            }
            if (MainTabIndex == 3)
            {
                commonAddButtonList(buttonsModel, buttonsModelList4);
            }
            if (MainTabIndex == 4)
            {
                commonAddButtonList(buttonsModel, buttonsModelList5);
            }

            geterateButtons();
        }


        private void CommonGenerateButtons(WebBrowser webBrowser, List<ButtonsModel> buttonsModelList)
        {
            string buttontext = Storage.DocumentHtmlString;
            string cssStyle = Storage.DocumentButtonStypeStrig;

            foreach (var item in buttonsModelList)
            {
                string txt = "";

                if (item.buttonTypeEnum == enums.ButtonTypeEnum.PHONE_NUMBER)
                {
                    txt = "📞" + item.text;
                }
                else if (item.buttonTypeEnum == enums.ButtonTypeEnum.URL)
                {
                    txt = "🔗 " + item.text;
                }
                else
                {
                    txt = item.text;
                }
                buttontext += "<button style='margin:5px;" + cssStyle + "' type='button' id='" + item.id + "' >" + txt + "</button>";
            }
            webBrowser.DocumentText = buttontext + "</body></html>";
        }
        private void geterateButtons()
        {
            int MainTabIndex = materialTabControl2.SelectedIndex;
            if (MainTabIndex == 0)
            {
                CommonGenerateButtons(webBrowser1, buttonsModelList1);
            }
            else if (MainTabIndex == 1)
            {
                CommonGenerateButtons(webBrowser2, buttonsModelList2);
            }
            else if (MainTabIndex == 2)
            {
                CommonGenerateButtons(webBrowser3, buttonsModelList3);
            }
            else if (MainTabIndex == 3)
            {
                CommonGenerateButtons(webBrowser4, buttonsModelList4);
            }
            else if (MainTabIndex == 4)
            {
                CommonGenerateButtons(webBrowser5, buttonsModelList5);
            }


        }

        private void materialButton19_Click(object sender, EventArgs e)
        {
            ShowAddButtonDialog();
        }

        private void importNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteNumber pasteNumber = new PasteNumber(this);
            pasteNumber.ShowDialog();

        }
    }


}
