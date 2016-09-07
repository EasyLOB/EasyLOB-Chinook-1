function zAjaxFailureMessage(jqXHR) { // jqXHR = jQuery XMLHttpRequest
    var message = "";

    if (jqXHR != null) {
        //alert("jqXHR");
        //alert(JSON.stringify(jqXHR));
        if (jqXHR.responseJSON != null) {
            //alert("jqXHR.responseJSON");
            //alert(JSON.stringify(jqXHR.responseJSON));
            if (jqXHR.responseJSON.OperationResult != null) {
                //alert("jqXHR.responseJSON.OperationResult");
                //alert(JSON.stringify(jqXHR.responseJSON.OperationResult));
                message = jqXHR.responseJSON.OperationResult.Html;
            }
        } else if (jqXHR.responseText != null) {
            message = jqXHR.responseText;
        }
    }

    //alert(message);

    return message;
}

function zEDMCssClass(fileAcronym) {
    var cssClass = "";

    switch (fileAcronym) {
        case "mp3":
            cssClass = "z-file-audio";
            break;
        case "xls":
        case "xlsx":
            cssClass = "z-file-excel";
            break;
        case "jpg":
        case "png":
            cssClass = "z-file-image";
            break;
        case "pdf":
            cssClass = "z-file-pdf";
            break;
        case "avi":
        case "mov":
        case "mp4":
        case "mpeg":
        case "wmv":
            cssClass = "z-file-video";
            break;
        case "doc":
        case "docx":
            cssClass = "z-file-word";
            break;
        default:
            cssClass = "z-file-unknown";
            break;
    }

    return cssClass;
}

function zErrorMessage(response) {
    var message = "?";

    if (response != undefined) {
        message = $(response).find("i").html();
        if (message != undefined) {
            message = message.replace("<br>", "\n");
        } else {
            message = $(response).find("fieldset").html();
            if (message != undefined) {
                message = message.replace("<br>", "\n");
            } else {
                message = response;
            }
        }
    }

    return message;
}

function zExceptionMessage(fileName, functionName, exception) {
    return "File: " + fileName +
        "\nFunction: " + functionName +
        "\nException: " + exception.message;
}

function zLookupElements(data, elements, culture) {    
    //alert(JSON.stringify(data));
    //alert(JSON.stringify(elements));
    //alert(JSON.stringify(culture));
    if (elements != null) {
        for (var i = 0, length = elements.length; i < length; i++) {
            // EasyLOB.Library.Mvc.LookupModelElement
            //     elements.Id
            //     elements.Property
            //alert(JSON.stringify(element.Id));
            //alert(JSON.stringify(element.Property));
            var element = elements[i];
            // $("#Id")
            if ($("#" + element.Id).length) {
                // data.Property
                if (data.hasOwnProperty(element.Property)) {
                    //alert(data[element.Property].toLocaleString(culture));
                    $("#" + element.Id).val(data[element.Property].toLocaleString(culture));
                }
            }
        }
    }
}
