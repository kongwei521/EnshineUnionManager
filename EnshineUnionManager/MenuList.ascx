<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuList.ascx.cs" Inherits="EnshineUnionManager.MenuList" %>
				<aside id="sidebar">
	<nav id="navigation" class="collapse">
		<ul>
			<asp:Repeater ID="rpFatherMenu" runat="server" OnItemDataBound="rpFatherMenu_ItemDataBound">
				<ItemTemplate> 
				  <li id='<%#"topmenu"+Eval("menuid") %>'>
						<span title="<%#Eval("menuname") %>">
							<i class="<%#Eval("menuicon") %>"></i>
							<span class="nav-title"><%#Eval("menuname") %></span>
						</span> 
						<ul class="inner-nav">
							<asp:Repeater ID="rpSonMenu" runat="server">
								<ItemTemplate>
									<li id="sonmenu<%#Eval("menuid") %>"  >
										<a href='<%#Eval("menulink")+"?mid="+Eval("fathermenuid")+","+Eval("menuid") %>'>
											<i class="<%#Eval("menuicon") %>"></i>	<span class="digit"><%#Eval("menuname") %></span></a></li>
								</ItemTemplate>
							</asp:Repeater>
						</ul>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</nav>
</aside>
