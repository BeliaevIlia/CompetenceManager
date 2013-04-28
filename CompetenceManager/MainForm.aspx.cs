using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompetenceManager
{
    public partial class MainForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Exit_Click(object sender, EventArgs e)
        {
            Server.Transfer("Autorisation.aspx", true);
        }

        protected void btn_Competence_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditCompetence.aspx", true);
        }

        protected void btn_PostProfiles_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditPostProfile.aspx", true);
        }

        protected void btn_Staff_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditStaff.aspx", true);
        }

        protected void btn_testTasks_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditTestTasks.aspx", true);
        }

        protected void btn_Tests_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditTests.aspx", true);
        }

        protected void btn_Projects_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditProjects.aspx", true);
        }

        protected void btn_StudyPrograms_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditStudyPrograms.aspx", true);
        }
    }
}