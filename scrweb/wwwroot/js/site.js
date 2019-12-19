// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function FormatarData(data) {
    //var jsData = eval(data.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
    var jsData = new Date(data);
    var dataFormatada = jsData.toLocaleDateString();

    return dataFormatada;
}

function FormatarDataIso(data) {
    data = FormatarData(data);
    var d = data.split("/");
    return d[2] + "-" + d[1] + "-" + d[0];
}