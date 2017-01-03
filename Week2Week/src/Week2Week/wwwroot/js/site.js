// Write your Javascript code.
$("#Transaction_TransactionTypeId").on("change", function (e) {
    $.ajax({
        url: `/Transaction/GetSubTypes/${$(this).val()}`,
        method: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8'
    }).done((subTypes) => {
        $("#Transaction_TransactionSubTypeId").html("");
        $("#Transaction_TransactionSubTypeId").append("<option value=null> Choose the kind of credit or debit this is</option>");
        subTypes.forEach((option) => {
            console.log("these are the options", option);
            $("#Transaction_TransactionSubTypeId").append(`<option value="${option.TransactionSubTypeId}">${option.name}</option>`)
        });
    });
});
