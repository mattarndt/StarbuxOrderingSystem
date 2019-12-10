function validate() {
    var name = document.getElementById("txtname").value;
    var phonenum = document.getElementById("txtphonenumber").value;

    var list = document.getElementById("options"); //Client ID of the radiolist
    var inputs = list.getElementsByTagName("input");
    var selected = false;

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].checked) {
            selected = true;
            break;
        }
    }

    if (name == "" || phonenum == "") {
        alert("You must enter a name and phone number");
    } else if (!selected) {
        alert("You must select a delivery option");
    } else {
        document.getElementById("form1").submit();
    }
}