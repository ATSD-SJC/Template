<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="GerarExcelTable.aspx.cs" Inherits="ProjetoTemplate.Paginas.GerarExcelTable" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/Default.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblinfo" runat="server" BackColor="#5D7B9D" Font-Names="Verdana"
            ForeColor="black"></asp:Label>
        <asp:SqlDataSource ID="Sql" runat="server"
            ConnectionString="<%$ ConnectionStrings:defaultConnection %>"
            ProviderName="<%$ ConnectionStrings:defaultConnection.ProviderName %>"></asp:SqlDataSource>
        <br />
        <asp:Label ID="lbltotal" runat="server" BackColor="#5D7B9D" Font-Names="Verdana"
            ForeColor="Black"></asp:Label>
        <asp:GridView ID="gvExcel" align="left" runat="server" CellPadding="4"
            ForeColor="#333333" Style="font-family: Verdana; font-size: x-small; text-align: left;"
            OnSelectedIndexChanged="gvExcel_SelectedIndexChanged"
            CaptionAlign="Left" OnDataBinding="gvExcel_DataBinding"
            OnInit="gvExcel_Init">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle Width="5px" BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

        <br />
    </form>
</body>
</html>
