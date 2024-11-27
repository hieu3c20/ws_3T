<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FAQ.aspx.vb" Inherits="FAQ"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <style type="text/css">
        .faq-question
        {
            text-transform: none !important;
            font-weight: bold;
        }
        ul.faq
        {
            padding-left: 20px;
        }
        ul.faq li
        {
            list-style-type: disc;
            margin-left: 10px;
            padding: 5px 0;
        }
        h4
        {
            margin-top: 5px;
        }
        table.grid.download-grid
        {
            margin-top: 0px !important;
        }
        table.grid.download-grid tr td
        {
            padding: 5px 10px 5px 10px !important;
        }
        ul.faq li span
        {
            color: #000;
        }
        ul.faq li a
        {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Frequently Asked Questions
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Khi tôi cần liên hệ với các phòng ban liên quan thì tôi liên hệ với ai?
                (When I need to contact with related department, who can I contact?)
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <h4>
                        Finance:</h4>
                    <ul class="faq">
                        <li>HQ: Nguyễn Thị Hoài Linh, Ext: 2242, Email: <a href="mailto:linhnth@toyotavn.com.vn">
                            linhnth@toyotavn.com.vn</a></li>
                        <li>HN: Nguyễn Quốc Cẩm, Ext: 301 , Email: <a href="mailto:adcamnq@toytotavn.com.vn">
                            adcamnq@toytotavn.com.vn</a></li>
                        <li>HCM: Nguyễn Thị Thu, Ext: 111/112, Email: <a href="mailto:tbthu@toyotavn.com.vn">
                            tbthu@toyotavn.com.vn</a></li>
                    </ul>
                    <h4>
                        Budget:</h4>
                    <ul class="faq">
                        <li>Trần Khánh Huy, Ext: 2221, Email: <a href="mailto:huytk@toyotavn.com.vn">huytk@toyotavn.com.vn</a></li>
                        <li>Đỗ Thị Thủy, Ext: 2221, Email: <a href="mailto:thuydt@toyotavn.com.vn">thuydt@toyotavn.com.vn</a></li>
                    </ul>
                    <h4>
                        GA:</h4>
                    <ul class="faq">
                        <li>HQ: Nguyễn Thị Hà, Ext: 1009, Email: <a href="mailto:adhant@toyotavn.com.vn">adhant@toyotavn.com.vn</a>;</li>
                        <li style="list-style: none"><span style="visibility: hidden">HQ:</span> Nguyễn Thị
                            Thu Hà, Ext: 2201, Email: <a href="mailto:adhantt@toyotavn.com.vn">adhantt@toyotavn.com.vn</a></li>
                        <li>HN: Phan Thị Thanh Hòa, Ext: 303, Email: <a href="mailto:hnhoapt@toyotavn.com.vn">
                            hnhoapt@toyotavn.com.vn</a>;</li>
                        <li style="list-style: none"><span style="visibility: hidden">HN:</span> Hoàng Thị Phượng,
                            Ext:200, Email: <a href="mailto:hnphuonght@toyotavn.com.vn">hnphuonght@toyotavn.com.vn</a></li>
                        <li>HCM: Trần Thị Hạnh Uyên, Ext: 112, Email: <a href="mailto:tbuyentth@toyotavn.com.vn">
                            tbuyentth@toyotavn.com.vn</a></li>
                    </ul>
                    <h4>
                        IT:</h4>
                    <ul class="faq">
                        <li>All sites: IT Supporter: Ext: 2222 , Email: <a href="mailto:support@toyotavn.com.vn">
                            support@toyotavn.com.vn</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Trình duyệt nào hỗ trợ tốt nhất khi sử dụng chương trình Business Trip?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Firefox version 35 and above</li>
                        <li>Internet Explorer version 10 and above</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Quy trình làm Business Trip Online như thế nào?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <h4>
                        Trước khi đi công tác</h4>
                    <ul class="faq">
                        <li>Người yêu cầu (requester) nhập kế hoạch đi công tác vào hệ thống, sau đó in quyết
                            định phê duyệt từ hệ thống để xin ký theo LOA </li>
                        <li>Người yêu cầu gửi (ấn nút submit) thông tin chuyến đi lên bộ phận kế toán</li>
                        <li>Kế toán kiểm tra và phê duyệt ngân sách </li>
                        <li>Hành chính tiến hành các thủ tục đặt vé máy bay, khách sạn... (nếu có) (Chỉ xuất
                            vé khi nhận được bản cứng phê duyệt)</li>
                        <li>Kế toán kiểm tra giữa bản cứng phê duyệt và hệ thống để làm tạm ứng đi công tác cho nhân viên</li>
                        <li>(Các bước trên sẽ được gửi email tới email của người yêu cầu để theo dõi)</li>
                    </ul>
                    <h4>
                        Sau khi đi công tác</h4>
                    <ul class="faq">
                        <li>Người yêu cầu kê khai chi phí đi công tác vào trong hệ thống, sau đó in bản kê khai
                            chi phí từ hệ thống và xin ký duyệt theo LOA</li>
                        <li>Người yêu cầu chuyển các giấy tờ, bản gốc liên quan đến chuyến công tác cho kế toán</li>
                        <li>Kế toán kiểm tra thông tin công tác, giấy tờ bản gốc liên quan và tiến hành làm
                            thủ tục thanh toán (Sau khi nhận bản cứng phê duyệt từ user)</li>
                        <li>(Các bước trên sẽ được gửi email tới email của người yêu cầu để theo dõi)</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Mức phụ cấp di chuyển được tính như thế nào?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <div class="ui-datatable-tablewrapper">
                        <table class="grid download-grid">
                            <tr>
                                <th style="width: 35%">
                                    Điểm đến khi công tác
                                </th>
                                <th style="width: 35%">
                                    Danh sách điểm đến thường xuyên
                                </th>
                                <th style="width: 15%">
                                    Điều kiện
                                </th>
                                <th style="width: 15%">
                                    Trợ cấp
                                </th>
                            </tr>
                            <tr>
                                <td colspan="4" style="font-weight: bold">
                                    Nội địa
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px !important;">
                                    Trong cùng tỉnh, thành phố
                                </td>
                                <td>
                                    HNB<->TGP, HO<->THVP, TRN<->TTX, TSC<->TBD ...
                                </td>
                                <td rowspan="2">
                                </td>
                                <td rowspan="2">
                                    Không áp dụng
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Giữa các chi nhánh, trụ sở chính
                                </td>
                                <td>
                                    HNB<->HO, HNB<->TRN, HCM<->TSC, TRN<->HO
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Miền Bắc Việt Nam
                                </td>
                                <td>
                                    Hai Phong, Hai Duong, Quang Ninh, Thai Nguyen ...
                                </td>
                                <td rowspan="3">
                                    ≥ 60km tính từ địa điểm làm việc
                                </td>
                                <td rowspan="3">
                                    130,000vnd/ chuyến
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Miền Trung Việt Nam
                                </td>
                                <td>
                                    Thanh Hoa, Nghe An, Hue, Da Nang, Nha Trang ....
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Miền Nam Việt Nam
                                </td>
                                <td>
                                    Can Tho, Vung Tau, Gia Lai....
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="font-weight: bold">
                                    Nước ngoài
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ASEAN & Đài Loan
                                </td>
                                <td>
                                    Thailand, Singapore, Indonexia, Malaysia, Philippines..
                                </td>
                                <td rowspan="5">
                                </td>
                                <td>
                                    6$/chuyến
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ASIA (Ngoại trừ ASEAN & Đài Loan)
                                </td>
                                <td>
                                    Japan, India ...
                                </td>
                                <td>
                                    14$/chuyến
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    EUROPE
                                </td>
                                <td>
                                    Russia
                                </td>
                                <td>
                                    20$/chuyến
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OCEANIA
                                </td>
                                <td>
                                    Australia
                                </td>
                                <td>
                                    20$/chuyến
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    AMERICA
                                </td>
                                <td>
                                    USA
                                </td>
                                <td>
                                    20$/chuyến
                                </td>
                            </tr>
                        </table>
                        <p style="color: Red">
                            <span style="font-weight: bold">(*)Đối tượng áp dụng: </span>Chỉ áp dụng đối với
                            chức vụ từ Phó phòng trở xuống.</p>
                    </div>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Khoảng cách >= 60 km để xét phụ cấp di chuyển là tính trên 1 chiều hay
                cộng cả hai chiều?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Khoảng cách >= 60 km để xét phụ cấp di chuyển là tính trên 1 chiều.</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Do yêu cầu của công việc bắt buộc phải đi công tác vào ngày nghỉ thứ 7,
                chủ nhật thì mức phụ cấp sẽ được tính như thế nào?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <h4>
                    Việc đi công tác vào ngày nghỉ thứ 7, chủ nhật thì sẽ có các khoản phụ cấp sau:
                </h4>
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>1. Được trả OT cho thời gian làm việc tại nơi đến (không trả OT cho thời gian di
                            chuyển) </li>
                        <li>2. Phụ cấp di chuyển (theo địa điểm công tác)</li>
                        <li>3. Phụ cấp công tác khác (theo chính sách "đi công tác")</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Trường hợp nhân viên đi công tác ngắn, nhiều lần trong ngày mà có tổng
                quãng đường >=60 km thì có được hưởng phụ cấp không?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Phụ cấp di chuyển khi đi công tác không áp dụng cho các chuyến công tác nội tỉnh,
                            các chuyến công tác ngắn <60 km</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Budget Remaining đang được tính như thế nào?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <h4>
                    Công thức</h4>
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>A: Bằng tổng ngân sách (budget) trừ đi số tiền đã thanh toán với kế toán(invoiced)</li>
                        <li>B: Bằng số tiền tạm ứng đã được phòng kế toán phê duyệt ngân sách (budget) trên
                            BTS (chưa gồm vé máy bay) và chưa thanh toán với kế toán</li>
                        <li>C: Bằng A trừ B là số tiền còn lại (Budget Remaining) đang hiển thị trên BTS</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Tiền tạm ứng khách sạn tính như thế nào khi đi nước ngoài?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Là giá phòng thực tế mà khách sạn yêu cầu trả, phải có bằng chứng xác nhận từ khách
                            sạn (Actual booking amount) gửi kế toán </li>
                        <li>Nếu sử dụng thẻ tín dụng, không được tạm ứng tiền khách sạn bằng tiền mặt(Từ MA
                            trở lên) </li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Trong trường hợp Requester làm sai và Kế toán Reject lại thì Requester
                cần làm gì?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Requester cần chỉnh sửa lại thông tin trên bản request bị reject rồi submit lại.</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Nếu Requester không lấy tạm ứng khi đi công tác trong ngày (One day trip)
                thì Requester có phải khai báo BT Approval không?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Không cần khai báo BT Approval khi không lấy tạm ứng công tác trong ngày</li>
                        <li>Vào thẳng chức năng BT Expense Declaration để khai báo và in 2 biểu mẫu BT Approval
                            và BT Expense Declaration xin ký.<br />
                            <span style="color: red">(Có thể gộp nhiều chuyến đi công tác trong ngày của cùng 1
                                tháng để khai báo chi phí 1 lần)</span></li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Tại sao tôi hay nhận được email cảnh báo về việc sử dụng budget của nhân
                viên phòng khác từ hệ thống BTS?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Vì nhân viên phòng khác đang sử dụng budget của phòng Anh/Chị quản lý để đi công
                            tác.<br />
                            (Anh/Chị đang là P.I.C budget của phòng mình)</li>
                    </ul>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top faq-question"
                role="tab">
                <span class="ui-icon"></span>
                <%=No%>. Tôi không tìm thấy chức năng thoát khỏi hệ thống BTS (Log out), đổi mật
                khẩu (Change password) của tôi?
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <ul class="faq">
                        <li>Từ góc trên bên phải màn hình, click vào biểu tượng hình người. Ví dụ như ảnh dưới:
                            <br />
                            <img src="/images/app/profile.png" alt="" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">
</asp:Content>
