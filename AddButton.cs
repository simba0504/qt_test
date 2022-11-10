using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaAutoReplyBot;
using WASender.Models;
using WASender.Validators;
using FluentValidation.Results;
using MaterialSkin.Controls;


namespace WASender
{
    public partial class AddButton : MyMaterialPopOp
    {
        WaSenderForm waSenderForm;
        ButtonsModel buttonsModel;
        public AddButton(WaSenderForm _waSenderForm, ButtonsModel _buttonsModel)
        {
            waSenderForm = _waSenderForm;
            buttonsModel = _buttonsModel;
            InitializeComponent();
            init();
            var ss = buttonsModel.buttonTypeEnum;
            if (buttonsModel.buttonTypeEnum == enums.ButtonTypeEnum.NONE)
            {
                materialComboBox1.SelectedIndex = 0;
            }
            else if (buttonsModel.buttonTypeEnum == enums.ButtonTypeEnum.URL)
            {
                materialComboBox1.SelectedIndex = 1;
                materialTextBox22.Hint = Strings.EnterURL;
                materialTextBox22.Text = buttonsModel.url;
            }
            else if (buttonsModel.buttonTypeEnum == enums.ButtonTypeEnum.PHONE_NUMBER)
            {
                materialComboBox1.SelectedIndex = 2;
                materialTextBox22.Hint = Strings.EnterPhoneNumber;
                materialTextBox22.Text = buttonsModel.phoneNumber;
            }

            if (buttonsModel.editMode == false)
            {
                btnDelete.Hide();
            }

            materialTextBox21.Text = buttonsModel.text;
            ComboChange();
        }

        private void init()
        {
            this.Text = Strings.AddButtons;
            materialTextBox21.Hint = Strings.ButtonText;
            materialLabel1.Text = Strings.ButtonType;
            materialComboBox1.DisplayMember = "Text";
            materialComboBox1.ValueMember = "Value";
            materialComboBox1.Items.Add(new { Text = Strings.NormalButton, Value = "1" });
            materialComboBox1.Items.Add(new { Text = Strings.Link, Value = "2" });
            materialComboBox1.Items.Add(new { Text = Strings.PhoneNumber, Value = "3" });

            materialTextBox22.Hide();
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboChange();
        }

        private void ComboChange()
        {
            string selectedValue = (materialComboBox1.SelectedItem as dynamic).Value;
            if (selectedValue == "1")
            {
                materialTextBox22.Hide();
            }
            else if (selectedValue == "2")
            {
                materialTextBox22.Show();
                materialTextBox22.Hint = Strings.EnterURL;
            }
            else if (selectedValue == "3")
            {
                materialTextBox22.Show();
                materialTextBox22.Hint = Strings.EnterPhoneNumber;
            }
        }


        private void GenerateModel()
        {
            buttonsModel.text = materialTextBox21.Text;

            string selectedValue = "";

            try
            {
                selectedValue = (materialComboBox1.SelectedItem as dynamic).Value;
            }
            catch (Exception ex)
            {

            }

            if (selectedValue == "1")
            {
                buttonsModel.buttonTypeEnum = enums.ButtonTypeEnum.NONE;
            }
            else if (selectedValue == "2")
            {
                buttonsModel.buttonTypeEnum = enums.ButtonTypeEnum.URL;
                buttonsModel.url = materialTextBox22.Text;
            }
            else if (selectedValue == "3")
            {
                buttonsModel.buttonTypeEnum = enums.ButtonTypeEnum.PHONE_NUMBER;
                buttonsModel.phoneNumber = materialTextBox22.Text;
            }

            List<ValidationFailure> validator = new ButtonModelValidator().Validate(buttonsModel).Errors.ToList();


            MaterialSnackBar SnackBarMessage;
            if (validator.Count() > 0)
            {
                foreach (var item in validator)
                {
                    SnackBarMessage = new MaterialSnackBar(item.ErrorMessage, Strings.OK, true);
                    SnackBarMessage.Show(this);
                }
            }
            else
            {
                buttonsModel.id = Guid.NewGuid().ToString();
                waSenderForm.RecievButton(buttonsModel);
                this.Hide();
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            GenerateModel();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            waSenderForm.RemoveButton(buttonsModel);
            this.Hide();
        }
    }
}
