using MaterialSkin.Controls;
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

namespace WASender
{
    public partial class ChooseGroup : MyMaterialPopOp
    {
        GetGroupMember getGroupMember;
        List<WAPI_GroupModel> wAPI_GroupModel;
        public ChooseGroup(GetGroupMember _getGroupMember, List<WAPI_GroupModel> _wAPI_GroupModel)
        {
            InitializeComponent();
            getGroupMember = _getGroupMember;
            wAPI_GroupModel = _wAPI_GroupModel;
            initLanguage();


            foreach (var item in wAPI_GroupModel)
            {
                MaterialSkin.MaterialListBoxItem lbitem=new MaterialSkin.MaterialListBoxItem();
                lbitem.Text=item.GroupName;
                materialListBox1.Items.Add(lbitem);
            }


        }

        private void initLanguage()
        {
            this.Text = Strings.ChooseGroup;
            materialButton1.Text = Strings.Select;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (materialListBox1.SelectedIndex < 0)
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.PleaseSelectGroup, Strings.OK, true);
                SnackBarMessage.Show(this);
            }
            else
            {
                this.getGroupMember.ReturnBack(materialListBox1.SelectedIndex);
                this.Hide();
            }
        }
    }
}
