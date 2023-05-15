////$(function () {
////    BuyersGetData();
////})



function BuyersGetData() {
    $.ajax({
        type: "POST",
        url:"/Buyers/Index" , /*'@Url.Action("Index", "Buyers")'*/
        data: "{}",
        contentType: "application/json;charset:utf-8",
        dataType: "json",
        success: function (response) {
            var dataa
            $.each(JSON.parse(response.d), function (index, column) {
                dataa += ("<tr >\
										<td> "+ column.BuyersId + "</td> + <td>" + column.Name + "</td> + <td>" + column.BuyersAddress + "</td></tr>");
            })

            $("#TableBody").html(
                dataa
            );

        },
        //error: function (response, textStatus, errorThrown) {
        //    var err = eval("(" + response.responseText + ")");
        //    alert(err.Message);
        //}
    });

    $('#Table').DataTable();

}