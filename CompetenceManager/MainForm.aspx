<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="CompetenceManager.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Outset" HorizontalAlign="Justify" ScrollBars="Auto">
            <br />
            <asp:Table ID="Table1" runat="server" Width ="800px" Height="16px">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" Width ="500">
                        <asp:Panel ID="p_LeftColumn" runat="server" BorderStyle="None" Width ="500px">
                            <br />
                            <asp:Panel ID="Panel2" runat="server" BorderStyle="Outset" HorizontalAlign="Center">
                                <br />
                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Данные о вошедшем пользователе:"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="l_FIO" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="16pt" Text="ФИО"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="l_Post" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Должность"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="l_Role" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Роль в системе"></asp:Label>
                                <br />
                                <br />
                                <asp:Button ID="btn_Exit" runat="server" Font-Names="Arial" Font-Size="16pt" OnClick="btn_Exit_Click" Text="Выйти из программы" />
                                <br />
                                <br />
                            </asp:Panel>
                            <br />
                            <br />
                            <asp:Button ID="btn_LetsTest" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Пройти тест" Width="500px" />
                            <br />
                            <br />
                            <asp:Button ID="btn_GetCurrentPost" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Просмотреть текущий уровень сотрудника" Width="500px" />
                            <br />
                            <br />
                            <asp:Button ID="btn_CheckPost" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Проверить соответствие должности" Width="500px" />
                            <br />
                            <br />
                            <asp:Button ID="btn_GetStudyProgram" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Подобрать учебную программу" Width="500px" />
                            <br />
                            <br />
                            <asp:Button ID="btn_GetPartyProject" runat="server" Font-Names="Arial" Font-Size="16pt" Text="Подобрать команду на проект" Width="500px" />
                            <br />
                            <br />
            </asp:Panel>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width ="300">
                        <asp:Panel ID="p_RightColumn" runat="server" BorderStyle="Outset" Width ="300" HorizontalAlign="Center">
                            <br />
                            <asp:Button ID="btn_Competence" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Компетенции" Width="300px" OnClick="btn_Competence_Click" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_PostProfiles" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Должностные профили" Width="300px" OnClick="btn_PostProfiles_Click" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_Staff" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Сотрудники" Width="300px" OnClick="btn_Staff_Click" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_testTasks" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Тестовые задания" Width="300px" OnClick="btn_testTasks_Click" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_Tests" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Тесты" Width="300px" OnClick="btn_Tests_Click" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_Projects" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Проекты" Width="300px" OnClick="btn_Projects_Click" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btn_StudyPrograms" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="16pt" Font-Underline="True" Text="Учебные программы" Width="300px" OnClick="btn_StudyPrograms_Click" />
                            <br />
                            <br />
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            
        </asp:Panel>
    </div>
    </form>
</body>
</html>
