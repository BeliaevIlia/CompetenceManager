using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompetenceManager
{
    public partial class Autorisation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_GoIn_Click(object sender, EventArgs e)
        {
            if (tb_Password.Text != "112233")
                l_Error.Visible = true;
            else
                Server.Transfer("MainForm.aspx", true);
        }

        protected void tb_Password_TextChanged(object sender, EventArgs e)
        {
            l_Error.Visible = false;
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            tb_Password.Text = "";
            l_Error.Visible = false;
        }
    }
}