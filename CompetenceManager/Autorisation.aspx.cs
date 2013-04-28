using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompetenceManager
{
    public partial class Autorisation : System.Web.UI.Page
    {
        DataClasses1DataContext _eJDataContext;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["CompDBConnectionString"].ConnectionString;
            _eJDataContext = new DataClasses1DataContext(connStr);
        }

        protected void btn_GoIn_Click(object sender, EventArgs e)
        {
            var allStaff = from staff in _eJDataContext.Staff.ToList()
                           select staff;
            foreach (Staff staf in allStaff)
            {
                if (tb_Password.Text == staf.Password.Replace(" ", ""))
                    Server.Transfer("MainForm.aspx", true);
            }
            l_Error.Visible = true;
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